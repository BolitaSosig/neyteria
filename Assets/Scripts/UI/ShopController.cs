using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private PlayerItems _items;
    public Shop _shop;
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
    public TextMeshProUGUI _itemCantInv;
    public TextMeshProUGUI _itemDescripcion;
    public GameObject[] _costes = new GameObject[3];

    public int _selectedItemID;
    private Vector3 startPos;

    [SerializeField]
    public bool isShown
    {
        set
        {
            GameObject.Find("Player").GetComponent<PlayerController>().canMove = !value;
            transform.position = value ? startPos : startPos - new Vector3(0, 1000);
        }
        get
        {
            return transform.position.Equals(startPos);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _items = FindObjectOfType<PlayerItems>();
        startPos = transform.position;
        transform.position = startPos - new Vector3(0, 1000);

        _itemName.text = "";
        _itemDescripcion.text = "";
        _itemRareza.text = "";
        _itemCantidad.text = "";
        _itemCantInv.text = "";
        foreach (GameObject g in _costes)
        {
            g.transform.Find("CosteItem").GetComponent<TextMeshProUGUI>().text = "";
            g.transform.Find("CosteCant").GetComponent<TextMeshProUGUI>().text = "";
            g.transform.Find("CosteIcon").GetComponent<Image>().sprite = Item.NONE.icono;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _degiterioCount.text = _items.getByID(1).cant.ToString();
        _occaterioCount.text = _items.getByID(2).cant.ToString();
        _itemCantInv.text = "Tienes: " + _items.getByID(_selectedItemID).cant;

        if (isShown && Input.GetButtonDown("Cancel"))
            isShown = false;

    }

    void FillItems()
    {
        DeleteItems();
        List<Item> shopItems = _shop.item;
        List<int> shopCant = _shop.cantidad;
        for(int i = 0; i < shopItems.Count; i++)
        {
            GameObject go = Instantiate(_itemPrefab);
            go.GetComponentsInChildren<Image>()[0].sprite = shopItems[i].icono;
            go.GetComponentsInChildren<RawImage>()[0].color = Item.GetRarezaColor(shopItems[i].rareza) - new Color(0, 0, 0, 0.5f);
            go.GetComponentInChildren<TextMeshProUGUI>().text = shopCant[i].ToString();
            go.name = "Item " + shopItems[i].ID.ToString();
            go.transform.parent = _viewportContent.transform;
        }
    }

    void DeleteItems()
    {
        foreach (Transform go in _viewportContent.transform)
        {
            Destroy(go.gameObject);
        }
    }

    public void SetShop(Shop shop)
    {
        _shop = shop;
        FillItems();
    }

    void Comprar()
    {
        StartCoroutine(ComprarC());
    }
    IEnumerator ComprarC()
    {
        var data = _shop.GetDataByItem(Item.getItemByID(_selectedItemID));

        WarningMessage.Show("¿Quieres comprar x" + data.cant + " " + data.item.nombre + " a cambio de los objetos requeridos?");
        yield return new WaitUntil(() => WarningMessage.answerReady);
        if(WarningMessage.GetAnswer)
        {
            var cost = data.cost;
            for (int i = 0; i < cost.items.Length; i++)
            {
                var v = _items.getByID(cost.items[i].ID);
                if(v.item.Equals(Item.NONE) || v.cant < cost.cant[i])
                {
                    WarningMessage.Show("Objetos insuficientes. Necesitas x" + cost.cant[i] + " " + cost.items[i].nombre + ", pero tienes x" + v.cant + ".");
                    yield return new WaitUntil(() => WarningMessage.answerReady);
                    break;
                }
            }
            if(!WarningMessage.answerReady)
            {
                for(int i = 0; i < cost.items.Length; i++)
                {
                    _items.Remove(cost.items[i], cost.cant[i]);
                }
                _items.Add(data.item, data.cant);
            }
        }
    }
}
