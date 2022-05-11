using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    private Dictionary<Item, int> items = new Dictionary<Item, int>();
    public bool printItems = false;


    public void Start()
    {
        Add(Resources.Load<Item>("Data\\Item\\Degiterio"), 0);
        Add(Resources.Load<Item>("Data\\Item\\Occaterio"), 0);
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
