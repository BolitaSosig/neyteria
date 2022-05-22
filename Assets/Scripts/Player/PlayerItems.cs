using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItems : MonoBehaviour
{

    public static Item NONE;
    public static Item DEGITERIO;
    public static Item OCCATERIO;
    public static Item SUERO_VITAL;
    public static Item SUERO_ENERGETICO;
    public static Item SUERO_FORTALECEDOR;
    public static Item SUERO_PROTECTOR;
    public static Item MINERAL_FRAGMENTADO;
    public static Item NEXOTEK;
    public static Item MINERAL_COMPACTO;
    public static Item PIEDRAS_DE_LAVA;
    public static Item ROCA_DE_MAGMA;
    public static Item SOLLOZOS_DEL_CREPUSCULO;
    public static Item TEMOR_DEL_CREPUSCULO;


    private Dictionary<Item, int> items = new Dictionary<Item, int>();
    public bool printItems = false;

    public InventoryController _inventoryController;

    public void Start()
    {
        StartItems(); 

        Add(DEGITERIO, 0);
        Add(OCCATERIO, 0);
    }

    void StartItems()
    {
        NONE = Resources.Load<Item>("Data\\Item\\NONE");
        DEGITERIO = Resources.Load<Item>("Data\\Item\\Degiterio");
        OCCATERIO = Resources.Load<Item>("Data\\Item\\Occaterio");
        SUERO_VITAL = Resources.Load<Item>("Data\\Item\\Suero_vital");
        SUERO_ENERGETICO = Resources.Load<Item>("Data\\Item\\Suero_energetico");
        SUERO_FORTALECEDOR = Resources.Load<Item>("Data\\Item\\Suero_fortalecedor");
        SUERO_PROTECTOR = Resources.Load<Item>("Data\\Item\\Suero_protector");
        MINERAL_FRAGMENTADO = Resources.Load<Item>("Data\\Item\\Mineral_fragmentado");
        NEXOTEK = Resources.Load<Item>("Data\\Item\\Nexotek");
        MINERAL_COMPACTO = Resources.Load<Item>("Data\\Item\\Mineral_compacto");
        PIEDRAS_DE_LAVA = Resources.Load<Item>("Data\\Item\\Piedras_de_lava");
        ROCA_DE_MAGMA = Resources.Load<Item>("Data\\Item\\ROca_de_magma");
        SOLLOZOS_DEL_CREPUSCULO = Resources.Load<Item>("Data\\Item\\Sollozos_del_crepusculo");
        TEMOR_DEL_CREPUSCULO = Resources.Load<Item>("Data\\Item\\Temor_del_crepusculo");
    }

    public void Update()
    {
        if (printItems)
        {
            printItems = false;
            ShowConsole();
        }
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
        _inventoryController.SendMessage("FillItems");
    }

    public void Remove(Item i, int c)
    {
        if(items.ContainsKey(i))
        {
            int cant = items[i] - c;
            if (cant >= 0)
                items[i] = cant;
        }
        _inventoryController.SendMessage("FillItems");
    }

    public void Remove(Item i)
    {
        items.Remove(i);
        _inventoryController.SendMessage("FillItems");
    }

    public (Item, int) getByID(int id)
    {
        Debug.Log(items.Count);
        foreach (Item item in items.Keys)
        {
            if (item.ID == id)
                return (item, items[item]);
        }
        return (NONE, 0);
    }

    public List<(Item, int)> getAllItemsCant()
    {
        List<(Item, int)> res = new List<(Item, int)>();
        foreach(Item item in items.Keys)
        {
            res.Add((item, items[item]));
        }
        return res;
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
}
