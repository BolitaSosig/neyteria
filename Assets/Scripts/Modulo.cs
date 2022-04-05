using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo : MonoBehaviour
{
    [SerializeField] protected PlayerController player;

    public int[] ID = new int[3];
    public float[] CD = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        ID[0] = 1;
        EquipModule(ID[0], 0); // Supersalto
        ID[1] = 2;
        EquipModule(ID[1], 1); // Invencibilidad
        ID[2] = 3;
        EquipModule(ID[2], 2); // Vigor
    }

    void EquipModule(int id, int slot)
    {
        GameObject obj = player.gameObject;
        System.Type type = null;
        switch (id)
        {
            case 1:
                type = typeof(Supersalto);
                break;
            case 2:
                type = typeof(Invencibilidad);
                break;
            case 3:
                type = typeof(Vigor);
                break;
        }

        if (obj.GetComponent(type) == null)
        {
            obj.AddComponent(type);
            player.Modulos[slot] = obj.GetComponent(type);
        }
    }

}
