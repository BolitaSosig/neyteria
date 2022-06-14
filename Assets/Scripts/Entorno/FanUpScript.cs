using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanUpScript : MonoBehaviour
{
    GameObject player;

    Animator anim;

    public float Force = 10;
    public float ForceRate = 0.1f;
    public bool mover = false;
    public bool letMove = true;
    public bool activated = true;

    private GameObject wind1, wind2, wind3;

    private void Start()
    {
        player = GameObject.Find("Player");
        anim = gameObject.GetComponent<Animator>();

        
        
        wind1 = GetChildWithName(gameObject, "Wind");
        wind2 = GetChildWithName(gameObject, "Wind (1)");
        wind3 = GetChildWithName(gameObject, "Wind (2)");

        
        
        if (!activated) { wind1.SetActive(false); wind2.SetActive(false); wind3.SetActive(false); }

        anim.SetBool("Activated", activated);
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

        wind1.SetActive(activated); wind2.SetActive(activated); wind3.SetActive(activated);

        anim.SetBool("Activated", activated);
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


    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
}
