using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private CameraController camera;
    [SerializeField] private Transform pointer;
    public PolygonCollider2D zone_collider;

    public bool active 
    { 
        get { return GLOBAL.zona.Equals(zone_collider.name); }
    }


    private Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<CameraController>();
        prevPos = pointer.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (active)
            Active();
        else 
        {
            foreach (Transform t in transform)
            {
                t.position = new Vector3(t.parent.position.x, t.parent.position.y, t.position.z);
            }
        }
    }

    void Active()
    {
        float deltaX = camera.transform.position.x - prevPos.x;
        float deltaY = camera.transform.position.y - prevPos.y;
        foreach (Transform t in transform)
        {
            float v = t.position.z / transform.childCount;
            t.Translate(new Vector3(v * deltaX, v * deltaY, 0));
        }
        prevPos = camera.transform.position;
    }

    void Deactive()
    {

    }
}
