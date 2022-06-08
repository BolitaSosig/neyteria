using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOrbController : MonoBehaviour
{
    public SpriteRenderer icon;
    public SpriteRenderer back;
    public Item item;
    public int cantidad;

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
        Launch();
    }

    void Launch()
    {
        float angle = Random.Range(-0.349f, 0.349f);
        float force = Random.Range(100f, 300f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle)) * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(PickupItem(collision.gameObject.GetComponent<PlayerItems>()));
    }

    IEnumerator PickupItem(PlayerItems pi)
    {
        GetComponent<CircleCollider2D>().enabled = false;
        pi.Add(item, cantidad);
        GetComponent<Animator>().SetBool("picked", true);
        yield return new WaitForSecondsRealtime(0.7f);
        Destroy(gameObject);
    }
}
