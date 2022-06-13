using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public SpriteRenderer background;
    
    [SerializeField] private Sprite[] background_sprites;
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


    public void EnterCollisionNivel(string name)
    {
        switch(name)
        {
            case "CollisionNivel1":
                CollisionNivel1();
                break;
            case "CollisionNivel2":
                CollisionNivel2();
                break;
            case "CollisionNivel31Under":
                CollisionNivel31Under();
                break;
            case "CollisionNivel31Outer":
                CollisionNivel31Outer();
                break;
            case "CollisionNivel32":
                CollisionNivel32();
                break;
        }
    }

    void CollisionNivel1()
    {
        GLOBAL.zona =  background_names[0];
        Luz luz = GameObject.Find("Luz").GetComponent<Luz>();
        luz._linterna.color = new Color(1f, 1f, 0.6f);
        luz._globalLight.color = new Color(0f, 0f, 0f);
        luz._globalLight.intensity = 1f;
        background.sprite = background_sprites[0];
    }

    void CollisionNivel2()
    {
        GLOBAL.zona = background_names[1];
        Luz luz = GameObject.Find("Luz").GetComponent<Luz>();
        luz._linterna.color = new Color(0f, 0f, 0f);
        luz._globalLight.color = new Color(0.5f, 0.4f, 0.2f);
        luz._globalLight.intensity = 1f;
        background.sprite = background_sprites[1];
    }

    void CollisionNivel31Under()
    {
        GLOBAL.zona = background_names[2];
        Luz luz = GameObject.Find("Luz").GetComponent<Luz>();
        luz._linterna.color = new Color(0f, 0f, 0f);
        luz._globalLight.color = new Color(0.5f, 0.4f, 0.2f);
        luz._globalLight.intensity = 0.5f;
        background.sprite = background_sprites[2];
    }

    void CollisionNivel31Outer()
    {
        GLOBAL.zona = background_names[3];
        Luz luz = GameObject.Find("Luz").GetComponent<Luz>();
        luz._linterna.color = new Color(0f, 0f, 0f);
        luz._globalLight.color = new Color(1f, 1f, 1f);
        luz._globalLight.intensity = 2f;
        background.sprite = background_sprites[3];
    }

    void CollisionNivel32()
    {
        GLOBAL.zona = background_names[4];
        Luz luz = GameObject.Find("Luz").GetComponent<Luz>();
        luz._linterna.color = new Color(0f, 0f, 0f);
        luz._globalLight.color = new Color(0.7f, 1f, 0.6f);
        luz._globalLight.intensity = 1f;
        background.sprite = background_sprites[4];
    }
}
