using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // CONSTANTES
    private const float SPEED_MOV = 5f;
    private const float JUMP_FORCE = 15f;
    private const float DASH_FORCE = 12f;
    private const float DMG_CD = 1f;

    // ATRIBUTOS PERSONAJE
    [SerializeField] private float HP = 100f;
    [SerializeField] private float Stamina = 100f;
    [SerializeField] private float Attack = 1f;
    [SerializeField] private float Defense = 1f;
    [SerializeField] private float Weight = 1f;
    [SerializeField] private float MovSpeed = 1f;
    [SerializeField] private float JumpCap = 1f;
    [SerializeField] private float DashRange = 0.5f;
    [SerializeField] private bool Inmune = false;

    // REFERENCIAS
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;


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
        get { return _animator.GetBool("can_dash"); }
        set { _animator.SetBool("can_dash", value); }
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
        Moverse();

        // test damage
        if (Input.GetKeyDown(KeyCode.K))
            GetDamage(5f);
    }

    void Moverse()
    {
        ////// HORIZONTAL //////
        if(Input.GetAxisRaw("Horizontal") == 1) 
            transform.localScale = new Vector2(1, transform.localScale.y); // mira a la derecha
        else if (Input.GetAxisRaw("Horizontal") == -1) 
            transform.localScale = new Vector2(-1, transform.localScale.y); // mira a la izquerda

        if(canDash)
            _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * SPEED_MOV * MovSpeed, _rigidbody2D.velocity.y); // desplazamiento del personaje
        _animator.SetFloat("velocity_x", Mathf.Abs(_rigidbody2D.velocity.x)); // establece velocity_x en el animator


        ////// SALTO //////
        if (Input.GetKeyDown(KeyCode.W) && grounded) // comprueba que puede saltar
        {
            _rigidbody2D.AddForce(Vector2.up * JUMP_FORCE * Mathf.Sqrt(JumpCap), ForceMode2D.Impulse); // impulsa al personaje hacia arriba
        }

        _animator.SetBool("on_air", !grounded);


        ////// EVASION //////
        if (Input.GetKeyDown(KeyCode.LeftShift) /*Input.GetAxisRaw("Dash") == 1*/ && canDash) // comprueba que puede dashear
        {
            //_rigidbody2D.AddForce(new Vector2(transform.localScale.x, 0) * DASH_FORCE, ForceMode2D.Impulse); // impulsa hacia donde mire el personaje para dashear.
            _rigidbody2D.velocity = new Vector2(DASH_FORCE * transform.localScale.x, _rigidbody2D.velocity.y);
            canDash = false;
        }
    }


    public void GetDamage(float dmg)
    {
        if (!Inmune)
        {
            HP = Mathf.Max(0, HP - dmg);
            Inmune = true;
            //#### FALTA AÑADIR TEMPORIZADOR DE DMG_CD SEGUNDOS ####//
        }
    }

}
