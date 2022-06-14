using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexoCentralController : MonoBehaviour
{
    public Nexo nexo;
    public bool discovered;
    public bool teleporting;
    public Canvas canvas;

    private bool colisionando;
    private Animator animator;

    public enum Nexo
    {
        Nexo_1_2,
        Nexo_2_3,
        Nexo_3_3,
        Nexo_4_4
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

        /*if(colisionando && discovered && !teleporting && Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(Teleport());
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colisionando = true;
            canvas.gameObject.SetActive(true);
            collision.GetComponent<PlayerAttack>().canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colisionando = false;
            canvas.gameObject.SetActive(false);
            collision.GetComponent<PlayerAttack>().canAttack = false;
        }
    }

    IEnumerator Teleport()
    {
        Nexo end = Nexo.Nexo_1_2;
        switch(nexo)
        {
            case Nexo.Nexo_1_2:
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow));
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    end = Nexo.Nexo_2_3;
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    end = Nexo.Nexo_4_4;
                break;
            case Nexo.Nexo_2_3:
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow));
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    end = Nexo.Nexo_3_3;
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    end = Nexo.Nexo_1_2;
                break;
            case Nexo.Nexo_3_3:
                end = Nexo.Nexo_2_3;
                break;
            case Nexo.Nexo_4_4:
                end = Nexo.Nexo_1_2;
                break;
        }
        FindObjectOfType<TeleportController>().Teleport(nexo, end);
    }

    void Equipo()
    {
        GameObject.FindObjectOfType<EquipmentController>().IsShow = true;
    }
}
