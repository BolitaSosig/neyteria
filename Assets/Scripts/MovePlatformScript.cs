using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformScript : MonoBehaviour
{

    public Transform startPos;
    public Transform endPos;
    public float speed = 1f;
    public float startFadeTime;
    public float endFadeTime;
    public bool loop;

    private Vector3 startCoord;
    private Vector3 endCoord;

    public bool begin = true;
    public bool move = false;
    public bool fading = false;

    public bool isPlayer = false;
    private GameObject _player;


    // Start is called before the first frame update
    void Start()
    {
        startCoord = startPos.position;
        endCoord = endPos.position;

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (startCoord != startPos.position)
            startCoord = startPos.position;
        if (endCoord != endPos.position)
            endCoord = endPos.position;
        if (!begin && move)
        {
            move = false;
            StartCoroutine(Vector3LerpCoroutine(gameObject, startCoord));
        }

        if (begin && move)
        {
            move = false;
            StartCoroutine(Vector3LerpCoroutine(gameObject, endCoord));
        }
    }


    IEnumerator FadePlatform(Vector3 startC, Vector3 endC, float tf, bool accel)
    {
        float time = 0f;
        float var = speed / (tf * tf);
        Vector3 dir = Vector3.Normalize(endC - startC);
        Vector3 end = startC + tf * speed * dir;

        while (time < tf)
        {
            float func = accel ? time * time * var : speed - (tf - time) * (tf - time) * var;
            transform.position = Vector3.Lerp(startC, end, func / speed);
                //Vector3.SmoothDamp(startC, end, ref vel, tf, 20 /*speed * accel.CompareTo(false)*/);
            time += Time.deltaTime;
            yield return null;
        }
        fading = false;
    }

    //Lerp of platform movement

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target)
    {
        fading = startFadeTime > 0f;
        Vector3Fade(obj, target, startFadeTime, true);
        yield return new WaitUntil(() => !fading);

        Vector3 startPosition = startCoord;
        float time = startFadeTime;

        float timeMax = Vector3.Distance(startPosition, target) / speed;

        while (obj.transform.position != target && time < (timeMax - endFadeTime) && Vector3.Distance(obj.transform.position, target) > endFadeTime)
        {
            float prevX = obj.transform.position.x;

            obj.transform.position = Vector3.Lerp(startPosition, target, time / timeMax);
            time += Time.deltaTime;

            float postX = obj.transform.position.x;

            if (isPlayer) { _player.transform.position = new Vector3(_player.transform.position.x + (postX - prevX), _player.transform.position.y, _player.transform.position.z); } //El player sigue el movimiento de la plataforma

            yield return null;
        }

        fading = endFadeTime > 0f;
        Vector3Fade(obj, target, endFadeTime, false);
        yield return new WaitUntil(() => !fading);
        obj.transform.Translate(target - obj.transform.position);

        begin = !begin;
        move = loop;
    }

    void Vector3Fade(GameObject obj, Vector3 target, float tf, bool accel)
    {
        if (fading)
            StartCoroutine(FadePlatform(obj.transform.position, target, tf, accel));
    }


    //Begin the movement of the platform
    public void InvertActivatedDesactivated()
    {
        move = !move;
    }


    //Collider detections

    /*void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        { isPlayer = true; }
    }


    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        { isPlayer = false; }
    }*/

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        { isPlayer = true; }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        { isPlayer = false; }
    }


    //Encontar hijos por su nombre

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

}
