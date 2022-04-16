using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supersalto : MonoBehaviour
{
    private PlayerController player;
    public float cd = 0;
    public int slot = 1;
    public int lvl = 1;
    public const int ID = 1;
    public const int Slots = 1;
    public const float Duracion = 15f;
    public const float TdE = 30f;

    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
    }

    public void Launch()
    {
        if (cd == 0)
            StartCoroutine(Skill());
    }

    public IEnumerator Skill()
    {
        player.JumpCap += 0.5f + 0.25f * (lvl - 1);
        StartCoroutine(Cooldown());
        yield return new WaitForSecondsRealtime(Duracion);
        if (player.noCD) cd = 0;
        player.JumpCap -= 0.5f + 0.25f * (lvl - 1);
    }
    public IEnumerator Cooldown()
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
