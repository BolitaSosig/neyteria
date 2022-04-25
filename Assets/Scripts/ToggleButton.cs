using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject _togglePlatform;
    [SerializeField] private Sprite _endFrame;
    public bool _activated = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Animator>().SetBool("isToggled", _activated);
        if(_activated)
            gameObject.GetComponent<SpriteRenderer>().sprite = _endFrame;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!_activated && collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.Space))
            Toggle();
    }

    void Toggle()
    {
        Debug.Log("sos"); 
        _activated = true;
        gameObject.GetComponent<Animator>().SetBool("trigger", _activated);
        _togglePlatform.SetActive(true);
        //gameObject.GetComponent<Animator>().Play("togglebutton");

    }
}
