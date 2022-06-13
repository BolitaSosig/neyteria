using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public float PLAYER_STAY_SIZE = 5f;
    public float PLAYER_MOV_SIZE = 6.5f;
    public float CINEMATIC_SIZE = 6.5f;

    private CinemachineVirtualCamera _cam;
    private Rigidbody2D _playerBody;

    private bool resizing = false;
    private bool cinematic = false;

    private void Start()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
        _playerBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();

        
    }

    void Update()
    {
        CinematicFadeSize();
        if(!resizing && !cinematic)
            PlayerMovimentFadeSize();
    }

    void PlayerMovimentFadeSize()
    {
        if (_playerBody.velocity.magnitude == 0 && _cam.m_Lens.OrthographicSize != PLAYER_STAY_SIZE)
            StartCoroutine(ResizeReduce(PLAYER_STAY_SIZE, 0.05f));
        else if (_playerBody.velocity.magnitude > 0.1f && _cam.m_Lens.OrthographicSize != PLAYER_MOV_SIZE)
            StartCoroutine(ResizeGrow(PLAYER_MOV_SIZE, 0.05f));
    }
    void CinematicFadeSize()
    {
        if (cinematic && _cam.m_Lens.OrthographicSize != CINEMATIC_SIZE)
            StartCoroutine(ResizeGrow(CINEMATIC_SIZE, 0.08f));
    }

    public IEnumerator ResizeGrow(float size, float speed)
    {
        resizing = true;
        float var = size - _cam.m_Lens.OrthographicSize;

        _cam.m_Lens.OrthographicSize += speed * Mathf.Sqrt((1.51f - var) / (size - PLAYER_STAY_SIZE + 0.01f));
        _cam.m_Lens.OrthographicSize = Mathf.Min(_cam.m_Lens.OrthographicSize, size);
        yield return new WaitForSeconds(0.01f);
        resizing = false;
    } 

    public IEnumerator ResizeReduce(float size, float speed)
    {
        resizing = true;
        float var = _cam.m_Lens.OrthographicSize - size;

        _cam.m_Lens.OrthographicSize -= speed * Mathf.Sqrt((1.51f - var) / (PLAYER_MOV_SIZE - size + 0.01f));
        _cam.m_Lens.OrthographicSize = Mathf.Max(_cam.m_Lens.OrthographicSize, size);
        yield return new WaitForSeconds(0.01f);
        resizing = false;
    }

    public IEnumerator CinematicaInterruptor(GameObject player, GameObject togglePlatform, Tilemap tm, bool show)
    {
        cinematic = true;
        player.GetComponent<PlayerController>().canMove = false;
        if(GLOBAL.zona.Equals("CollisionNivel1"))
            GameObject.Find("Global_Plano").GetComponent<Light2D>().color = Color.white;

        if (show) { 
            tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, 0f);

            _cam.m_Follow = togglePlatform.transform;
            togglePlatform.SetActive(true);
            yield return new WaitForSeconds(1);
            while (tm.color.a < 1)
            {
                tm.color += new Color(0, 0, 0, 0.1f);
                yield return new WaitForSeconds(0.1f);
            }
        } else
        {
            tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, 1f);

            _cam.m_Follow = togglePlatform.transform;
            yield return new WaitForSeconds(1);
            while (tm.color.a > 0)
            {
                tm.color -= new Color(0, 0, 0, 0.1f);
                yield return new WaitForSeconds(0.1f);
            }
            togglePlatform.SetActive(false);
        }


        if (GLOBAL.zona.Equals("CollisionNivel1"))
            GameObject.Find("Global_Plano").GetComponent<Light2D>().color = Color.black;
        _cam.m_Follow = player.transform;
        player.GetComponent<PlayerController>().canMove = true;
        cinematic = false;
    }

    IEnumerator CinematicaInterruptor(GameObject player, GameObject togglePlatform, SpriteRenderer sr, bool show)
    {
        cinematic = true;
        player.GetComponent<PlayerController>().canMove = false;
        if (GLOBAL.zona.Equals("CollisionNivel1"))
            GameObject.Find("Global_Plano").GetComponent<Light2D>().color = Color.white;

        if (show)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);

            _cam.m_Follow = togglePlatform.transform;
            togglePlatform.SetActive(true);
            yield return new WaitForSeconds(1);
            while (sr.color.a < 1)
            {
                sr.color += new Color(0, 0, 0, 0.1f);
                yield return new WaitForSeconds(0.1f);
            }
        } else
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);

            _cam.m_Follow = togglePlatform.transform;
            yield return new WaitForSeconds(1);
            while (sr.color.a > 0)
            {
                sr.color -= new Color(0, 0, 0, 0.1f);
                yield return new WaitForSeconds(0.1f);
            }
            togglePlatform.SetActive(false);
        }


        if (GLOBAL.zona.Equals("CollisionNivel1"))
            GameObject.Find("Global_Plano").GetComponent<Light2D>().color = Color.black;
        _cam.m_Follow = player.transform;
        player.GetComponent<PlayerController>().canMove = true;
        cinematic = false;
    }

    public IEnumerator CinematicaInterruptorSimple(GameObject player, GameObject togglePlatform)
    {
        cinematic = true;
        player.GetComponent<PlayerController>().canMove = false;
        if (GLOBAL.zona.Equals("CollisionNivel1"))
            GameObject.Find("Global_Plano").GetComponent<Light2D>().color = Color.white;


        _cam.m_Follow = togglePlatform.transform;
        yield return new WaitForSeconds(2);


        if (GLOBAL.zona.Equals("CollisionNivel1"))
            GameObject.Find("Global_Plano").GetComponent<Light2D>().color = Color.black;
        _cam.m_Follow = player.transform;
        player.GetComponent<PlayerController>().canMove = true;
        cinematic = false;
    }

    public IEnumerator CinematicaTeletransporte(bool on)
    {
        if(on)
        {
            _cam.m_Follow = GameObject.Find("TpPointer").transform;
        } else
        {
            _cam.m_Follow = _playerBody.transform;
        }
        yield return null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionNivel"))
            FindObjectOfType<SceneController>().EnterCollisionNivel(collision.name);
    }
}
