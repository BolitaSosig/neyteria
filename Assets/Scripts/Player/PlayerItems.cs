using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public List<(Item item, int cant)> items;



    public void AddItem(Item i, int c)
    {
        items.Add((i, c));
    }

    public void RemoveItem(Item i, int c)
    {
        items.Remove((i, c));
    }
}
