using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathProjectile : MonoBehaviour
{
    private float BULLET_DMG = 1.75f;

    public float gunDMG = 1f;

    public bool doDMG = false;

    private Collider2D col2D;

    private GameObject objetoColisionado;
    // Start is called before the first frame update
    void Start()
    {
        col2D = GetComponent<Collider2D>();
        StartCoroutine(DoDMG());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("colision con Player"); return;
            other.SendMessage("GetDamageByEnemy", gunDMG * BULLET_DMG); print("Daño a player realizado");

            //Debug.Log("colision con Player"); 
            //Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            //other.gameObject.GetComponent<EnemyController>().TakeDamage;
            //Debug.Log(other.GetComponent<Enemy1Controller>());


        }
        else if (!other.CompareTag("ZoneLoader"))
        {
            //Debug.Log("colision con algo"); 
            //Destroy(gameObject);
        }
        else
        {
            //Debug.Log("colision con algo"); 
            //Destroy(gameObject);
        }
    }


    public IEnumerator DoDMG()
    {
        col2D.enabled = false;
        yield return new WaitForSecondsRealtime(0.3f);
        col2D.enabled = true;
    }
}