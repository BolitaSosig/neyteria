using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItems : MonoBehaviour
{
    private Dictionary<Item, int> items = new Dictionary<Item, int>();
    public List<Item> rapidos = new List<Item>();
    public int index_rapido = 0;
    public bool printItems = false;

    public InventoryController _inventoryController;
    public EquipmentController _equipmentController;
    
    [Space(20)]
    [SerializeField] private Item a?adirObjeto;
    [SerializeField] private int a?adirCantidad;
    [SerializeField] private bool a?adir;


    public void Start()
    {
        items.Add(Item.DEGITERIO, 0);
        items.Add(Item.OCCATERIO, 0);
        items.Add(Item.ESPADA_CORTA, 1);
        items.Add(Item.MAZA, 1);
        items.Add(Item.CANON_LASER, 1);
        items.Add(Item.TUNICA_PROTECTORA, 1);
        _inventoryController.SendMessage("FillItems");
        _equipmentController.SendMessage("FillEquipment");
    }

    public void Update()
    {
        if(rapidos.Count > 0)
            RotateQuickItem();
        if (printItems)
        {
            printItems = false;
            ShowConsole();
        }
        if (a?adir)
            AddItemTroughtInspector();
    }

    public void Add(Item i, int c)
    {
        if(items.ContainsKey(i))
        {
            items[i] = Mathf.Min(c + items[i], i.maxCant);
        } else
        {
            items.Add(i, c);
        }
        FindObjectOfType<ItemObtenido>().ShowItem(i, c);
        _inventoryController.SendMessage("FillItems");
        _equipmentController.SendMessage("FillEquipment");
    }

    public void Remove(Item i, int c)
    {
        if(items.ContainsKey(i))
        {
            int cant = items[i] - c;
            if (cant > 0)
                items[i] = cant;
            else
            {
                Remove(i);
                return;
            }
        }
        _inventoryController.SendMessage("FillItems");
        _equipmentController.SendMessage("FillEquipment");
    }

    public void Remove(Item i)
    {
        RemoveQuickItem(i);
        items.Remove(i);
        if (i.GetType().IsEquivalentTo(typeof(Modulo)) && GetComponent<PlayerModulos>().IsEquiped((Modulo)i))
            GetComponent<PlayerModulos>().EquipModulo((Modulo)i);
        _inventoryController.SendMessage("FillItems");
        _equipmentController.SendMessage("FillEquipment");
    }

    public (Item item, int cant) getByID(int id)
    {
        foreach (Item item in items.Keys)
        {
            if (item.ID == id)
                return (item, items[item]);
        }
        return (Item.NONE, 0);
    }

    public List<(Item item, int cant)> getAllItemsCant()
    {
        List<(Item, int)> res = new List<(Item, int)>();
        foreach(Item item in items.Keys)
        {
            res.Add((item, items[item]));
        }
        return res;
    }

    public void AddQuickItem(Item item)
    {
        if (items.ContainsKey(item))
        {
            rapidos.Add(item);
            _inventoryController.SendMessage("FillItems");
        }
    }
    public void RemoveQuickItem(Item item)
    {
        if (items.ContainsKey(item))
        {
            rapidos.Remove(item);
            _inventoryController.SendMessage("FillItems");
            index_rapido = Mathf.Max(0, index_rapido - 1);
        }
    }

    void RotateQuickItem()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            index_rapido = (index_rapido + 1) % rapidos.Count;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            index_rapido = index_rapido - 1 < 0 ? rapidos.Count - 1 : index_rapido - 1;
    }

    public void ShowConsole()
    {
        string res = "\n---------------------\nPlayer Items:\n";
        foreach(Item item in items.Keys)
        {
            res += "  " + item.nombre + " x" + items[item] + "\n";
        }
        res += "---------------------\n";
        Debug.Log(res);
    }

    private void AddItemTroughtInspector()
    {
        a?adir = false;
        Add(a?adirObjeto, a?adirCantidad);
    }



    public IEnumerator UseQuickItem()
    {
        bool used = true;
        switch (rapidos[index_rapido].ID)
        {
            case 3:
                SueroVital_Effect();
                break;
            case 5:
                StartCoroutine(SueroFuerza_Effect());
                break;
            case 6:
                StartCoroutine(SueroProteccion_Effect());
                break;
            case 8:
                StartCoroutine(Nexoterio_Effect());
                break;
            default:
                used = false;
                break;
        }
        if(used)
            Remove(rapidos[index_rapido], 1);
        yield return new WaitForSeconds(0f);
    }
    private void SueroVital_Effect()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.SendMessage("RecuperarSalud", 50f);
    }
    private IEnumerator SueroFuerza_Effect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        if (!player.buffStorage.ContainsKey(5))
        {
            player.buffStorage.Add(3, 0.1f);
            player.AumAttack += 0.1f;
            yield return new WaitForSecondsRealtime(60f);
            player.AumAttack -= 0.1f;
        }
    }
    private IEnumerator SueroProteccion_Effect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        if (!player.buffStorage.ContainsKey(6))
        {
            player.AumDefense += 0.1f;
            yield return new WaitForSecondsRealtime(60f);
            player.AumDefense -= 0.1f;
        }
    }

    private IEnumerator Nexoterio_Effect()
    {
        FindObjectOfType<PlayerController>().SendMessage("TeleportLastNexo");
        yield return null;
    }
}
