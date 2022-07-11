using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentController : MonoBehaviour
{
    public Item selectedEquip;

    public GameObject armasViewport;
    public GameObject trajesViewport;
    public GameObject modulosViewport;
    [Space]
    public Transform espadasContent;
    public Transform mazasContent;
    public Transform laserContent;
    public Transform trajesContent;
    public Transform modulosContent;
    public Transform estadoInfo;
    public Transform itemInfo;

    private Vector2 menuInitPos;
    private Vector2 armasInitPos;
    private Vector2 trajesInitPos;
    private Vector2 modulosInitPos;
    private Vector2 estadoInitPos;
    private Vector2 itemInitPos;

    public PlayerItems _items;
    public PlayerController _player;
    public GameObject _itemPrefab;
    [Space]
    // EQUIP INFO
    public TextMeshProUGUI _itemName;
    public Image _itemIcon;
    public TextMeshProUGUI _itemRareza;
    public TextMeshProUGUI _label1;
    public TextMeshProUGUI _label2;
    public TextMeshProUGUI _label3;
    public TextMeshProUGUI _itemDescripcion;
    [Space]
    public TextMeshProUGUI _hp;
    public TextMeshProUGUI _stamina;
    public TextMeshProUGUI _attack;
    public TextMeshProUGUI _defense;
    public TextMeshProUGUI _weight;
    public TextMeshProUGUI _movSpeed;
    public TextMeshProUGUI _attSpeed;
    public TextMeshProUGUI _jumpCap;
    public TextMeshProUGUI _gastoDash;
    public TextMeshProUGUI _dmgReduc;
    [Space]
    public GameObject _equiparOpcion;

    private bool _isShow = false;
    public bool IsShow
    {
        set 
        { 
            _isShow = value;
            transform.position = _isShow ? menuInitPos : menuInitPos * new Vector2(0, -100);
            _equiparOpcion.SetActive(_player.GetComponent<PlayerInputManager>()._isEquipmentNexo);
        }
        get { return _isShow; }
    }

    // Start is called before the first frame update
    void Start()
    {
        menuInitPos = transform.position;
        armasInitPos = new Vector2(armasViewport.transform.position.x, armasViewport.transform.position.y);
        trajesInitPos = new Vector2(trajesViewport.transform.position.x, trajesViewport.transform.position.y);
        modulosInitPos = new Vector2(modulosViewport.transform.position.x, modulosViewport.transform.position.y);
        estadoInitPos = new Vector2(estadoInfo.transform.position.x, estadoInfo.transform.position.y);
        itemInitPos = new Vector2(itemInfo.transform.position.x, itemInfo.transform.position.y);

        _items = FindObjectOfType<PlayerItems>();
        _player = FindObjectOfType<PlayerController>();
        itemInfo.transform.position = itemInitPos * new Vector2(0, -100);
        SelectArmas();
        IsShow = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (/*Input.GetButton("Equipment") && Input.GetButtonDown("Equipment2") Input.GetKeyDown(KeyCode.R))
            IsShow = !IsShow;*/
        if (Input.GetKeyDown(KeyCode.Z))
        {
            estadoInfo.transform.position = !estadoInfo.transform.position.Equals(estadoInitPos) ? estadoInitPos : estadoInitPos * new Vector2(0, -100);
            itemInfo.transform.position = !itemInfo.transform.position.Equals(itemInitPos) ? itemInitPos : itemInitPos * new Vector2(0, -100);
        }
        if (Input.GetButtonDown("UseItem") && IsShow && _equiparOpcion.activeSelf)
            Equipar();
        UpdateStats();
    }

    public void SelectArmas()
    {
        armasViewport.transform.position = armasInitPos;
        trajesViewport.transform.position = trajesInitPos * new Vector2(0, -100);
        modulosViewport.transform.position = modulosInitPos * new Vector2(0, -100);
    }
    
    public void SelectTrajes()
    {
        armasViewport.transform.position = armasInitPos * new Vector2(0, -100);
        trajesViewport.transform.position = trajesInitPos;
        modulosViewport.transform.position = modulosInitPos * new Vector2(0, -100);
    }
    
    public void SelectModulos()
    {
        armasViewport.transform.position = armasInitPos * new Vector2(0, -100);
        trajesViewport.transform.position = trajesInitPos * new Vector2(0, -100);
        modulosViewport.transform.position = modulosInitPos;
    }

    public void FillEquipment()
    {
        DeleteItems();

        foreach ((Item i, int c) in _items.getAllItemsCant())
        {
            GameObject go = Instantiate(_itemPrefab);
            go.GetComponentsInChildren<Image>()[0].sprite = i.icono;
            go.GetComponentsInChildren<RawImage>()[0].color = Item.GetRarezaColor(i.rareza) - new Color(0, 0, 0, 0.5f);
            go.name = "Item " + i.ID.ToString();
            go.transform.GetChild(2).gameObject.SetActive(false);

            if (i.GetType().IsEquivalentTo(typeof(Arma)))  // ARMAS
            {
                Arma a = (Arma)i;
                switch (a.tipo)
                {
                    case Arma.Tipo.Espada:
                        go.transform.SetParent(espadasContent.transform);
                        break;
                    case Arma.Tipo.Maza:
                        go.transform.SetParent(mazasContent.transform);
                        break;
                    case Arma.Tipo.Laser:
                        go.transform.SetParent(laserContent.transform);
                        break;
                }
                //go.GetComponent<InventoryItemSelect>().equiped.SetActive(_items.gameObject.GetComponent<PlayerAttack>().arma.Equals(a));
            }
            else if (i.GetType().IsEquivalentTo(typeof(Modulo)))  // MODULOS
            {
                Modulo m = (Modulo)i;
                go.transform.parent = modulosContent.transform;
                go.GetComponent<InventoryItemSelect>().equiped.GetComponentInChildren<TextMeshProUGUI>().text =
                    "" + (_player.GetComponent<PlayerModulos>().FindModule(m) + 1);
                //go.GetComponent<InventoryItemSelect>().equiped.SetActive(_items.gameObject.GetComponent<PlayerModulos>().IsEquiped(m));
            }
            else
                Destroy(go);
        }
    }

    void DeleteItems()
    {
        foreach (Transform go in espadasContent)
            Destroy(go.gameObject);
        foreach (Transform go in mazasContent)
            Destroy(go.gameObject);
        foreach (Transform go in laserContent)
            Destroy(go.gameObject);
        foreach (Transform go in trajesContent)
            Destroy(go.gameObject);
        foreach (Transform go in modulosContent)
            Destroy(go.gameObject);
    }

    void UpdateStats()
    {
        _hp.text = (int)_player.HP + "/" + (int)_player.MaxHP;
        _stamina.text = (int)_player.Stamina + "/" + (int)_player.MaxStamina;
        _attack.text = "" + FormatFloat(_player.Attack);
        _defense.text = "" + FormatFloat(_player.Defense);
        _weight.text = "" + FormatFloat(_player.Weight);
        _movSpeed.text = FormatPerc(_player.MovSpeed) + "%";
        _attSpeed.text = FormatPerc(_player.AttSpeed) + "%";
        _jumpCap.text = FormatPerc(_player.JumpCap) + "%";  
        _gastoDash.text = "" + (int)_player.gastoDash;
        _dmgReduc.text = FormatPerc(_player.dmgReduc) + "%";
    }

    string FormatFloat(float v)
    {
        return ((float)Mathf.RoundToInt(v * 10f)/10f).ToString();
    }

    string FormatPerc(float p)
    {
        return Mathf.RoundToInt(p * 100f).ToString();
    }

    void Equipar()
    {
        if (selectedEquip.GetType().IsEquivalentTo(typeof(Arma)))
        {
            _player.GetComponent<PlayerAttack>().arma = (Arma)selectedEquip;
            FillEquipment();
        }
        else if (selectedEquip.GetType().IsEquivalentTo(typeof(Traje)))
        {
            _player.GetComponent<PlayerTrajes>()._traje = (Traje)selectedEquip;
            FillEquipment();
        }
        else if (selectedEquip.GetType().IsEquivalentTo(typeof(Modulo)))
            _player.GetComponent<PlayerModulos>().SendMessage("EquipModulo", (Modulo)selectedEquip);
    }
}
