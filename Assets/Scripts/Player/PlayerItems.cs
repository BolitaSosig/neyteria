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

    public void Start()
    {
        Add(Item.DEGITERIO, 0);
        Add(Item.OCCATERIO, 0);
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
        items.Remove(i);
        RemoveQuickItem(i);
        _inventoryController.SendMessage("FillItems");
        _equipmentController.SendMessage("FillEquipment");
    }

    public (Item, int) getByID(int id)
    {
        foreach (Item item in items.Keys)
        {
            if (item.ID == id)
                return (item, items[item]);
        }
        return (Item.NONE, 0);
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



    public IEnumerator UseQuickItem()
    {
        bool used = true;
        switch (rapidos[index_rapido].ID)
        {
            case 3:
                StartCoroutine(SueroVital_Effect());
                break;
            case 5:
                StartCoroutine(SueroFuerza_Effect());
                break;
            case 6:
                StartCoroutine(SueroProteccion_Effect());
                break;
            default:
                used = false;
                break;
        }
        if(used)
            Remove(rapidos[index_rapido], 1);
        yield return new WaitForSeconds(0f);
    }
    private IEnumerator SueroVital_Effect()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        int hpo = 50;
        while (player.HP < player.MaxHP && hpo > 0)
        {
            player.HP += 1;
            hpo--;
            yield return new WaitForSecondsRealtime(1f / 50f);
        }
    }
    private IEnumerator SueroFuerza_Effect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.AumAttack += 0.1f;
        yield return new WaitForSecondsRealtime(60f);
        player.AumAttack -= 0.1f;
    }
    private IEnumerator SueroProteccion_Effect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.AumDefense += 0.1f;
        yield return new WaitForSecondsRealtime(60f);
        player.AumDefense -= 0.1f;
    }
}
