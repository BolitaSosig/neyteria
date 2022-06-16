using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private const float HP_UP = 10;
    private const float STAMINA_UP = 10;
    private const float ATTACK_UP = 1;
    private const float DEFENSE_UP = 10;
    private const float WEIGHT_UP = -0.05f;

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

    [Space]
    public int UpgradeCost = 1000;
    [Range(0, 10)]
    public int HpNv = 0;
    [Range(0, 10)]
    public int StaminaNv = 0;
    [Range(0, 10)]
    public int AttackNv = 0;
    [Range(0, 10)]
    public int DefenseNv = 0;
    [Range(0, 10)]
    public int WeightNv = 0;

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
        UpgradeCost = 1000 + (HpNv + StaminaNv + AttackNv + DefenseNv + WeightNv) * 100;

        _pc.MaxHP = (HP_UP * HpNv + BaseHP) * AumHP + AdicHP;
        _pc.MaxStamina = (STAMINA_UP * StaminaNv +  BaseStamina) * AumStamina + AdicStamina;
        _pc.Attack = (ATTACK_UP * AttackNv + BaseAttack) * AumAttack + AdicAttack;
        _pc.Defense = (DEFENSE_UP * DefenseNv + BaseDefense) * AumDefense + AdicDefense;
        _pc.Weight = BaseWeight * (AumWeight + WEIGHT_UP * WeightNv) + AdicWeight;
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

    public void UpgradeStat(GameObject go)
    {
        string msg = "¿Quieres consumir Degiterio x" + UpgradeCost + " para aumentar el nivel ";
        switch(go.name)
        {
            case "HP":
                if (HpNv == 10) return;
                msg += "de la salud a " + (HpNv + 1) + "?";
                break;
            case "Stamina":
                if (StaminaNv == 10) return;
                msg += "de la resistencia a " + (StaminaNv + 1) + "?";
                break;
            case "Attack":
                if (AttackNv == 10) return;
                msg += "del ataque a " + (AttackNv + 1) + "?";
                break;
            case "Defense":
                if (DefenseNv == 10) return;
                msg += "de la defensa a " + (DefenseNv + 1) + "?";
                break;
            case "Weight":
                if (WeightNv == 10) return;
                msg += "del peso a " + (WeightNv + 1) + "?";
                break;
            default:
                return;
        }
        StartCoroutine(Upgrade(msg, go.name));
    }

    IEnumerator Upgrade(string msg, string stat)
    {
        WarningMessage.Show(msg);
        yield return new WaitUntil(() => WarningMessage.answerReady);
        bool cond = WarningMessage.GetAnswer;

        PlayerItems items = _pc.GetComponent<PlayerItems>();
        int cant = items.getByID(1).cant;

        if(cant < UpgradeCost)
        {
            WarningMessage.Show("No hay suficientes Degiterio. Tienes x" + cant + ", cuando se necesitan x" + UpgradeCost);
            yield return new WaitUntil(() => WarningMessage.answerReady);
            cond = WarningMessage.GetAnswer;
        } else if (cond)
        {
            items.Remove(Item.DEGITERIO, UpgradeCost);
            switch (stat)
            {
                case "HP":
                    HpNv++;
                    break;
                case "Stamina":
                    StaminaNv++;
                    break;
                case "Attack":
                    AttackNv++;
                    break;
                case "Defense":
                    DefenseNv++;
                    break;
                case "Weight":
                    WeightNv++;
                    break;
            }
        }
        
        yield return null;
    }
}
