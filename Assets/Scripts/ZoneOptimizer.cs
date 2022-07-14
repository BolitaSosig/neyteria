using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneOptimizer : MonoBehaviour
{
    [SerializeField]
    private bool _zonePaused;
    public bool ZonePaused
    {
        get { return _zonePaused; }
        set
        {
            _zonePaused = value;
            BroadcastMessage("Pause", value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name.Equals("level1"))
            FindObjectOfType<SceneController>().SendMessage("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
