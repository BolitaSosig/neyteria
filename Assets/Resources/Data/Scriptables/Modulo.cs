using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Módulo", menuName = "Scriptable/Módulo del Ocaso")]
public class Modulo : Item
{
    [Range(1, 3)]
    public int Slots = 1;
    public float Duracion;
    public float TdE;
    public Modulo Upgrade;

    [Range(1, 3)]
    public int Nv = 1;
    public bool override_start;
    public bool override_skill;
    public bool override_cooldown;



    public static void SkillOn(int id, int lvl)
    {
        Skill(true, id, lvl);
    }
    public static void SkillOff(int id, int lvl)
    {
        Skill(false, id, lvl);
    }

    private static void Skill(bool on, int id, int lvl)
    {
        switch (id)
        {
            case 100:
            case 101:
            case 102:
                Supersalto_Skill(on, lvl);
                break;
            case 103:
                Invencibilidad_Skill(on);
                break;
            case 104:
                Aguante_Skill(on);
                break;
            case 105:
            case 106:
            case 107:
                Rumarh_Skill(on, lvl);
                break;
        }
    }

    private static void Supersalto_Skill(bool on, int lvl)
    {
        PlayerStats player = GameObject.Find("Player").GetComponent<PlayerStats>();
        float buff = 0.5f + 0.25f * (lvl - 1);
        player.JumpCap += on ? buff : -buff;
    }

    private static void Invencibilidad_Skill(bool on)
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.Inmune = on;
    }

    private static void Aguante_Skill(bool on)
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.staminaDecrease = !on;
    }

    private static void Rumarh_Skill(bool on, int lvl)
    {
        PlayerStats player = GameObject.Find("Player").GetComponent<PlayerStats>();
        float buff = 0.1f * lvl;
        player.dmgReduc += on ? buff : -buff;
    }
}
