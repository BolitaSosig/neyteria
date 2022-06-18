using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemPack", menuName = "Scriptable/Item Pack")]
public class ItemPack : ScriptableObject
{
    public Item[] items;
    public int[] cant;
}
