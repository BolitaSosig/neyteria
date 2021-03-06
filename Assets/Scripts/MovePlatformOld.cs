using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformOld : MonoBehaviour
{

    public GameObject platformPathStart;
    public GameObject platformPathEnd;
    public int speed = 2;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private Vector3 aux;

    public bool invertPositions = false;

    public bool begin = true;
    public bool move = true;

    public bool isPlayer = false;
    private GameObject _player;


    // Start is called before the first frame update
    void Start()
    {
        platformPathStart = GetChildWithName(gameObject, "StartPath");
        platformPathEnd = GetChildWithName(gameObject, "EndPath");

        startPosition = platformPathStart.transform.position;
        endPosition = platformPathEnd.transform.position;

        _player = GameObject.FindGameObjectWithTag("Player");

        if (invertPositions) { aux = startPosition; startPosition = endPosition; endPosition = aux; } //Se invierten las start y end positions

        gameObject.transform.position = startPosition;
        //move = false;
        //begin = false;
        //StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
    }

    // Update is called once per frame
    void Update()
    {
        if (!begin && move)
        {
            move = false;
            StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
        }

        if (begin && move)
        {
            move = false;
            StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        }
    }


    //Lerp of platform movement

    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        Vector3 startPosition = obj.transform.position;
        float time = 0f;

        float timeMax = Vector3.Distance(startPosition, target) / speed;

        while (obj.transform.position != target && time < timeMax)
        {
            float prevX = obj.transform.position.x;

            obj.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;

            float postX = obj.transform.position.x;

            if (isPlayer) { _player.transform.position = new Vector3(_player.transform.position.x + (postX - prevX), _player.transform.position.y, _player.transform.position.z); } //El player sigue el movimiento de la plataforma

            yield return null;
        }

        begin = !begin;
        move = true;
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
