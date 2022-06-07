using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // BASE
    public float BaseHP = 100f;                // Puntos de salud máximos
    public float BaseStamina = 100f;           // Puntos de resistencia máxima
    public float BaseAttack = 1f;              // Ataque
    public float BaseDefense = 1f;             // Defensa
    public float BaseWeight = 1f;              // Peso
    public float AumDmg = 1f;                   // Peso
    public float MovSpeed = 1f;                // Velocidad con la que se desplaza el personaje
    public float AttSpeed = 1f;              // Velocidad con la que ataca el personaje
    public float JumpCap = 1f;                 // Altura que se alcanza con el salto
    public float DashRange = 0.7f;               // Intervalo de invulnerabilidad al evadir
    public float gastoDash = 25f;              // Gasto de resistencia al evadir
    public float StaminaVelRec = 1f;           // Velocidad con la que se recupera la resistencia
    public float dmgReduc = 0f;

    [Space]
    // MULTIPLICADORES
    public float AumHP = 1f;
    public float AumStamina = 1f;
    public float AumAttack = 1f;
    public float AumDefense = 1f;
    public float AumWeight = 1f;
    public float AumGastoDash = 1f;

    [Space]
    // ADICIONALES
    public float AdicHP = 0f;
    public float AdicStamina = 0f;
    public float AdicAttack = 0f;
    public float AdicDefense = 0f;
    public float AdicWeight = 0f;
    public float AdicGastoDash = 0f;

    public Dictionary<int, float> buffStorage = new Dictionary<int, float>();

    // REF
    private PlayerController _pc;

    // Start is called before the first frame update
    void Start()
    {
        _pc = GetComponent<PlayerController>();
        UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
    }

    void UpdateStats()
    {
        float mhpaux = _pc.MaxHP;

        _pc.MaxHP = BaseHP * AumHP + AdicHP;
        _pc.MaxStamina = BaseStamina * AumStamina + AdicStamina;
        _pc.Attack = BaseAttack * AumAttack + AdicAttack;
        _pc.Defense = BaseDefense * AumDefense + AdicDefense;
        _pc.Weight = BaseWeight * AumWeight + AdicWeight;
        _pc.AumDmg = AumDmg;
        _pc.MovSpeed = MovSpeed;
        _pc.AttSpeed = AttSpeed;
        _pc.JumpCap = JumpCap;
        _pc.DashRange = DashRange;
        _pc.gastoDash = gastoDash * AumGastoDash + AdicGastoDash;
        _pc.StaminaVelRec = StaminaVelRec;
        _pc.dmgReduc = dmgReduc;

        _pc.HP = _pc.MaxHP * _pc.HP / mhpaux;
    }
}
