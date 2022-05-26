using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSelect : MonoBehaviour
{
    private InventoryController _inventory;
    private PlayerItems _items;


    private void Start()
    {
        _inventory = GameObject.Find("Inventario").GetComponent<InventoryController>();
        _items = GameObject.Find("Player").GetComponent<PlayerItems>();
    }

    public void SelectItem()
    {
        int id = Int32.Parse(gameObject.name.Replace("Item ", ""));
        (Item item, int cant) pair = _items.getByID(id);
        _inventory._selectedItemID = id;

        _inventory._itemName.text = pair.item.nombre;
        _inventory._itemDescripcion.text = pair.item.descripcion;
        _inventory._itemRareza.text = "Rareza " + pair.item.rareza;
        _inventory._itemIcon.sprite = pair.item.icono;
        _inventory._itemCantidad.text = "Tienes: " + pair.cant;
    }
}
