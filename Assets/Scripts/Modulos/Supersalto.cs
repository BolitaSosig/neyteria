using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supersalto : Modulo
{
    [SerializeField] private float cd = 0;

    public Supersalto() : base(ID, Slots, Duracion, TdE)
    {
        ID = 1;
        Slots = 1;
        Duracion = 15f;
        TdE = 30f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cd == 0 && Input.GetKeyDown(KeyCode.Alpha1))
            StartCoroutine(Habilidad());
    }

    IEnumerator Habilidad()
    {
        gameObject.GetComponent<PlayerController>().JumpCap *= 1.5f;
        StartCoroutine(Cooldown());
        yield return new WaitForSecondsRealtime(Duracion);
        gameObject.GetComponent<PlayerController>().JumpCap /= 1.5f;
    }

    IEnumerator Cooldown()
    {
        cd = TdE;
        while(cd > 0)
        {
            cd -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        cd = 0;
    }
}
