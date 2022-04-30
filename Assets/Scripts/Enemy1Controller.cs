using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy1Controller : MonoBehaviour
{
    // CONSTANTES
    private const float SPEED_MOV = 2f;
    private const float JUMP_FORCE = 15f;
    private float DISTANCIA_EN_SEGUNDOS = 3f;

    // ATRIBUTOS PERSONAJE
    [SerializeField] private int _nivel = 1;
    private int _oldNivel = 0;
    [SerializeField] private float HP = 50f;
    [SerializeField] private float MaxHP = 100f;
    [SerializeField] private float Stamina = 100f;
    [SerializeField] private float Attack = 4f;
    [SerializeField] private float Defense = 1f;
    [SerializeField] private float Weight = 1f;
    [SerializeField] private float MovSpeed = 1f;
    [SerializeField] private float AttSpeed = 1f;
    [SerializeField] private float JumpCap = 1f;
    [SerializeField] private float mov_x = 0f;
    [SerializeField] private int cont_mov_x = 0;

    private float distReco = 0f;
    private bool moving = false;

    // REFERENCIAS
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Transform _canvasTranform;

    // Barra de vida
    [SerializeField] public Transform healthBarEnemy;

    public float baseHP;
    public float baseAttack;
    public float baseDefense;
    public float baseMovSpeed;
    public float baseAttSpeed;

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
    }


    void Update()
    {
        if (levelHasChanged)
            Nivel = _nivel;
        StartCoroutine(Moverse());
        HealthBarUpdate();
    }

    IEnumerator Moverse()
    {
        if(!moving)
        {
            moving = true;
            float cont = 0;
            while (cont < DISTANCIA_EN_SEGUNDOS * 10f/MovSpeed)
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
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
         
    }

    ////// RECIBE DAÑO //////
    public void GetDamage(float dmg)
    {
        HP = Mathf.Max(0, HP - dmg);
        if (HP <= 0) StartCoroutine(Die());
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

}
