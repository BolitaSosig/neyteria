using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Suministrador : MonoBehaviour
{
    public bool _opened;
    public bool isPlayer;
    [SerializeField] private GameObject ToggleCanvas;
    [SerializeField] private Light2D _luz;
    private GameObject _player;
    private ItemObtenido _itemObtenido;

    public Item[] drops;
    public int[] cant;
    [Range(0, 1)]
    public float[] rate;
    public bool[] asegurado;

    public bool Opened 
    {
        get { return _opened; }
        set
        {
            _opened = value;
            GetComponent<Animator>().SetBool("opened", _opened);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _itemObtenido = GameObject.Find("ItemObtained").GetComponent<ItemObtenido>();
        //ToggleCanvas = GetChildWithName(gameObject, "canvas2");
        ToggleCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Unlock();
                isPlayer = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool cond = other.CompareTag("Player") && !Opened;
        ToggleCanvas.SetActive(cond);
        isPlayer = cond;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        bool cond = other.CompareTag("Player") && !Opened;
        ToggleCanvas.SetActive(cond);
        isPlayer = cond;
    }

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    protected virtual void Unlock()
    {
        Opened = true;
        GetItems();
    }

    void GetItems()
    {
        PlayerItems pi = _player.GetComponent<PlayerItems>();
        for(int i = 0; i < drops.Length; i++)
        {
            int cantidad = asegurado[i] ? 1 : 0;
            for(int j = cantidad; j < cant[i]; j++)
            {
                float prob = Random.value;
                cantidad += prob <= rate[i] ? 1 : 0;
            }
            if (cantidad != 0)
            {
                pi.Add(drops[i], cantidad);
            }
        }
    }
}
