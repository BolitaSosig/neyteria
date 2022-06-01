using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentController : MonoBehaviour
{
    public GameObject armasViewport;
    public GameObject trajesViewport;
    public GameObject modulosViewport;
    [Space]
    public Transform espadasContent;
    public Transform mazasContent;
    public Transform laserContent;
    public Transform trajesContent;
    public Transform modulosContent;

    private Vector2 menuInitPos;
    private Vector2 armasInitPos;
    private Vector2 trajesInitPos;
    private Vector2 modulosInitPos;

    public PlayerItems _items;
    public GameObject _itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        menuInitPos = transform.position;
        armasInitPos = new Vector2(armasViewport.transform.position.x, armasViewport.transform.position.y);
        trajesInitPos = new Vector2(trajesViewport.transform.position.x, trajesViewport.transform.position.y);
        modulosInitPos = new Vector2(modulosViewport.transform.position.x, modulosViewport.transform.position.y);

        _items = FindObjectOfType<PlayerItems>();
        transform.position = menuInitPos * new Vector2(0, -100);

        //SelectArmas();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            transform.position = !transform.position.Equals(menuInitPos) ? menuInitPos : menuInitPos * new Vector2(0, -100);
        if (Input.GetKeyDown(KeyCode.M))
            FillEquipment();
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
        foreach ((Item i, int c) in _items.getAllItemsCant())
        {
            GameObject go = Instantiate(_itemPrefab);
            go.GetComponentsInChildren<Image>()[0].sprite = i.icono;
            go.name = "Item " + i.ID.ToString();
            go.transform.GetChild(2).gameObject.SetActive(false);

            if (i.GetType().IsEquivalentTo(typeof(Arma)))  // ARMAS
            {
                Arma a = (Arma)i;
                go.GetComponent<InventoryItemSelect>().equiped.SetActive(_items.gameObject.GetComponent<PlayerAttack>().arma.Equals(a));
                switch(a.tipo)
                {
                    case Arma.Tipo.Espada:
                        go.transform.parent = espadasContent.transform;
                        break;
                    case Arma.Tipo.Maza:
                        go.transform.parent = mazasContent.transform;
                        break;
                    case Arma.Tipo.Laser:
                        go.transform.parent = laserContent.transform;
                        break;
                }
            }
            if (i.GetType().IsEquivalentTo(typeof(Modulo)))  // MODULOS
            {
                Modulo m = (Modulo)i;
                go.GetComponent<InventoryItemSelect>().equiped.SetActive(_items.gameObject.GetComponent<PlayerModulos>().IsEquiped(m));
                go.transform.parent = modulosContent.transform;
            }
        }
    }
}
