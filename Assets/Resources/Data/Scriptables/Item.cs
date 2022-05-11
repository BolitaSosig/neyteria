using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "Scriptable/Item")]
public class Item : ScriptableObject
{
    public int ID;
    public Sprite icono;
    [Range(1, 5)]
    public int rareza;
    public bool usable;
    public int maxCant = 999;
    public string nombre;
    [TextArea(3, 10)]
    public string descripcion;
}
