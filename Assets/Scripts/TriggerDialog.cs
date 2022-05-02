using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collision");
        if (collision.CompareTag("Player"))
        {   
            //GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartDialogue(dialogue);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            print("collision-start-dialogue");
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collision");
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }*/
}
