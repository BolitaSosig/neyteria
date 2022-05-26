using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuministradorCond : Suministrador
{
    protected override void Unlock()
    {
        SendMessage(gameObject.name.Replace(" ",""));
    }

    public IEnumerator Suministrador5()
    {
        if (GameObject.FindWithTag("Nivel1_Enemigos1").transform.childCount == 0)
            base.Unlock();
        else
        {
            GameObject.Find("DialogoSuministrador5").GetComponent<BoxCollider2D>().enabled = true;
            yield return new WaitForSecondsRealtime(0.1f);
            GameObject.Find("DialogoSuministrador5").GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public IEnumerator Suministrador8()
    {
        if (true)
            base.Unlock();
        else
        {
            GameObject.Find("DialogoSuministrador8").GetComponent<BoxCollider2D>().enabled = true;
            yield return new WaitForSecondsRealtime(0.1f);
            GameObject.Find("DialogoSuministrador8").GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
