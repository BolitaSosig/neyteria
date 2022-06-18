using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Scriptable/Shop")]
public class Shop : ScriptableObject
{
    public List<Item> item;
    public List<int> cantidad;
    public List<ItemPack> costeItem;

    public (Item item, int cant, ItemPack cost) GetDataByItem(Item i)
    {
        int index = GetIndexByItem(i);
        return (item[index], cantidad[index], costeItem[index]);
    }

    public int GetIndexByItem(Item i)
    {
        for (int j = 0; j < item.Count; j++)
            if (item[j].Equals(i))
                return j;
        return -1;
    }
}
