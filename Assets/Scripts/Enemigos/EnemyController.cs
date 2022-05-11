using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // CONSTANTES
    private const float SPEED_MOV = 2f;
    private const float JUMP_FORCE = 15f;
    private float DISTANCIA_EN_SEGUNDOS = 3f;

    // ATRIBUTOS PERSONAJE
    [SerializeField] private int _nivel = 1;
    [SerializeField] private float HP = 100f;
    [SerializeField] private float MaxHP = 100f;
    [SerializeField] private float Stamina = 100f;
    [SerializeField] private float Attack = 4f;
    [SerializeField] private float Defense = 1f;
    [SerializeField] private float MovSpeed = 1f;
    [SerializeField] private float AttSpeed = 1f;
    [SerializeField] private float JumpCap = 1f;
    [SerializeField] private float mov_x = 0f;
    [SerializeField] private int cont_mov_x = 0;

    public float baseHP;
    public float baseAttack;
    public float baseDefense;
    public float baseMovSpeed;
    public float baseAttSpeed;

    public int Nivel { 
        get { return _nivel; }
        set
        {
            _nivel = value;
            UpdateStats();
        }
    }

    private float distReco = 0f;
    private bool moving = false;

    // REFERENCIAS
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;

    // Barra de vida
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] public GameObject healthBarEnemy;
    [SerializeField] public Transform pivotHealthBarEnemy;
    [SerializeField] private float distancia = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public bool onAir
    {
        get { return _rigidbody2D.velocity.y != 0 /*&& !grounded*/; }
    }

    void DoDamage(GameObject player)
    {
        player.GetComponent<PlayerController>().GetDamageByEnemy(Attack);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            DoDamage(collision.gameObject);
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
        float var = 1 + (Nivel * (Nivel - 1) * GLOBAL.AUMENTO_NV) / 4;
        float oldMHP = MaxHP;
        MaxHP = baseHP * var;
        HP = HP / oldMHP * MaxHP;
        Attack = baseAttack * var;
        Defense = baseDefense * var;
        MovSpeed = baseMovSpeed * var;
        AttSpeed = baseAttSpeed * var;
    }
}
