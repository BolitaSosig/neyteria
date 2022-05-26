using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Módulo", menuName = "Scriptable/Módulo del Ocaso")]
public class Modulo : Item
{
    [Range(1, 3)]
    public int Slots = 1;
    public float Duracion;
    public float TdE;
    public Modulo Upgrade;

    public Object script;
    [Range(1, 3)]
    public int Nv = 1;
    public bool override_start;
    public bool override_skill;
    public bool override_cooldown;

    private MonoBehaviour _script;
    private PlayerController player;
    private float cd = 0;

    void Start()
    {
        _script = (MonoBehaviour)script;
        if (override_start)
            player.SendMessage("Start");
        else
            player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void Launch()
    {
        if (cd == 0)
            _script.SendMessage("Skill", this);
    }

    public IEnumerator Skill()
    {
        //player.JumpCap += 0.5f + 0.25f * (lvl - 1);
        _script.SendMessage("SkillOn", player);
        //StartCoroutine(Cooldown());
        yield return new WaitForSecondsRealtime(Duracion);
        if (player.noCD) cd = 0;
        //player.JumpCap -= 0.5f + 0.25f * (lvl - 1);
        _script.SendMessage("SkillOff", player);
    }
    public IEnumerator Cooldown()
    {
        if (override_cooldown)
            _script.SendMessage("Cooldown");
        else
        {
            cd = TdE;
            while (cd > 0)
            {
                cd -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            cd = 0;
        }
    }
}
