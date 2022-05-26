using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterDialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	public bool onetime = true;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) { FindObjectOfType<DialogueManager>().StartDialogue(dialogue); Quitar(); }
	}

    private void Quitar()
    {
		if (onetime)
		{
			Destroy(gameObject);
		}
    }



}
