using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // CONSTANTES
    private const float SPEED_MOV = 8f;
    private const float JUMP_FORCE = 18f;
    private const float DASH_FORCE = 12f;
    private const float DMG_CD = 0.5f;
    private const float DASH_RANGE = 0.1f;
    private const float STAMINA_REC_SPEED = 30f;

    // ATRIBUTOS PERSONAJE
    [SerializeField] public float HP = 100f;                    // Puntos de salud reales
    [SerializeField] public float MaxHP = 100f;                 // Puntos de salud máximos
    [SerializeField] public float Stamina = 100f;               // Puntos de resistencia reales
    [SerializeField] public float MaxStamina = 100f;            // Puntos de resistencia máxima
    [SerializeField] public float Attack = 1f;                  // Ataque
    [SerializeField] public float Defense = 1f;                 // Defensa
    [SerializeField] public float Weight = 1f;                  // Peso
    [SerializeField] public float MovSpeed = 1f;                // Velocidad con la que se desplaza el personaje
    [SerializeField] public float JumpCap = 1f;                 // Altura que se alcanza con el salto
    [SerializeField] public float DashRange = 1f;               // Intervalo de invulnerabilidad al evadir
    [SerializeField] public float gastoDash = 25f;              // Gasto de resistencia al evadir
    [SerializeField] public float StaminaVelRec = 1f;           // Velocidad con la que se recupera la resistencia
    [SerializeField] public float dmgReduc = 0f;                // Reducción del daño recibido

    // REFERENCIAS
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    [SerializeField] public Component[] Modulos = new Component[3];
    private GameObject _GLOBAL;
    

    // AUXILIARES

    // FLAGS
    public bool staminaIsUsed = false;
    [SerializeField] public bool Inmune = false;                // Inmortal
    public bool staminaDecrease = true;

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
        _GLOBAL = GameObject.Find("GLOBAL");
    }


    void Update()
    {
        Moverse();
        UseModulo();

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
        /*if (Input.GetAxisRaw("Horizontal") == 1)
            transform.localScale = new Vector2(1, transform.localScale.y); // mira a la derecha
        else if (Input.GetAxisRaw("Horizontal") == -1)
            transform.localScale = new Vector2(-1, transform.localScale.y); // mira a la izquerda*/

        //Mi versión reducida y optimizada (funciona cuando el escalado es distinto de 1)
        if (Input.GetAxisRaw("Horizontal") == 1 && transform.localScale.x < 0 || Input.GetAxisRaw("Horizontal") == -1 && transform.localScale.x > 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // cambia de direccion

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
            int shortDash = _rigidbody2D.velocity.x == 0 ? 1 : 0; // 1 si dashea mientras se esta moviendo, 0 si está quieto
            _rigidbody2D.velocity = new Vector2(DASH_FORCE * transform.localScale.x, _rigidbody2D.velocity.y);
            canDash = false;
            StartCoroutine(GastarStamina(gastoDash - 0.35f * gastoDash * shortDash)); // consume resistencia al evadir
        }
    }

    ////// RECIBE DAÑO //////
    public IEnumerator GetDamage(float dmg)
    {
        if (!Inmune)
        {
            HP = Mathf.Max(0, HP - dmg);
            Inmune = true;

            yield return new WaitForSecondsRealtime(DMG_CD);
            Inmune = false;
        }
    }

    ////// RECIBE DAÑO POR UN ENEMIGO //////
    public void GetDamageByEnemy(float ataqEnemigo)
    {
        StartCoroutine(GetDamage(5 * ataqEnemigo / Mathf.Sqrt(Defense) * (1 - dmgReduc)));
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
        if (staminaDecrease)
        {
            staminaIsUsed = true;
            Stamina = Mathf.Max(0, Stamina - cant);
            yield return new WaitForSecondsRealtime(0.5f);
            staminaIsUsed = false;
            StartCoroutine(RecuperacionStamina());
        }
    }


    void UseModulo()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Modulos[0].GetType().GetMethod("Launch").Invoke(Modulos[0], new object[] { });
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Modulos[1].GetType().GetMethod("Launch").Invoke(Modulos[1], new object[] { });
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            Modulos[2].GetType().GetMethod("Launch").Invoke(Modulos[2], new object[] { });
    }
}
