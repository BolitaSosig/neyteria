using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemigo
{

    override protected void Start()
    {
        StartReferences();
        baseHP = 51f;
        baseAttack = 4f;
        baseDefense = 1f;
        baseMovSpeed = 1f;
        baseAttSpeed = 1f;
    }
}
