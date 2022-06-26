using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemObtenido : MonoBehaviour
{
    [SerializeField] private GameObject[] obj = new GameObject[4];
    Queue<(Item, int)> itemQueue = new Queue<(Item, int)>();


    void SetShowing(int i, bool value)
    {
        obj[i].GetComponent<Animator>().SetBool("show", value);
    }
    bool GetShowing(int i)
    {
        return obj[i].GetComponent<Animator>().GetBool("show");
    }

    bool NoOneShowing()
    {
        return !GetShowing(0) && !GetShowing(1) && !GetShowing(2) && !GetShowing(3);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(itemQueue.Count > 0 && NoOneShowing())
        {
            for(int i = 0; i < obj.Length && itemQueue.Count > 0; i++)
            {
                if(!GetShowing(i))
                {
                    (Item item, int cant) pair = itemQueue.Dequeue();
                    StartCoroutine(ShowItemTimer(i, pair.item, pair.cant));
                }
            }
        }
    }


    public void ShowItem(Item item, int cant)
    {
        itemQueue.Enqueue((item, cant));
    }

    IEnumerator ShowItemTimer(int i, Item item, int cant)
    {
        SetShowing(i, true);
        yield return new WaitForSecondsRealtime(0.2f);
        obj[i].GetComponentsInChildren<Image>()[1].sprite = item.icono;
        obj[i].GetComponentInChildren<TextMeshProUGUI>().text = "x" + cant + " " + item.nombre;
        obj[i].GetComponent<Image>().color = Color.Lerp(Item.GetRarezaColor(item.rareza), new Color(), 0.5f);
        yield return new WaitForSecondsRealtime(3f);
        SetShowing(i, false);
    }
}
