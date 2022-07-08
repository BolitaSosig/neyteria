using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexoCentralController : MonoBehaviour
{
    public Nexo nexo;
    public bool discovered;
    public bool teleporting;
    public Canvas canvas;
    public Shop tienda;

    private bool colisionando;
    private Animator animator;
    private PlayerInputManager input;

    public enum Nexo
    {
        Nexo_0,
        Nexo_1_2,
        Nexo_2_3,
        Nexo_3_3,
        Nexo_3_4,
        Nexo_4_4
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        input = FindObjectOfType<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("discovered", discovered);
        animator.SetBool("teleporting", teleporting);

        if(colisionando && Input.GetButtonDown("Interact") && !discovered)
        {
            ActivateTp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colisionando = true;
            canvas.gameObject.SetActive(discovered);
            collision.GetComponent<PlayerAttack>().canAttack = false;
            GLOBAL.lastNexo = nexo;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colisionando = false;
            canvas.gameObject.SetActive(false);
            collision.GetComponent<PlayerAttack>().canAttack = true;
            collision.GetComponent<PlayerInputManager>()._isEquipmentNexo = false;
        }
    }

    void ShowMap()
    {
        input.SendMessage("SetMap");
    }

    void Teleport(Nexo end)
    {
        FindObjectOfType<TeleportController>().Teleport(nexo, end);
    }

    void Equipo()
    {
        input.SetEquipmentNexo();
    }

    void Stats()
    {
        input.SendMessage("SetStatsUpgrade");
        //FindObjectOfType<MejoraNexoController>().Show();
    }

    void OpenShop()
    {
        FindObjectOfType<ShopController>().SetShop(tienda);
        input.SendMessage("SetShop");
    }

    void ActivateTp()
    {
        discovered = true;
        canvas.gameObject.SetActive(true);
        var map = FindObjectOfType<MapController>();
        switch(nexo)
        {
            case Nexo.Nexo_1_2:
                map.ActivateTp1();
                break;
            case Nexo.Nexo_2_3:
                map.ActivateTp2();
                break;
            case Nexo.Nexo_3_3:
                map.ActivateTp3();
                break;
            case Nexo.Nexo_3_4:
                map.ActivateTp32();
                break;
            case Nexo.Nexo_4_4:
                map.ActivateTp4();
                break;
        }
    }
}
