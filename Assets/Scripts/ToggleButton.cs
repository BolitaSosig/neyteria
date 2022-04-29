using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject _togglePlatform;
    private GameObject _player;
    [SerializeField] private Sprite _endFrame;
    [SerializeField] private Vector2 _tooglePosition;
    private CameraController _camera;
    private SpriteRenderer _spriteRenderer;
    public bool _activated = false;
    public bool isTimed = false;
    public float segundos = 0;
    [SerializeField] public bool Activated { 
        get { return _activated; }
        set
        {
            _activated = value;
            GetComponent<Animator>().SetBool("isToggled", _activated);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindObjectOfType<CameraController>();
        _player = GameObject.Find("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Activated && collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
            Toggle();
    }

    void Toggle()
    {
        Activated = true;
        gameObject.GetComponent<Animator>().SetBool("trigger", Activated);

        Tilemap tm = _togglePlatform.GetComponent<Tilemap>();
        if (tm != null)
        {
            StartCoroutine(_camera.CinematicaInterruptor(_player, _togglePlatform, tm));

            if (isTimed)
                StartCoroutine(Timing(tm));
        }
        else
        {
            SpriteRenderer sr = _togglePlatform.GetComponent<SpriteRenderer>();
            StartCoroutine(_camera.CinematicaInterruptor(_player, _togglePlatform, tm));
            if (isTimed)
                StartCoroutine(Timing(sr));
        }
        //gameObject.GetComponent<Animator>().Play("togglebutton");
    }

    IEnumerator Timing(Tilemap tm)
    {
        yield return new WaitForSeconds(segundos - 3f);

        for(int i = 30; i > 0; i--)
        {
            tm.color -= new Color(0, 0, 0, 1f/30f);
            yield return new WaitForSeconds(0.1f);
        }

        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, 0);
        _togglePlatform.SetActive(false);
        Activated = false;
    }
    IEnumerator Timing(SpriteRenderer sr)
    {
        yield return new WaitForSeconds(segundos - 3f);

        for(int i = 30; i > 0; i--)
        {
            sr.color -= new Color(0, 0, 0, 1f/30f);
            yield return new WaitForSeconds(0.1f);
        }

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        _togglePlatform.SetActive(false);
        Activated = false;
    }
}
