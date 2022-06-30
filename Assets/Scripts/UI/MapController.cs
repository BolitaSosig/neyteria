using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public const int TP1 = 0;
    public const int TP2 = 1;
    public const int TP3 = 2;
    public const int TP32 = 3;
    public const int TP4 = 4;

    public GameObject[] maps = new GameObject[5];
    private bool[] tps = new bool[5];
    private Vector3 startPos;
    private bool _show;
    
    public bool Show
    {
        get { return _show; }
        set
        {
            _show = value;
            FindObjectOfType<PlayerInputManager>()._isMap = _show;
            transform.position = _show ? startPos : startPos - new Vector3(0, 1000f);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        transform.position = startPos - new Vector3(0, 1000f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateTp(int i)
    {
        tps[i] = true;
        maps[i].SetActive(true);
    }

    public void ActivateTp1()
    {
        ActivateTp(TP1);
    }
    public void ActivateTp2()
    {
        ActivateTp(TP2);
    }
    public void ActivateTp3()
    {
        ActivateTp(TP3);
    }
    public void ActivateTp32()
    {
        ActivateTp(TP32);
    }
    public void ActivateTp4()
    {
        ActivateTp(TP4);
    }


    void Teleport(NexoCentralController.Nexo end)
    {
        if(!GLOBAL.lastNexo.Equals(end))
            FindObjectOfType<TeleportController>().Teleport(GLOBAL.lastNexo, end);
        Show = false;
    }

    void TeleportTp1()
    {
        Teleport(NexoCentralController.Nexo.Nexo_1_2);
    }
    void TeleportTp2()
    {
        Teleport(NexoCentralController.Nexo.Nexo_2_3);
    }
    void TeleportTp3()
    {
        Teleport(NexoCentralController.Nexo.Nexo_3_3);
    }
    void TeleportTp32()
    {
        Teleport(NexoCentralController.Nexo.Nexo_3_4);
    }
    void TeleportTp4()
    {
        Teleport(NexoCentralController.Nexo.Nexo_4_4);
    }
}
