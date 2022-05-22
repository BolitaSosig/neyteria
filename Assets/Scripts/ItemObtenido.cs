using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemObtenido : MonoBehaviour
{
    bool _showing = false;
    Queue<(Item, int)> itemQueue = new Queue<(Item, int)>();

    bool Showing
    {
        get { return GetComponent<Animator>().GetBool("show"); }
        set { GetComponent<Animator>().SetBool("show", value); }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(itemQueue.Count > 0)
        {
            if (!Showing)
            {
                (Item item, int cant) pair = itemQueue.Dequeue();
                StartCoroutine(ShowItemTimer(pair.item, pair.cant));
            }
        }
    }


    public void ShowItem(Item item, int cant)
    {
        itemQueue.Enqueue((item, cant));
    }

    IEnumerator ShowItemTimer(Item item, int cant)
    {
        Showing = true;
        yield return new WaitForSecondsRealtime(0.2f);
        GetComponentsInChildren<Image>()[1].sprite = item.icono;
        GetComponentInChildren<TextMeshProUGUI>().text = "x" + cant + " " + item.nombre;
        yield return new WaitForSecondsRealtime(3f);
        Showing = false;
    }
}
