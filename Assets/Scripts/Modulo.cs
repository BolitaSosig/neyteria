using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo : MonoBehaviour
{
    [SerializeField] protected static int ID = 0;
    [SerializeField] protected static int Slots = 0;
    [SerializeField] protected static float Duracion = 0;
    [SerializeField] protected static float TdE = 0;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
