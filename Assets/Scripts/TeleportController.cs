using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform[] nexo1_nexo2;
    public Transform[] nexo1_nexo3;
    public Transform[] nexo2_nexo3;
    public Transform[] nexo4_nexo1;
    public Transform[] nexo4_nexo2;
    public Transform[] nexo4_nexo3;
    [Space]
    public Transform tubos_1_2;
    public Transform tubos_2_3;
    public Transform tubos_4_1;

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
                switch (to)
                {
                    case NexoCentralController.Nexo.Nexo_2_3:
                        StartCoroutine(Teleport(nexo1_nexo2, new Transform[] { tubos_1_2 }));
                        break;
                    case NexoCentralController.Nexo.Nexo_4_4:
                        TeleportReverse(nexo4_nexo1, new Transform[] { tubos_4_1 });
                        break;
                    case NexoCentralController.Nexo.Nexo_3_3:
                        StartCoroutine(Teleport(nexo1_nexo3, new Transform[] { tubos_1_2, tubos_2_3}));
                        break;
                }
                break;
            case NexoCentralController.Nexo.Nexo_2_3:
                switch (to)
                {
                    case NexoCentralController.Nexo.Nexo_1_2:
                        TeleportReverse(nexo1_nexo2, new Transform[] { tubos_1_2 });
                        break;
                    case NexoCentralController.Nexo.Nexo_3_3:
                        StartCoroutine(Teleport(nexo2_nexo3, new Transform[] { tubos_2_3 }));
                        break;
                    case NexoCentralController.Nexo.Nexo_4_4:
                        TeleportReverse(nexo4_nexo2, new Transform[] { tubos_1_2, tubos_4_1});
                        break;
                }
                break;
            case NexoCentralController.Nexo.Nexo_3_3:
                switch (to)
                {
                    case NexoCentralController.Nexo.Nexo_2_3:
                        TeleportReverse(nexo2_nexo3, new Transform[] { tubos_2_3 });
                        break;
                    case NexoCentralController.Nexo.Nexo_1_2:
                        TeleportReverse(nexo1_nexo3, new Transform[] { tubos_2_3, tubos_1_2 });
                        break;
                    case NexoCentralController.Nexo.Nexo_4_4:
                        TeleportReverse(nexo4_nexo3, new Transform[] { tubos_2_3, tubos_1_2, tubos_4_1 });
                        break;
                }
                break;
            case NexoCentralController.Nexo.Nexo_4_4:
                switch (to)
                {
                    case NexoCentralController.Nexo.Nexo_1_2:
                        StartCoroutine(Teleport(nexo4_nexo1, new Transform[] { tubos_4_1 }));
                        break;
                    case NexoCentralController.Nexo.Nexo_2_3:
                        StartCoroutine(Teleport(nexo4_nexo2, new Transform[] { tubos_4_1, tubos_1_2 }));
                        break;
                    case NexoCentralController.Nexo.Nexo_3_3:
                        StartCoroutine(Teleport(nexo4_nexo3, new Transform[] { tubos_4_1, tubos_1_2, tubos_2_3 }));
                        break;
                }
                break;
        }
    }

    private IEnumerator Teleport(Transform[] t, Transform[] tubos)
    {
        if (!teleporting)
        {
            teleporting = true;
            t[0].GetComponentInChildren<NexoCentralController>().teleporting = true;
            t[t.Length - 1].GetComponentInChildren<NexoCentralController>().teleporting = true;
            StartCoroutine(ActivateTubos(tubos));
            pointer.transform.position = t[0].position;
            StartCoroutine(cam.CinematicaTeletransporte(true));

            for (int i = 1; i < t.Length; i++)
            {
                yield return new WaitUntil(() => pointer.transform.position == t[i - 1].position);
                pointer.startPos = t[i - 1];
                pointer.endPos = t[i];
                pointer.move = true;
                pointer.begin = true;
                if(i == 1)
                {
                    yield return new WaitForSecondsRealtime((Vector3.Distance(t[i - 1].position, t[i].position) / pointer.speed) / 2f);
                    FindObjectOfType<PlayerController>().transform.position = t[t.Length - 1].position;
                }
            }
            
            yield return new WaitUntil(() => pointer.transform.position == t[t.Length - 1].position);
            t[0].GetComponentInChildren<NexoCentralController>().teleporting = false;
            t[t.Length - 1].GetComponentInChildren<NexoCentralController>().teleporting = false;
            StartCoroutine(cam.CinematicaTeletransporte(false));
            teleporting = false;
            StartCoroutine(ActivateTubos(tubos));
        }
    }
    
    private void TeleportReverse(Transform[] t, Transform[] tubos)
    {
        ArrayList l = new ArrayList(t);
        l.Reverse();
        Transform[] tr = new Transform[l.Count];
        l.CopyTo(tr);
        StartCoroutine(Teleport(tr, tubos));
    }

    IEnumerator ActivateTubos(Transform[] parent)
    {
        foreach(Transform t in parent)
            foreach(Animator a in t.GetComponentsInChildren<Animator>())
            {
                a.SetBool("teleporting", teleporting);
                yield return null;
            }
    }
}
