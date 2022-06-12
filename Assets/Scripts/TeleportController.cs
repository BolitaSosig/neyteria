using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform[] nexo1_nexo2;
    public Transform[] nexo2_nexo3;
    public Transform[] nexo3_nexo4;

    private CameraController cam;
    private MovePlatformScript pointer;
    private bool teleporting;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraController>();
        pointer = GetComponent<MovePlatformScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Teleport(NexoCentralController.Nexo from, NexoCentralController.Nexo to)
    {
        switch(from)
        {
            case NexoCentralController.Nexo.Nexo_1_2:
                switch(to)
                {
                    case NexoCentralController.Nexo.Nexo_2_3:
                        StartCoroutine(Teleport(nexo1_nexo2, GameObject.Find("Tubos_1-2").transform));
                        break;
                }
                break;
            case NexoCentralController.Nexo.Nexo_2_3:
                switch (to)
                {
                    case NexoCentralController.Nexo.Nexo_1_2:
                       TeleportReverse(nexo1_nexo2, GameObject.Find("Tubos_1-2").transform);
                        break;
                }
                break;
            case NexoCentralController.Nexo.Nexo_3_3:
                //StartCoroutine(Teleport(nexo3_nexo4));
                break;
        }
    }

    private IEnumerator Teleport(Transform[] t, Transform tubos)
    {
        if (!teleporting)
        {
            t[0].GetComponentInChildren<NexoCentralController>().teleporting = true;
            t[t.Length - 1].GetComponentInChildren<NexoCentralController>().teleporting = true;
            StartCoroutine(ActivateTubos(tubos, true));
            pointer.transform.position = t[0].position;
            StartCoroutine(cam.CinematicaTeletransporte(true));

            for (int i = 1; i < t.Length; i++)
            {
                yield return new WaitUntil(() => pointer.transform.position == t[i - 1].position);
                pointer.startPos = t[i - 1];
                pointer.endPos = t[i];
                pointer.move = true;
                if(i == 1)
                {
                    yield return new WaitForSecondsRealtime((Vector3.Distance(t[i - 1].position, t[i].position) / pointer.speed) / 2f);
                    FindObjectOfType<PlayerController>().transform.position = t[t.Length - 1].position;
                }
            }
            
            yield return new WaitUntil(() => pointer.transform.position == t[t.Length - 1].position);
            StartCoroutine(cam.CinematicaTeletransporte(false));
            StartCoroutine(ActivateTubos(tubos, false));
            t[0].GetComponentInChildren<NexoCentralController>().teleporting = false;
            t[t.Length - 1].GetComponentInChildren<NexoCentralController>().teleporting = false;
        }
    }
    
    private void TeleportReverse(Transform[] t, Transform tubos)
    {
        ArrayList l = new ArrayList(t);
        l.Reverse();
        StartCoroutine(Teleport((Transform[]) l.ToArray(), tubos));
    }

    IEnumerator ActivateTubos(Transform parent, bool activate)
    {
        foreach(Animator a in parent.GetComponentsInChildren<Animator>())
        {
            a.SetBool("teleporting", activate);
            yield return null;
        }
    }
}
