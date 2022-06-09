using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ItemOrbController : MonoBehaviour
{
    public SpriteRenderer icon;
    public SpriteRenderer back;
    public Item item;
    public int cantidad;

    private bool launching;

    private void OnEnable()
    {
        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GetComponent<BoxCollider2D>());
    }

    public void Create(Item i, int c)
    {
        item = i;
        cantidad = c;
        icon.sprite = i.icono;
        back.color = Item.GetRarezaColor(i.rareza);
        GetComponentInChildren<Light2D>().color = back.color;
        GetComponentInChildren<ParticleSystem>().startColor = back.color;
        Launch();
    }

    void Launch()
    {
        float angle = Random.Range(-0.349f, 0.349f);
        float force = Random.Range(200f, 400f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle)) * force);
        StartCoroutine(LaunchCounter());
    }

    IEnumerator LaunchCounter()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        launching = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (launching && !GetComponent<Animator>().GetBool("grounded") && collision.gameObject.CompareTag("Plataformas"))
        {
            GetComponent<Animator>().applyRootMotion = false;
            GetComponent<Animator>().SetBool("grounded", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StartCoroutine(PickupItem(collision.gameObject.GetComponent<PlayerItems>()));
    }

    IEnumerator PickupItem(PlayerItems pi)
    {
        GetComponent<CircleCollider2D>().enabled = false;
        pi.Add(item, cantidad);
        GetComponent<Animator>().SetBool("picked", true);
        yield return new WaitForSecondsRealtime(0.7f);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
