using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;
    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + new Vector3(0,2), ref _velocity, smoothTime);
    }

    /* Esto hace que se centre m�s la c�mara en el eje y. Si subes, mirar� m�s para abajo. Si bajas, mirar� m�s hacia arriba.
    public float alpha = 0.3f;
    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, (1 - alpha) * target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + (alpha) * new Vector3(0, 2), ref _velocity, smoothTime);
    }*/
}
