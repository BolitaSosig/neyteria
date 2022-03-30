using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // CONSTANTES
    private const float SPEED_MOV = 5f;
    private const float JUMP_FORCE = 15f;
    private const float DASH_FORCE = 12f;
    private const float DMG_CD = 0.5f;
    private const float DASH_RANGE = 0.1f;
    private const float STAMINA_REC_SPEED = 30f;

    // ATRIBUTOS PERSONAJE
    [SerializeField] public float HP = 100f;                    // Puntos de salud reales
    [SerializeField] public float MaxHP = 100f;                 // Puntos de salud m�ximos
    [SerializeField] public float Stamina = 100f;               // Puntos de resistencia reales
    [SerializeField] public float MaxStamina = 100f;            // Puntos de resistencia m�xima
    [SerializeField] public float Attack = 1f;                  // Ataque
    [SerializeField] public float Defense = 1f;                 // Defensa
    [SerializeField] public float Weight = 1f;                  // Peso
    [SerializeField] public float MovSpeed = 1f;                // Velocidad con la que se desplaza el personaje
    [SerializeField] public float JumpCap = 1f;                 // Altura que se alcanza con el salto
    [SerializeField] public float DashRange = 1f;               // Intervalo de invulnerabilidad al evadir
    [SerializeField] public float gastoDash = 25f;              // Gasto de resistencia al evadir
    [SerializeField] public float StaminaVelRec = 1f;           // Velocidad con la que se recupera la resistencia
    [SerializeField] public bool Inmune = false;                // Inmortal

    // REFERENCIAS
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;

    // AUXILIARES

    // FLAGS
    private bool staminaIsUsed = false;

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
        get { return _animator.GetBool("can_dash") && Stamina >= gastoDash; }
        set { _animator.SetBool("can_dash", value); }
    }

    /* Flag que devuelve True si el personaje est� en el aire. */
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
        Walk();
        Jump();
        Dash();
    }

    ////// HORIZONTAL //////
    void Walk()
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
            transform.localScale = new Vector2(1, transform.localScale.y); // mira a la derecha
        else if (Input.GetAxisRaw("Horizontal") == -1)
            transform.localScale = new Vector2(-1, transform.localScale.y); // mira a la izquerda

        if (canDash || onAir)
            _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * SPEED_MOV * MovSpeed, _rigidbody2D.velocity.y); // desplazamiento del personaje
        _animator.SetFloat("velocity_x", Mathf.Abs(_rigidbody2D.velocity.x)); // establece velocity_x en el animator
    }

    ////// SALTO //////
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && grounded) // comprueba que puede saltar
        {
            _rigidbody2D.AddForce(Vector2.up * JUMP_FORCE * Mathf.Sqrt(JumpCap), ForceMode2D.Impulse); // impulsa al personaje hacia arriba
        }

        _animator.SetBool("on_air", !grounded);
    }

    ////// EVASION //////
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) /*Input.GetAxisRaw("Dash") == 1*/ && canDash) // comprueba que puede dashear
        {
            //_rigidbody2D.AddForce(new Vector2(transform.localScale.x, 0) * DASH_FORCE, ForceMode2D.Impulse); // impulsa hacia donde mire el personaje para dashear.
            _rigidbody2D.velocity = new Vector2(DASH_FORCE * transform.localScale.x, _rigidbody2D.velocity.y);
            canDash = false;
            StartCoroutine(GastarStamina(gastoDash)); // consume resistencia al evadir
        }
    }

    public IEnumerator GetDamage(float dmg)
    {
        if (!Inmune)
        {
            HP = Mathf.Max(0, HP - dmg);
            Inmune = true;
            //#### FALTA A�ADIR TEMPORIZADOR DE DMG_CD SEGUNDOS ####//
            yield return new WaitForSecondsRealtime(DMG_CD);
            Inmune = false;
        }
    }

    IEnumerator RecuperacionStamina()
    {
        while (Stamina < MaxStamina && !staminaIsUsed)
        {
            Stamina = Mathf.Min(MaxStamina, Stamina + Time.fixedDeltaTime * STAMINA_REC_SPEED);
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }
    }

    IEnumerator GastarStamina(float cant)
    {
        staminaIsUsed = true;
        Stamina = Mathf.Max(0, Stamina - cant);
        yield return new WaitForSecondsRealtime(0.5f);
        staminaIsUsed = false;
        StartCoroutine(RecuperacionStamina());
    }

}
