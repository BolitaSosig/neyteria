using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public static Item NONE = Resources.Load<Item>("Data\\Item\\NONE");
    public static Item DEGITERIO = Resources.Load<Item>("Data\\Item\\Degiterio");
    public static Item OCCATERIO = Resources.Load<Item>("Data\\Item\\Occaterio");
    public static Item SUERO_VITAL = Resources.Load<Item>("Data\\Item\\Suero_vital");
    public static Item SUERO_ENERGETICO = Resources.Load<Item>("Data\\Item\\Suero_energetico");
    public static Item SUERO_FORTALECEDOR = Resources.Load<Item>("Data\\Item\\Suero_fortalecedor");
    public static Item SUERO_PROTECTOR = Resources.Load<Item>("Data\\Item\\Suero_protector");
    public static Item MINERAL_FRAGMENTADO = Resources.Load<Item>("Data\\Item\\Mineral_fragmentado");
    public static Item NEXOTEK = Resources.Load<Item>("Data\\Item\\Nexotek");

    private Dictionary<Item, int> items = new Dictionary<Item, int>();
    public bool printItems = false;


    public void Start()
    {
        Add(DEGITERIO, 0);
        Add(OCCATERIO, 0);
    }

    public void Update()
    {
        
    }

    public void Add(Item i, int c)
    {
        if(items.ContainsKey(i))
        {
            items[i] = Mathf.Min(c + items[i], i.maxCant);
        } else
        {
            items.Add(i, c);
        }
    }

    public void Remove(Item i, int c)
    {
        if(items.ContainsKey(i))
        {
            int cant = items[i] - c;
            if (cant >= 0)
                items[i] = cant;
        }
    }

    public void Remove(Item i)
    {
        items.Remove(i);
    }

    public void ShowConsole()
    {
        string res = "\n---------------------\nPlayer Items:\n";
        foreach(Item item in items.Keys)
        {
            res += "  " + item.nombre + " x" + items[item] + "\n";
        }
        res += "---------------------\n";
        Debug.Log(res);
    }
}
