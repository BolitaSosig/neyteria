using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexoCentralController : MonoBehaviour
{
    public Nexo nexo;
    public bool discovered;
    public bool teleporting;

    private bool colisionando;
    private Animator animator;

    public enum Nexo
    {
        Nexo_1_2,
        Nexo_2_3,
        Nexo_3_3
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("discovered", discovered);
        animator.SetBool("teleporting", teleporting);

        if(colisionando && discovered && !teleporting && Input.GetKeyDown(KeyCode.T))
        {
            FindObjectOfType<TeleportController>().Teleport(nexo, Nexo.Nexo_2_3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            colisionando = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            colisionando = false;
    }
}
