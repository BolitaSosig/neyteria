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
    private CinemachineVirtualCamera _camera;
    private SpriteRenderer _spriteRenderer;
    public bool _activated = false;


    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        _player = GameObject.Find("Player");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Animator>().SetBool("isToggled", _activated);
        /*if(_activated)
            gameObject.GetComponent<SpriteRenderer>().sprite = _endFrame;*/
        Debug.Log(_spriteRenderer.sprite.name);
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
            StartCoroutine(Cinematica(tm));
        else
        {
            SpriteRenderer sr = _togglePlatform.GetComponent<SpriteRenderer>();
            StartCoroutine(Cinematica(sr));
        }
        //gameObject.GetComponent<Animator>().Play("togglebutton");
    }

    IEnumerator Cinematica(Tilemap tm)
    {
        _player.GetComponent<PlayerController>().canMove = false;

        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, 0f);

        _camera.m_Follow = _togglePlatform.transform;
        _togglePlatform.SetActive(true);
        yield return new WaitForSeconds(1);
        while(tm.color.a < 1)
        {
            tm.color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        _camera.m_Follow = _player.transform;
        _player.GetComponent<PlayerController>().canMove = true;
        _spriteRenderer.sprite = _endFrame;
    }

    IEnumerator Cinematica(SpriteRenderer sr)
    {
        _player.GetComponent<PlayerController>().canMove = false;

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);

        _camera.m_Follow = _togglePlatform.transform;
        _togglePlatform.SetActive(true);
        yield return new WaitForSeconds(1);
        while (sr.color.a < 1)
        {
            sr.color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        _camera.m_Follow = _player.transform;
        _player.GetComponent<PlayerController>().canMove = true;
        _spriteRenderer.sprite = _endFrame;
    }
}
