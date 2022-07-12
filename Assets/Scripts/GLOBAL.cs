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
    public bool hard_mode;

    public static string zona = "CollisionNivel1";
    public static NexoCentralController.Nexo lastNexo = NexoCentralController.Nexo.Nexo_0;

    // ENEMIGO
    public static float AUMENTO_NV = 0.99f;
    public static bool HARD_MODE = false;

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
        AUMENTO_NV = hard_mode ? 0.99f : 1.49f;
        if(hard_mode != HARD_MODE)
        {
            hard_mode = HARD_MODE;
            StartCoroutine(ChangeMode());
        }
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

    IEnumerator ChangeMode()
    {
        foreach (Enemigo e in FindObjectsOfType<Enemigo>())
            e.SendMessage("UpdateStats");
        yield return null;
    }
}
