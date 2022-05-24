using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GLOBAL : MonoBehaviour
{
    public GameObject startPosition;
    public GameObject player;
    public int d = 0;
    public PauseMenu pauseMenu;

    public static string zona;

    // ENEMIGO
    public static float AUMENTO_NV = 0.99f;

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

    public void StartPos()
    {
        if (startPosition.activeSelf)
            player.transform.position = startPosition.transform.position;
    }

    /*public static void SetZone(Scene z)
    {
        zona = z;
    }*/
}
