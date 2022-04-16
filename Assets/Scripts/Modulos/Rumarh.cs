using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rumarh : MonoBehaviour
{
    private PlayerController player;
    public float cd = 0;
    public int slot = 1;
    public int lvl = 1;
    public const int ID = 4;
    public const int Slots = 1;
    public const float Duracion = 10f;
    public const float TdE = 25f;

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
        player.dmgReduc += 0.2f * lvl;
        StartCoroutine(Cooldown());
        yield return new WaitForSecondsRealtime(Duracion);
        if (player.noCD) cd = 0;
        player.dmgReduc -= 0.2f * lvl;
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
