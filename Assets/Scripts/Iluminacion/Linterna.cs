using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SeguirPlayer();
    }

    void SeguirPlayer()
    {
        transform.position = _player.transform.position + new Vector3(-0.1f * Mathf.Sign(_player.transform.localScale.x), 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sign(_player.transform.localScale.x)  * -90);
    }
}
