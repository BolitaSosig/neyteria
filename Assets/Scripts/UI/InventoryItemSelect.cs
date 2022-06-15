using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSelect : MonoBehaviour
{
    private InventoryController _inventory;
    private EquipmentController _equipment;
    private PlayerItems _items;
    public GameObject equiped;
    public GameObject cantidad;

    public Item item;


    private void Start()
    {
        _inventory = GameObject.Find("Inventario").GetComponent<InventoryController>();
        _equipment = FindObjectOfType<EquipmentController>();
        _items = GameObject.Find("Player").GetComponent<PlayerItems>();

        item = _items.getByID(Int32.Parse(gameObject.name.Replace("Item ", ""))).Item1;
        equiped.SetActive(
            (item.GetType().IsEquivalentTo(typeof(Arma)) && _items.GetComponent<PlayerAttack>().arma.Equals((Arma)item)) ||
            (item.GetType().IsEquivalentTo(typeof(Traje)) && _items.GetComponent<PlayerTrajes>()._traje.Equals((Traje)item)) ||
            (item.GetType().IsEquivalentTo(typeof(Modulo)) && _items.GetComponent<PlayerModulos>().IsEquiped((Modulo)item))
            );
    }

    public void SelectItem()
    {
        int id = Int32.Parse(gameObject.name.Replace("Item ", ""));
        (Item item, int cant) pair = _items.getByID(id);

        if (pair.item.GetType().IsEquivalentTo(typeof(Arma)))
            SelectArmaEquipment((Arma)pair.item);
        else if (pair.item.GetType().IsEquivalentTo(typeof(Traje)))
            SelectTrajeEquipment((Traje)pair.item);
        else if (pair.item.GetType().IsEquivalentTo(typeof(Modulo)))
            SelectModuloEquipment((Modulo)pair.item);
        else
            SelectItemInventory(pair);
    }

    void SelectItemInventory((Item item, int cant) pair)
    {
        _inventory._selectedItemID = pair.item.ID;

        _inventory._itemName.text = pair.item.nombre;
        _inventory._itemDescripcion.text = pair.item.descripcion;
        _inventory._itemRareza.text = "Rareza " + pair.item.rareza;
        _inventory._itemRareza.color = Item.GetRarezaColor(pair.item.rareza);
        _inventory._itemIcon.sprite = pair.item.icono;
        _inventory._itemCantidad.text = "Tienes: " + pair.cant;
    }
    
    void SelectArmaEquipment(Arma arma)
    {
        _equipment.selectedEquip = arma;
        _equipment._itemName.text = arma.nombre;
        _equipment._itemDescripcion.text = arma.descripcion;
        _equipment._itemRareza.text = "Rareza " + arma.rareza;
        _equipment._itemRareza.color = Item.GetRarezaColor(arma.rareza);
        _equipment._itemIcon.sprite = arma.icono;
        _equipment._label1.text = "Tipo: " + arma.tipo.ToString();
        _equipment._label2.text = "Ataque: " + arma.ataque + "p";
        _equipment._label3.text = "Peso: " + arma.peso + "p";
    }

    void SelectTrajeEquipment(Traje traje)
    {
        _equipment.selectedEquip = traje;
        _equipment._itemName.text = traje.nombre;
        _equipment._itemDescripcion.text = traje.descripcion;
        _equipment._itemRareza.text = "Rareza " + traje.rareza;
        _equipment._itemRareza.color = Item.GetRarezaColor(traje.rareza);
        _equipment._itemIcon.sprite = traje.icono;
        _equipment._label1.text = "";
        _equipment._label2.text = "Defensa: " + traje.defensa + "p";
        _equipment._label3.text = "Peso: " + traje.peso + "p";
    }

    void SelectModuloEquipment(Modulo modulo)
    {
        _equipment.selectedEquip = modulo;
        _equipment._itemName.text = modulo.nombre;
        _equipment._itemDescripcion.text = modulo.descripcion;
        _equipment._itemRareza.text = "Rareza " + modulo.rareza;
        _equipment._itemRareza.color = Item.GetRarezaColor(modulo.rareza);
        _equipment._itemIcon.sprite = modulo.icono;
        _equipment._label1.text = "Ranuras: " + modulo.Slots;
        _equipment._label2.text = "Duración: " + modulo.Duracion + "s";
        _equipment._label3.text = "TdE: " + modulo.TdE + "s";
    }
}
