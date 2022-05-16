using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public PlayerItems _items;
    public GameObject _itemPrefab;
    public GameObject _viewportContent;

    [SerializeField]
    public bool isShown
    {
        set
        {
            GetComponent<Animator>().SetBool("show", value);
        }
        get
        {
            return GetComponent<Animator>().GetBool("show");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _items = GameObject.Find("Player").GetComponent<PlayerItems>();
        _items._inventoryController = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            isShown = !isShown;
        }
    }

    void FillItems()
    {
        Debug.Log("FillItems");
        DeleteItems();
        foreach((Item i, int c) in _items.getAllItemsCant())
        {
            GameObject go = Instantiate(_itemPrefab);
            go.GetComponentsInChildren<Image>()[0].sprite = i.icono;
            go./*transform.Find("CantBack").*/GetComponentInChildren<TextMeshProUGUI>().text = c.ToString();
            go.transform.parent = _viewportContent.transform;
        }
    }

    void DeleteItems()
    {
        foreach(Transform go in _viewportContent.transform)
        {
            Destroy(go.gameObject);
        }
    }
}