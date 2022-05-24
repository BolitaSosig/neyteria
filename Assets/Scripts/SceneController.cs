using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(string objName)
    {
        switch (objName)
        {
            case "RestZoneLoadLevel1":
                if (SceneManager.GetSceneByName("RestZone").name == null)
                {
                    SceneManager.LoadSceneAsync("RestZone", LoadSceneMode.Additive);
                    SceneManager.LoadSceneAsync("Nivel2-1", LoadSceneMode.Additive);
                    GLOBAL.zona = "RestZone";
                } else
                {
                    SceneManager.UnloadSceneAsync("RestZone");
                    SceneManager.UnloadSceneAsync("Nivel2-1");
                    GLOBAL.zona = "Nivel1-1";
                }
                GameObject.FindObjectOfType<Luz>().ChangeLightBetweenZones();
                return;
            case "RestZoneLoadLevel2":
                if (SceneManager.GetSceneByName("RestZone").name == null)
                {
                    SceneManager.LoadSceneAsync("RestZone", LoadSceneMode.Additive);
                    SceneManager.LoadSceneAsync("Nivel1-1", LoadSceneMode.Additive);
                    GLOBAL.zona = "RestZone";
                }
                else
                {
                    SceneManager.UnloadSceneAsync("RestZone");
                    SceneManager.UnloadSceneAsync("Nivel1-1");
                    GLOBAL.zona = "Nivel2-1";
                }
                GameObject.FindObjectOfType<Luz>().ChangeLightBetweenZones();
                return;
        }
    }

    public void UnloadLevels()
    {
        for(int i = 0; i < SceneManager.sceneCount; i++) {
            if(!Scene.Equals(SceneManager.GetSceneAt(i), SceneManager.GetActiveScene()))
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }
    }
}
