using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformScript : MonoBehaviour
{

    public GameObject platformPathStart;
    public GameObject platformPathEnd;
    public int speed;
    public Vector3 startPosition;
    public Vector3 endPosition;

    public bool begin = true;
    public bool move = true;

    //public bool isPlayer = false;
    //private GameObject _player;


    // Start is called before the first frame update
    void Start()
    {
        platformPathStart = GetChildWithName(gameObject, "StartPath");
        platformPathEnd = GetChildWithName(gameObject, "EndPath");

        startPosition = platformPathStart.transform.position;
        endPosition = platformPathEnd.transform.position;

        //_player = GameObject.FindGameObjectWithTag("Player");


        move = false;
        begin = false;
        StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
    }

    // Update is called once per frame
    void Update()
    {
        if (begin && move)
        {
            move = false;
            StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
        }

        if (!begin && move)
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
            obj.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed);
            time += Time.deltaTime;

            //if (isPlayer) { _player.transform.position = Vector3.Lerp(startPosition, target, (time / Vector3.Distance(startPosition, target)) * speed); }
            yield return null;
        }

        begin = !begin;
        move = true;
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
