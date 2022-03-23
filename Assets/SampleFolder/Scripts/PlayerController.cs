using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // CONSTANTES
    private const float SPEED_MOV = 7f;
    private const float JUMP_FORCE = 15f;

    // ATRIBUTOS PERSONAJE
    [SerializeField] private float HP = 100f;
    [SerializeField] private float Stamina = 100f;
    [SerializeField] private float Attack = 1f;
    [SerializeField] private float Defense = 1f;
    [SerializeField] private float Weight = 1f;
    [SerializeField] private float MovSpeed = 1f;
    [SerializeField] private float JumpCap = 1f;

    // REFERENCIAS
    private Rigidbody2D _Rigidbody2D;

    private bool onAir
    {
        get { return _Rigidbody2D.velocity.y != 0; }
    }

    void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Moverse();
    }

    void Moverse()
    {
        // HORIZONTAL
        _Rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * SPEED_MOV * MovSpeed, _Rigidbody2D.velocity.y);

        // SALTO
        if(Input.GetAxisRaw("Jump") == 1 && !onAir) 
            _Rigidbody2D.AddForce(Vector2.up * JUMP_FORCE * Mathf.Sqrt(JumpCap), ForceMode2D.Impulse);
    }
}
