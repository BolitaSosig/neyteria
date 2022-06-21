using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatProjectileScript : MonoBehaviour
{
    private float BULLET_DMG = 1.75f;

    public float gunDMG = 1f;

    public float speed = 10f;

    private GameObject objetoColisionado;

    private Transform player;
    private Vector2 target;

    public bool follow = false;
    public float FollowRate = 0.5f;
    public float alpha = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        Vector2 direction = target - new Vector2(this.transform.position.x, this.transform.position.y);

        GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(direction, 1) * speed;

        if (follow) StartCoroutine(Following());

    }

    private void Update()
    {
        /*if (follow) {
            Vector2 direction2 = new Vector2(player.position.x, player.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
            GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(direction2, 1) * speed;
        }*/

        //if (follow) transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        //else transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //if (transform.position.x == target.x && transform.position.y == target.y) { }
    }

    public IEnumerator Following()
    {
        GetComponent<SpriteRenderer>().color = Color.magenta;
        while (follow)
        {
            Vector2 direction2 = new Vector2(player.position.x, player.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
            GetComponent<Rigidbody2D>().velocity = (Vector2.ClampMagnitude(direction2, 1) * speed) * (1-alpha) + GetComponent<Rigidbody2D>().velocity * alpha;

            yield return new WaitForSecondsRealtime(FollowRate);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("colision con Player"); return;
            other.SendMessage("GetDamageByEnemy", gunDMG * BULLET_DMG);

            //Debug.Log("colision con Enemy"); 
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            //other.gameObject.GetComponent<EnemyController>().TakeDamage;
            //Debug.Log(other.GetComponent<Enemy1Controller>());


        }
        else if(other.CompareTag("ZoneLoader"))
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
}