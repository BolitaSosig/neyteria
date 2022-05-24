using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.Tilemaps;

public class ElementalWater : MonoBehaviour
{
    // CONSTANTES
    private const float SPEED_MOV = 2f;
    private const float JUMP_FORCE = 15f;
    private float DISTANCIA_EN_SEGUNDOS = 3f;

    public GameObject[] enemigosNivel = new GameObject[5];

    // ATRIBUTOS PERSONAJE
    [SerializeField] private int _nivel = 1;
    private int _oldNivel = 0;
    [SerializeReference] private float HP = 50f;
    [SerializeField] private float MaxHP = 100f;
    [SerializeField] private float Stamina = 100f;
    [SerializeField] private float Attack = 4f;
    [SerializeField] private float Defense = 1f;
    [SerializeField] private float Weight = 1f;
    [SerializeField] private float MovSpeed = 1f;
    [SerializeField] private float AttSpeed = 1f;
    [SerializeField] private float JumpCap = 1f;
    [SerializeField] private float dmgReduc = 0f;
    [SerializeField] private float mov_x = 0f;
    [SerializeField] private int cont_mov_x = 0;

    private float distReco = 0f;
    private bool moving = false;

    // REFERENCIAS
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Transform _canvasTranform;
    [SerializeField] private GameObject _damageDealTMP;
    [SerializeField] public WeatherController _weather;

    // Barra de vida
    [SerializeField] public Transform healthBarEnemy;


    //Attack
    public Transform _shootTransform;
    public GameObject projectile;
    public float swordRange = 0.6f;
    public Transform _ability1Transform;
    public float ability1Range = 0.6f;
    public Transform _ability2Transform;
    public float ability2Range = 0.6f;

    public LayerMask playerLayers;

    public GameObject _audioSource;

    float lookRadius = 10f;
    Transform target;

    float seDispara = 9.95f;
    bool attacking = false;


    bool firstTimeMediaVida = true;
    bool firstTimePocaVida = true;

    private CameraController _camera;
    private GameObject _player;
    [SerializeField] private GameObject _togglePlatform;
    [SerializeField] private GameObject _transformPlataform;



    public float baseHP;
    public float baseAttack;
    public float baseDefense;
    public float baseMovSpeed;
    public float baseAttSpeed;

    private int _weatherVarNivel = 0;

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

    private (Vector2, Vector2) getGroundCheckCorners()
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

    public bool canDash {
        get { return Input.GetAxisRaw("Dash") == 1; }
    }

    /* Flag que devuelve True si el personaje está en el aire. */
    public bool onAir
    {
        get { return _rigidbody2D.velocity.y != 0 /*&& !grounded*/; }
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _weather = GameObject.Find("WeatherController").GetComponent<WeatherController>();

        _camera = GameObject.FindObjectOfType<CameraController>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        _player = GameObject.Find("Player");
    }


    void Update()
    {
        CheckWeatherChange();
        if (levelHasChanged)
            Nivel = _nivel;

        if (!PauseMenu.GameIsPaused) 
            StartCoroutine(Moverse());
        HealthBarUpdate();
        CheckEnemy();
    }

