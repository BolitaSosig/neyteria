using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject objetoColisionado;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("colision con Player"); return;   
        }
        if (other.CompareTag("Enemy"))
        {
            //other.gameObject.GetComponent<EnemyController>().TakeDamage;
            Debug.Log("colision con Enemy"); Destroy(gameObject);
        }
        else {
            Debug.Log("colision con algo"); Destroy(gameObject);
        }
    }
}
