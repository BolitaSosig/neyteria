using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public SpriteRenderer background;
    
    [SerializeField] private BackgroundController[] background_sprites;
    [SerializeField] private string[] background_names = new string[] {
        "CollisionNivel1",
        "CollisionNivel2",
        "CollisionNivel31Under",
        "CollisionNivel31Outer",
        "CollisionNivel32"
    };

    // Start is called before the first frame update
    void Start()
    {
        EnterCollisionNivel(GLOBAL.zona);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadLevel(string objName)
    {
        switch (objName)
        {
            case "RestZoneLoadLevel1":
                //SceneManager.LoadSceneAsync("Nivel2-1", LoadSceneMode.Additive);
                FindObjectOfType<TeleportController>().tubos_1_2.gameObject.SetActive(true);
                SceneManager.GetSceneByName("Nivel2-1").GetRootGameObjects()[0].GetComponent<ZoneOptimizer>().ZonePaused = false;
                SceneManager.GetSceneByName("Nivel1-1").GetRootGameObjects()[0].GetComponent<ZoneOptimizer>().ZonePaused = true;
                if (SceneManager.GetSceneByName("RestZone").name == null)
                {
                    //SceneManager.LoadSceneAsync("RestZone", LoadSceneMode.Additive);
                    //GLOBAL.zona = "RestZone";
                } else
                {
                    //SceneManager.UnloadSceneAsync("RestZone");
                    //SceneManager.UnloadSceneAsync("Nivel2-1");
                    //GLOBAL.zona = "Nivel1-1";
                }
                GameObject.FindObjectOfType<Luz>().ChangeLightBetweenZones();
                break;
            case "RestZoneToLevel1":
                //SceneManager.UnloadSceneAsync("Nivel2-1");
                FindObjectOfType<TeleportController>().tubos_1_2.gameObject.SetActive(false);
                SceneManager.GetSceneByName("Nivel2-1").GetRootGameObjects()[0].GetComponent<ZoneOptimizer>().ZonePaused = true;
                SceneManager.GetSceneByName("Nivel1-1").GetRootGameObjects()[0].GetComponent<ZoneOptimizer>().ZonePaused = false;
                GameObject.FindObjectOfType<Luz>().ChangeLightBetweenZones();
                break;
            case "RestZoneLoadLevel2":
                if (SceneManager.GetSceneByName("RestZone").name == null)
                {
                    //SceneManager.LoadSceneAsync("RestZone", LoadSceneMode.Additive);
                    SceneManager.LoadSceneAsync("Nivel1-1", LoadSceneMode.Additive);
                    //GLOBAL.zona = "RestZone";
                }
                else
                {
                    //SceneManager.UnloadSceneAsync("RestZone");
                    SceneManager.UnloadSceneAsync("Nivel1-1");
                    //GLOBAL.zona = "Nivel2-1";
                }
                GameObject.FindObjectOfType<Luz>().ChangeLightBetweenZones();
                break;
        }
        yield return null;
    }

    public void UnloadLevels()
    {
        for(int i = 0; i < SceneManager.sceneCount; i++) {
            if(!Scene.Equals(SceneManager.GetSceneAt(i), SceneManager.GetActiveScene()))
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
        }
    }


    public void EnterCollisionNivel(string name)
    {
        switch(name)
        {
            case "CollisionNivel1":
                CollisionNivel1(name);
                break;
            case "CollisionNivel2":
                CollisionNivel2(name);
                break;
            case "CollisionNivel31Under":
                CollisionNivel31Under(name);
                break;
            case "CollisionNivel31Outer":
                CollisionNivel31Outer(name);
                break;
            case "CollisionNivel32":
                CollisionNivel32(name);
                break;
        }
    }

    void CollisionNivel1(string name)
    {
        GLOBAL.zona =  name;
        Luz luz = GameObject.Find("Luz").GetComponent<Luz>();
        luz._linterna.color = new Color(1f, 1f, 0.6f);
        luz._globalLight.color = new Color(0f, 0f, 0f);
        luz._globalLight.intensity = 1f;
    }

    void CollisionNivel2(string name)
    {
        GLOBAL.zona = name;
        Luz luz = GameObject.Find("Luz").GetComponent<Luz>();
        luz._linterna.color = new Color(0f, 0f, 0f);
        luz._globalLight.color = new Color(0.5f, 0.4f, 0.2f);
        luz._globalLight.intensity = 1f;
    }

    void CollisionNivel31Under(string name)
    {
        GLOBAL.zona = name;
        Luz luz = GameObject.Find("Luz").GetComponent<Luz>();
        luz._linterna.color = new Color(0f, 0f, 0f);
        luz._globalLight.color = new Color(0.5f, 0.4f, 0.2f);
        luz._globalLight.intensity = 0.5f;
    }

    void CollisionNivel31Outer(string name)
    {
        GLOBAL.zona = name;
        Luz luz = GameObject.Find("Luz").GetComponent<Luz>();
        luz._linterna.color = new Color(0f, 0f, 0f);
        luz._globalLight.color = new Color(1f, 1f, 1f);
        luz._globalLight.intensity = 2f;
    }

    void CollisionNivel32(string name)
    {
        GLOBAL.zona = name;
        Luz luz = GameObject.Find("Luz").GetComponent<Luz>();
        luz._linterna.color = new Color(0f, 0f, 0f);
        luz._globalLight.color = new Color(0.7f, 1f, 0.6f);
        luz._globalLight.intensity = 1f;
    }
}
