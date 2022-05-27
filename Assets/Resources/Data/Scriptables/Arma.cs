using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arma", menuName = "Scriptable/Arma")]
public class Arma : Item
{
    public Tipo tipo;
    public float ataque;
    public float peso;

    public enum Tipo
    {
        Espada,
        Maza,
        Laser
    }
}
