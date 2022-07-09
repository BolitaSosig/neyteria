using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLevelController : MonoBehaviour
{

    

    public int id = -1;
    public int act = 0;

    public int numMusicas = 5;
    GameObject[] GameObjectsMusic;
    // Start is called before the first frame update
    void Start()
    {
        GameObjectsMusic = new GameObject[numMusicas];
        Actualizar(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (act != id) Actualizar(act);
    }

    public void FindGameObjectsMusic ()
    {
        GameObjectsMusic[0] = GameObject.Find("MusicLevel1");
        GameObjectsMusic[1] = GameObject.Find("MusicLevel2");
        GameObjectsMusic[2] = GameObject.Find("MusicLevel3-1");
        GameObjectsMusic[3] = GameObject.Find("MusicLevel3-2");
        GameObjectsMusic[4] = GameObject.Find("MusicLevel4");
    }

    public void Actualizar(int v)
    {
        FindGameObjectsMusic();
        foreach (GameObject Imusic in GameObjectsMusic) { if (Imusic != null) Imusic.GetComponent<AudioSource>().mute = true; }


        if (v >= 0 && v < numMusicas && GameObjectsMusic[v] != null)
        {
            GameObjectsMusic[v].GetComponent<AudioSource>().mute = false; id = act;
        }
        else
        {
            id = -1; act = -1;
        }


    }


    /*  0 es musica nivel 1
     *  1 es musica nivel 2
     *  2 es musica nivel 3-1
     *  3 es musica nivel 3-2
     *  4 es musica nivel 4
     *  -1 es error
     */


}
