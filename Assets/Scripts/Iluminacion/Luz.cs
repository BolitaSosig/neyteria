using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Luz : MonoBehaviour
{
    [SerializeField] public Light2D _globalLight;
    [SerializeField] public Light2D _linterna;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLightBetweenZones()
    {
        if(GLOBAL.zona.Equals("CollisionNivel1"))
        {
            _linterna.gameObject.SetActive(true);
            _globalLight.color = Color.black;
        } else
        {
            _linterna.gameObject.SetActive(false);
            _globalLight.color = Color.white;
        }
    }
}
