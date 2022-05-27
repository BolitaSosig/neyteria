using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // CONSTANTES
    protected const float SPEED_MOV = 2f;
    protected const float JUMP_FORCE = 15f;
    protected float DISTANCIA_EN_SEGUNDOS = 3f;

    // ATRIBUTOS PERSONAJE
    [SerializeField] protected int _nivel = 1;
    protected int _oldNivel = 0;
    [SerializeReference] protected float HP = 50f;
    [SerializeField] protected float MaxHP = 100f;
    [SerializeField] protected float Stamina = 100f;
    [SerializeField] protected float Attack = 4f;
    [SerializeField] protected float Defense = 1f;
    [SerializeField] protected float Weight = 1f;
    [SerializeField] protected float MovSpeed = 1f;
    [SerializeField] protected float AttSpeed = 1f;
    [SerializeField] protected float JumpCap = 1f;
    [SerializeField] protected float dmgReduc = 0f;
    [SerializeField] protected float mov_x = 0f;
    [SerializeField] protected int cont_mov_x = 0;

    protected bool moving = false;
    protected bool dying = true;

    // REFERENCIAS
    protected Rigidbody2D _rigidbody2D;
    protected BoxCollider2D _boxCollider2D;
    protected PolygonCollider2D _polygonCollider2D;
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected TextMeshProUGUI _levelText;
    [SerializeField] protected Transform _canvasTranform;
    [SerializeField] protected GameObject _damageDealTMP;
    [SerializeField] public WeatherController _weather;

    // Barra de vida
    [SerializeField] public Transform healthBarEnemy;

    public float baseHP;
    public float baseAttack;
    public float baseDefense;
    public float baseMovSpeed;
    public float baseAttSpeed;
    public EnemyDrop drop;

    protected int _weatherVarNivel = 0;

    public bool levelHasChanged
    {
        get { return _nivel != _oldNivel; }
    }

    public int Nivel
    {
        set
        {
            _oldNivel = value;
            UpdateStats();
            LevelTextUpdate();
        }
    }

    protected (Vector2, Vector2) getGroundCheckCorners()
    {
        Vector3 max = _boxCollider2D.bounds.max;
        Vector3 min = _boxCollider2D.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.1f);
        return (corner1, corner2);
    }
    public bool grounded
    {
        get
        {
            var (corner1, corner2) = getGroundCheckCorners();
            Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
            return (hit != null);
        }
    }

    protected bool checkBorderPlatform()
    {
        Vector3 max = _boxCollider2D.bounds.max;
        Vector3 min = _boxCollider2D.bounds.min;
        Vector2 corner1 = new Vector2(max.x + Mathf.Sign(transform.localScale.x) * 0.25f, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x + Mathf.Sign(transform.localScale.x) * 0.25f, min.y - 0.1f);
        return Physics2D.OverlapPoint(corner1) == null || Physics2D.OverlapPoint(corner2) == null;
    }

    public bool canDash
    {
        get { return Input.GetAxisRaw("Dash") == 1; }
    }

    /* Flag que devuelve True si el personaje está en el aire. */
    public bool onAir
    {
        get { return _rigidbody2D.velocity.y != 0 /*&& !grounded*/; }
    }

    protected virtual void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _weather = GameObject.Find("WeatherController").GetComponent<WeatherController>();

        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), _boxCollider2D);
    }


    protected virtual void Update()
    {
        CheckWeatherChange();
        if (levelHasChanged)
            Nivel = _nivel;
        if (!PauseMenu.GameIsPaused)
            StartCoroutine(Moverse());
        HealthBarUpdate();
    }

    protected IEnumerator Moverse()
    {
        if (!moving)
        {
            moving = true;
            float cont = 0;
            while (!checkBorderPlatform() && cont < DISTANCIA_EN_SEGUNDOS * 10f / MovSpeed)
            {
                _rigidbody2D.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * SPEED_MOV * MovSpeed, _rigidbody2D.velocity.y); // desplazamiento del personaje
                _animator.SetFloat("velocity_x", Mathf.Abs(_rigidbody2D.velocity.x)); // establece velocity_x en el animator*/
                cont++;
                yield return new WaitForSecondsRealtime(0.1f);
            }

            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y); // quieto el personaje
            _animator.SetFloat("velocity_x", Mathf.Abs(_rigidbody2D.velocity.x)); // establece velocity_x en el animator*/
            yield return new WaitForSecondsRealtime(1f);

            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            _canvasTranform.localScale = new Vector2(-_canvasTranform.localScale.x, _canvasTranform.localScale.y);
            moving = false;
        }

    }

    protected void DoDamage(GameObject player)
    {
        player.GetComponent<PlayerController>().GetDamageByEnemy(Attack);
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            DoDamage(collision.gameObject);
    }


    protected void HealthBarUpdate()
    {
        healthBarEnemy.localScale = new Vector2(HP / MaxHP, healthBarEnemy.localScale.y);
    }

    protected void LevelTextUpdate()
    {
        _levelText.text = "Nivel " + _nivel;
    }

    public IEnumerator Die()
    {
        _animator.SetTrigger("dead");
        _boxCollider2D.enabled = false;
        _rigidbody2D.gravityScale = 0f;
        drop.GetDrops();
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
    }

    ////// RECIBE DAÑO //////
    public void GetDamage(float dmg)
    {
        HP = Mathf.Max(0, HP - dmg);
        ShowDamageDeal(Mathf.RoundToInt(dmg));
        if (dying && HP <= 0)
        {
            dying = false;
            StartCoroutine(Die());
        }
    }

    public void GetDamageByPlayer(float attack)
    {
        if(dying)
            GetDamage(5 * attack / Mathf.Sqrt(Defense) * (1 - dmgReduc));
    }

    protected void UpdateStats()
    {
        Debug.Log(HP);
        float var = (100 + (_nivel * (_nivel - 1) * GLOBAL.AUMENTO_NV) / 4) / 100f;
        float oldMHP = MaxHP;
        MaxHP = baseHP * var;
        HP = HP / oldMHP * MaxHP;
        Attack = baseAttack * var;
        Defense = baseDefense * var;
        MovSpeed = baseMovSpeed * (0.99f + _nivel / 100f);
        AttSpeed = baseAttSpeed * (0.99f + _nivel / 100f);
    }

    protected void ShowDamageDeal(int dmg)
    {
        _damageDealTMP.GetComponentInChildren<TextMeshPro>().text = dmg.ToString();
        Instantiate(_damageDealTMP, transform.position, Quaternion.identity);
    }

    protected void CheckWeatherChange()
    {

        if (_weather.WeatherChanged)
        {
            switch ((int)_weather._current)
            {
                case 0: // DAY
                    _levelText.color = Color.white;
                    SetStatsDay();
                    break;
                case 1: // NIGHT
                    _levelText.color = new Color(1f, 0.67f, 0.11f); // Naranja
                    SetStatsNight();
                    break;
                case 2: // STORM
                    _levelText.color = Color.cyan;
                    SetStatsStorm();
                    break;
                case 3: // ECLIPSE
                    _levelText.color = Color.red;
                    SetStatsEclipse();
                    break;
            }
        }
    }

    protected void SetStatsDay()
    {
        _nivel = Mathf.Max(1, _nivel - _weatherVarNivel);
        _weatherVarNivel = 0;
    }
    protected void SetStatsNight()
    {
        _nivel = _nivel - _weatherVarNivel + 5;
        _weatherVarNivel = 5;
    }
    protected void SetStatsStorm()
    {
        if (_nivel - _weatherVarNivel - 5 < 1)
        {
            _weatherVarNivel = _nivel - 1;
            _nivel = 1;
        }
        else
        {
            _nivel = _nivel - _weatherVarNivel - 5;
            _weatherVarNivel = -5;
        }
    }
    protected void SetStatsEclipse()
    {
        _nivel = _nivel - _weatherVarNivel + 10;
        _weatherVarNivel = 10;
    }
}