    IEnumerator Moverse()
    {
        if(!moving)
        {
            //Debug.Log("moviendo");
            moving = true;
            float cont = 0;
            while (cont < DISTANCIA_EN_SEGUNDOS * 10f/MovSpeed)
            {
                _rigidbody2D.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * SPEED_MOV * MovSpeed, _rigidbody2D.velocity.y); // desplazamiento del personaje
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

    void DoDamage(GameObject player)
    {
        player.GetComponent<PlayerController>().GetDamageByEnemy(Attack);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
            DoDamage(collision.gameObject);
    }
    

    private void HealthBarUpdate()
    {
        healthBarEnemy.localScale = new Vector2(HP / MaxHP, healthBarEnemy.localScale.y);
    }

    private void LevelTextUpdate()
    {
        _levelText.text = "Nivel " + _nivel;
    }

    public IEnumerator Die()
    {
        _animator.SetTrigger("dead");
        _canvasTranform.gameObject.SetActive(false);
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _boxCollider2D.enabled = false;
        this.enabled = false;
        _rigidbody2D.gravityScale = 0f;
        _audioSource.SetActive(false);
        yield return new WaitForSecondsRealtime(2f);
        if (_togglePlatform != null) _togglePlatform.SetActive(false);
        //Destroy(gameObject);
    }

    public IEnumerator DoAttack()
    {
        if (!attacking) {
        attacking = true;

        _animator.SetTrigger("attack");
        yield return new WaitForSecondsRealtime(0.7f);

            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(_shootTransform.position, swordRange, playerLayers);

            foreach (Collider2D player in hitPlayers)
            {
                //enemy.GetComponent<Enemy1Controller>().GetDamage(swordDMG);
                Debug.Log("colision con Player");
                player.SendMessage("GetDamageByEnemy", Attack);
            }

            yield return new WaitForSecondsRealtime(AttSpeed - 0.5f);

        attacking = false;
        }
    }

    public IEnumerator DoAbility()
    {
        if (!attacking)
        {
            attacking = true;

            

            _animator.SetTrigger("ability");
            yield return new WaitForSecondsRealtime(0.5f);
            GameObject newBullet;
            //newBullet = Instantiate(projectile, target.position, target.rotation);
            newBullet = Instantiate(projectile, new Vector3(target.position.x, target.position.y + 2, target.position.z), target.rotation);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, newBullet.GetComponent<Rigidbody2D>().velocity.y);
            newBullet.GetComponent<BringerOfDeathProjectile>().gunDMG = Attack;
            Destroy(newBullet, 1.4f);

            if (Random.Range(0f, 10f) >= 9)
            {
                InstantiateEnemy(Random.Range(0, 5), 1, new Vector3(target.position.x, target.position.y + 2, target.position.z), target.rotation);
                /*GameObject newBullet;
                newBullet = Instantiate(enemigosNivel[Random.Range(0,5)], new Vector3(target.position.x, target.position.y + 2, target.position.z), target.rotation);
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);*/
            }

            yield return new WaitForSecondsRealtime(AttSpeed - 0.5f);

            attacking = false;
        }
    }

    public IEnumerator DoAbility1()
    {
        if (!attacking)
        {
            attacking = true;

            _animator.SetTrigger("ability1");
            yield return new WaitForSecondsRealtime(0.7f);

            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(_ability1Transform.position, ability1Range, playerLayers);

            foreach (Collider2D player in hitPlayers)
            {
                //enemy.GetComponent<Enemy1Controller>().GetDamage(swordDMG);
                Debug.Log("colision con Player");
                player.SendMessage("GetDamageByEnemy", Attack);
            }

            yield return new WaitForSecondsRealtime(AttSpeed - 0.5f);

            attacking = false;
        }
    }

    public IEnumerator DoAbility2()
    {
        if (!attacking)
        {
            attacking = true;

            _animator.SetTrigger("ability2");
            yield return new WaitForSecondsRealtime(0.7f);

            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(_ability2Transform.position, ability2Range, playerLayers);

            foreach (Collider2D player in hitPlayers)
            {
                //enemy.GetComponent<Enemy1Controller>().GetDamage(swordDMG);
                Debug.Log("colision con Player");
                player.SendMessage("GetDamageByEnemy", Attack);
            }

            yield return new WaitForSecondsRealtime(AttSpeed - 0.5f);

            attacking = false;
        }
    }

    public void CheckEnemy()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            //StopCoroutine(Moverse()); //moving = false; //StopAllCoroutines();
            if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAttack()); }
            if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAbility()); }

            if (!firstTimeMediaVida)
            {
                if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAbility1()); }
                if (Random.Range(0f, 10f) >= seDispara && !attacking) { StartCoroutine(DoAbility2()); }
            }

            //Debug.Log(distance);
            // Move towards the target

            if (transform.position.x <= target.position.x)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                _canvasTranform.localScale = new Vector2(-Mathf.Abs(_canvasTranform.localScale.x), _canvasTranform.localScale.y);
                _rigidbody2D.velocity = new Vector2(2, _rigidbody2D.velocity.y);
            }
            else
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                _canvasTranform.localScale = new Vector2(Mathf.Abs(_canvasTranform.localScale.x), _canvasTranform.localScale.y);
                _rigidbody2D.velocity = new Vector2(-2, _rigidbody2D.velocity.y);
            }

        }

        else
        { //_rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            //StartCoroutine(Moverse());
        }

        _animator.SetFloat("velocity_x", Mathf.Abs(_rigidbody2D.velocity.x)); // establece velocity_x en el animator*/
        
    }

    void InstantiateEnemy(int idEnemigo, int nEnemigos ,Vector3 donde, Quaternion rotacion)
    {
        for (int i = 0; i < nEnemigos; i++){
            GameObject newEnemy;
            newEnemy = Instantiate(enemigosNivel[idEnemigo], donde, rotacion);
            newEnemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.green;
        if (_shootTransform != null) Gizmos.DrawWireSphere(_shootTransform.position, swordRange);
        Gizmos.color = Color.white;
        if (_ability1Transform != null) Gizmos.DrawWireSphere(_ability1Transform.position, ability1Range);
        Gizmos.color = Color.yellow;
        if (_ability2Transform != null) Gizmos.DrawWireSphere(_ability2Transform.position, ability2Range);
    }

    void InvocarEnemigos1()
    {
        firstTimeMediaVida = false;
        _animator.SetBool("fury", true);

        for (int i = 0; i < 2; i++) InstantiateEnemy(Random.Range(0, 5), 1, new Vector3(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + 10, gameObject.transform.position.z), gameObject.transform.rotation);

        InstantiateEnemy(2, 1, new Vector3(gameObject.transform.position.x + Random.Range(-4,4), gameObject.transform.position.y + 10, gameObject.transform.position.z), gameObject.transform.rotation);

        for (int i = 0; i < 5; i++)
        {
            GameObject newBullet;
            //newBullet = Instantiate(projectile, target.position, target.rotation);
            newBullet = Instantiate(projectile, new Vector3(gameObject.transform.position.x + Random.Range(-8, 8), target.position.y + 2, target.position.z), target.rotation);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, newBullet.GetComponent<Rigidbody2D>().velocity.y);
            newBullet.GetComponent<BringerOfDeathProjectile>().gunDMG = Attack;
            Destroy(newBullet, 1.4f);
        }
    }

    void InvocarEnemigos2()
    {
        firstTimePocaVida = false;

        for (int i = 0; i < 2; i++) InstantiateEnemy(Random.Range(0, 4), 1, new Vector3(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + 10, gameObject.transform.position.z), gameObject.transform.rotation);

        InstantiateEnemy(4, 2, new Vector3(gameObject.transform.position.x + Random.Range(-4, 4), gameObject.transform.position.y + 10, gameObject.transform.position.z), gameObject.transform.rotation);

        for (int i = 0; i < 5; i++)
        {
            GameObject newBullet;
            //newBullet = Instantiate(projectile, target.position, target.rotation);
            newBullet = Instantiate(projectile, new Vector3(gameObject.transform.position.x + Random.Range(-8, 8), target.position.y + 2, target.position.z), target.rotation);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(_shootTransform.right * shootForce);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, newBullet.GetComponent<Rigidbody2D>().velocity.y);
            newBullet.GetComponent<BringerOfDeathProjectile>().gunDMG = Attack;
            Destroy(newBullet, 1.4f);
        }
    }


    ////// RECIBE DAÑO //////
    public void GetDamage(float dmg)
    {
        _animator.SetTrigger("hurt");
        HP = Mathf.Max(0, HP - dmg);
        ShowDamageDeal(Mathf.RoundToInt(dmg));
        if (HP <= (MaxHP/2) && firstTimeMediaVida) { InvocarEnemigos1(); healthBarEnemy.gameObject.GetComponent<RawImage>().color = Color.yellow; }
        if (HP <= (MaxHP/5) && firstTimePocaVida) { InvocarEnemigos2(); healthBarEnemy.gameObject.GetComponent<RawImage>().color = Color.magenta; }

        if (HP <= 0) { StopAllCoroutines(); moving = true; _animator.SetBool("isDead", true); StartCoroutine(Die()); } else { _animator.SetTrigger("hurt"); }
    }

    public void GetDamageByPlayer(float attack)
    {
        GetDamage(5 * attack / Mathf.Sqrt(Defense) * (1 - dmgReduc));
    }

    void UpdateStats()
    {
        float var = (100 + (_nivel * (_nivel - 1) * GLOBAL.AUMENTO_NV) / 4) / 100f;
        float oldMHP = MaxHP;
        MaxHP = baseHP * var;
        HP = HP / oldMHP * MaxHP;
        Attack = baseAttack * var;
        Defense = baseDefense * var;
        MovSpeed = baseMovSpeed * (0.99f + _nivel / 100f);
        AttSpeed = baseAttSpeed * (0.99f + _nivel / 100f);
    }

    void ShowDamageDeal(int dmg)
    {
        _damageDealTMP.GetComponentInChildren<TextMeshPro>().text = dmg.ToString();
        Instantiate(_damageDealTMP, transform.position, Quaternion.identity);
    }

    void CheckWeatherChange()
    {
        if(_weather.WeatherChanged)
        {
            switch((int)_weather._current)
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

    void SetStatsDay()
    {
        _nivel = Mathf.Max(1, _nivel - _weatherVarNivel);
        _weatherVarNivel = 0;
    }
    void SetStatsNight()
    {
        _nivel = _nivel - _weatherVarNivel + 5;
        _weatherVarNivel = 5;
    }
    void SetStatsStorm()
    {
        if(_nivel - _weatherVarNivel - 5 < 1)
        {
            _weatherVarNivel = _nivel - 1;
            _nivel = 1;
        } else
        {
            _nivel = _nivel - _weatherVarNivel - 5;
            _weatherVarNivel = -5;
        }
    }
    void SetStatsEclipse()
    {
        _nivel = _nivel - _weatherVarNivel + 10;
        _weatherVarNivel = 10;
    }
}
