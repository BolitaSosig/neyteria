using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Scriptable/Shop")]
public class Shop : ScriptableObject
{
    public List<Item> item;
    public List<int> cantidad;
    public List<Item> costeItem;
    public List<int> costeCant;
}
