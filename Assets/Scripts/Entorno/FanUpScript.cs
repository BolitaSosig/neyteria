using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanUpScript : MonoBehaviour
{
    GameObject player;

    public float Force = 10;
    public float ForceRate = 0.1f;
    public bool mover = false;
    public bool letMove = true;
    public bool activated = true;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mover = true;
            if (!letMove && activated) { player.GetComponent<PlayerController>().canMove = false; }
            StartCoroutine(DoMove());
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mover = false;
            if (!letMove && activated) { player.GetComponent<PlayerController>().canMove = true; }
            StopCoroutine(DoMove());
        }
    }

    public void InvertActivatedDesactivated()
    {
        activated = !activated;
    }

    public IEnumerator DoMove()
    {
        while (mover && activated)
        {
            //player.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * Force, player.GetComponent<Rigidbody2D>().velocity.y - 0.01f);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x - 0.01f, transform.localScale.y * Force);
            yield return new WaitForSecondsRealtime(ForceRate);
        }
    }
}
