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
    private CameraController _camera;
    private SpriteRenderer _spriteRenderer;
    public bool _activated = false;


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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!_activated && collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.Space))
            Toggle();
    }

    void Toggle()
    {
        _activated = true;
        gameObject.GetComponent<Animator>().SetBool("trigger", _activated);

        Tilemap tm = _togglePlatform.GetComponent<Tilemap>();
        if(tm != null)
            StartCoroutine(_camera.CinematicaInterruptor(_player, _togglePlatform, tm));
        else
        {
            SpriteRenderer sr = _togglePlatform.GetComponent<SpriteRenderer>();
            StartCoroutine(_camera.CinematicaInterruptor(_player, _togglePlatform, tm));
        }
        //gameObject.GetComponent<Animator>().Play("togglebutton");
    }


    
}
