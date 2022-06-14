using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToggleBreaking : MonoBehaviour
{
    public int nHits = 3;

    private GameObject _player;
    private CameraController _camera;

    public bool isTimed = false;
    public bool spawnPlatform = true;
    public float segundos = 0;

    private Vector3 posInicial;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindObjectOfType<CameraController>();
        _player = GameObject.Find("Player");
        posInicial = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamageByPlayer(float dmg)
    {
        nHits--;
        StartCoroutine(Vibrar());
        Debug.Log("golpe");
    }

    IEnumerator Vibrar()
    {

        for (int i = 20; i > 0; i--)
        {
            gameObject.transform.position = new Vector3(posInicial.x + Random.Range(-0.05f, 0.07f), posInicial.y + Random.Range(-0.025f, 0.025f), posInicial.z);
            yield return new WaitForSeconds(0.025f);
        }

        gameObject.transform.position = posInicial;

        if (nHits <= 0) { Toggle(); }
    }

    void Toggle()
    {
        Tilemap tm = GetComponent<Tilemap>();
        if (tm != null)
        {
            StartCoroutine(_camera.CinematicaInterruptor(_player, gameObject, tm, spawnPlatform));

            if (isTimed)
                StartCoroutine(Timing(tm));
        }
        else
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            StartCoroutine(_camera.CinematicaInterruptor(_player, gameObject, tm, spawnPlatform));
            if (isTimed)
                StartCoroutine(Timing(sr));
        }
    }


    IEnumerator Timing(Tilemap tm)
    {
        yield return new WaitForSeconds(segundos - 3f);

        for (int i = 30; i > 0; i--)
        {
            tm.color -= new Color(0, 0, 0, 1f / 30f);
            yield return new WaitForSeconds(0.1f);
        }

        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, 0);
        gameObject.SetActive(false);
    }
    IEnumerator Timing(SpriteRenderer sr)
    {
        yield return new WaitForSeconds(segundos - 3f);

        for (int i = 30; i > 0; i--)
        {
            sr.color -= new Color(0, 0, 0, 1f / 30f);
            yield return new WaitForSeconds(0.1f);
        }

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        gameObject.SetActive(false);
    }

    
}
