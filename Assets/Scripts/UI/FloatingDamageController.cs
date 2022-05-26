using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingDamageController : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (text.fontSize == 0)
            Destroy(gameObject);
    }
}
