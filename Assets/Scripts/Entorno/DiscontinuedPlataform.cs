using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DiscontinuedPlataform : MonoBehaviour
{

    public bool isTouched = false;
    public float segundos = 6f;
    public float segsDifuminar = 2.5f;
    private Tilemap tm;
    Collider2D col2d;
    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponent<Tilemap>();
        col2d = GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Collider detections

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isTouched)
        { isTouched = true; StartCoroutine(Timing(tm)); }
    }


    /*void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        { isPlayer = false; }
    }*/

    IEnumerator Timing(Tilemap tm)
    {
        if (isTouched)
        {
            while (tm.color.a > 0)                                      //Difuminar
            {
                tm.color -= new Color(0, 0, 0, 0.1f / segsDifuminar);
                yield return new WaitForSeconds(0.1f);
            }


            tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, 0);

            col2d.isTrigger = true;                                     //Quitar collider

            yield return new WaitForSeconds(segundos);                  //Pausa

            while (tm.color.a < 1)                                      //Crear
            {
                tm.color += new Color(0, 0, 0, 0.1f / segsDifuminar);
                yield return new WaitForSeconds(0.1f);
            }

            tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, 1);

            col2d.isTrigger = false;                                    //Poner collider

            isTouched = false;
        }
    }




}
