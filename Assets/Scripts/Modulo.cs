using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo : MonoBehaviour
{
    [SerializeField] protected PlayerController player;
    [SerializeField] protected static int ID = 0;
    [SerializeField] protected static int Slots = 0;
    [SerializeField] protected static float Duracion = 0;
    [SerializeField] protected static float TdE = 0;

    private Modulo[] Modulos;

    public Modulo(int id, int slots, float duracion, float tde)
    {
        ID = id;
        Slots = slots;
        Duracion = duracion;
        TdE = tde;
    }

    // Start is called before the first frame update
    void Start()
    {
        Modulos = new Modulo[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enable(int id)
    {
        switch (id)
        {
            case 1: // SUPER SALTO
                if(Supersalto.cd == 0) StartCoroutine(SupersaltoSkill());
                break;
        }
    }

    IEnumerator SupersaltoSkill()
    {
        player.JumpCap *= 1.5f;
        StartCoroutine(Cooldown(Supersalto.cd, Supersalto.TdE));
        yield return new WaitForSecondsRealtime(Supersalto.Duracion);
        player.JumpCap /= 1.5f;
    }

    static IEnumerator Cooldown(float cd, float TdE)
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
