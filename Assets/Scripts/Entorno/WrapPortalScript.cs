using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapPortalScript : MonoBehaviour
{
    public GameObject EndPortal;
    private GameObject PortalCanvas;
    private GameObject _player;
    public bool isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        PortalCanvas = GetChildWithName(gameObject, "canvas2");
        PortalCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TransportPortal();
                isPlayer = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool cond = other.CompareTag("Player");
        PortalCanvas.SetActive(cond);
        isPlayer = cond;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        bool cond = other.CompareTag("Player");
        PortalCanvas.SetActive(cond);
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

    void TransportPortal()
    {
        _player.transform.position = EndPortal.transform.position;
    }

}
