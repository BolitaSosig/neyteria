using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private CameraController camera;
    [SerializeField] private Transform pointer;
    public bool active = false;


    private Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (active)
            Active2();
    }

    void Active()
    {
        float x = 2 * pointer.position.x - camera.transform.position.x;
        float y = 2 * pointer.position.y - camera.transform.position.y;
        Debug.Log("(" + x + ", " + y + ")");
        foreach (Transform t in transform)
        {
            float z = t.position.z;
            t.position = new Vector3(x + z, y + z, z);
        }
    }

    void Active2()
    {

    }
}
