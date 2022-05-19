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
    // RESOURCES
    public TextMeshProUGUI _degiterioCount;
    public TextMeshProUGUI _occaterioCount;
    // ITEM INFO
    public TextMeshProUGUI _itemName;
    public Image _itemIcon;
    public TextMeshProUGUI _itemRareza;
    public TextMeshProUGUI _itemCantidad;
    public TextMeshProUGUI _itemDescripcion;

    [SerializeField]
    public bool isShown
    {
        set
        {
            GameObject.Find("Player").GetComponent<PlayerController>().canMove = !value;
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
        if (Input.GetKeyDown(KeyCode.F))
            isShown = !isShown;
        else if (Input.GetKeyDown(KeyCode.Escape) && isShown)
            isShown = false;

    }

    void FillItems()
    {
        DeleteItems();
        foreach((Item i, int c) in _items.getAllItemsCant())
        {
            if (i.visible)
            {
                GameObject go = Instantiate(_itemPrefab);
                go.GetComponentsInChildren<Image>()[0].sprite = i.icono;
                go./*transform.Find("CantBack").*/GetComponentInChildren<TextMeshProUGUI>().text = c.ToString();
                go.transform.parent = _viewportContent.transform;
                go.name = "Item " + i.ID.ToString();
            }
            if (i.Equals(PlayerItems.DEGITERIO))
                _degiterioCount.text = c.ToString();
            if (i.Equals(PlayerItems.OCCATERIO))
                _occaterioCount.text = c.ToString();
        }
    }

    void DeleteItems()
    {
        foreach(Transform go in _viewportContent.transform)
        {
            Destroy(go.gameObject);
        }
    }

    public void SelectItem()
    {
        
    }
}
