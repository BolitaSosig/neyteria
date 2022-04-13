using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigor : MonoBehaviour
{
    private PlayerController player;
    public float cd = 0;
    public int slot = 3;
    public const int ID = 3;
    public const int Slots = 1;
    public const float Duracion = 7f;
    public const float TdE = 45f;

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
        player.staminaDecrease = false;
        StartCoroutine(Cooldown());
        yield return new WaitForSecondsRealtime(Duracion);
        if (player.noCD) cd = 0;
        player.staminaDecrease = true;
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
