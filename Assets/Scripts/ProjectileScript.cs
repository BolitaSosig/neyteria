using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float BULLET_DMG = 1.75f;

    public float gunDMG = 1f;

    private GameObject objetoColisionado;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("colision con Player"); return;
            other.SendMessage("GetDamageByEnemy", gunDMG); //* BULLET_DMG);

            //Debug.Log("colision con Enemy"); 
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            //other.gameObject.GetComponent<EnemyController>().TakeDamage;
            //Debug.Log(other.GetComponent<Enemy1Controller>());


        }
        else if(!other.CompareTag("ZoneLoader"))
        {
            //Debug.Log("colision con algo"); 
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("colision con algo"); 
            Destroy(gameObject);
        }
    }
}