using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanUpScript : MonoBehaviour
{
    GameObject player;

    public float Force = 30;
    public float ForceRate = 0.005f;
    public bool mover = false;
    public bool letMove = false;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mover = true;
            if (!letMove) { player.GetComponent<PlayerController>().canMove = false; }
            StartCoroutine(DoMove());
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mover = false;
            if (!letMove) { player.GetComponent<PlayerController>().canMove = true; }
            StopCoroutine(DoMove());
        }
    }


    public IEnumerator DoMove()
    {
        while (mover)
        {
            //player.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * Force, player.GetComponent<Rigidbody2D>().velocity.y - 0.01f);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, transform.localScale.y * Force);
            yield return new WaitForSecondsRealtime(ForceRate);
        }
    }
}
