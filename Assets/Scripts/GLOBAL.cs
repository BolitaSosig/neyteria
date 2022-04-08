using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL : MonoBehaviour
{
    public GameObject startPosition;
    public GameObject player;
    public int d = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        startPosition = GameObject.Find("StartPos");
        StartPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartPos()
    {
        if (startPosition.activeSelf)
            player.transform.position = startPosition.transform.position;
    }
}
