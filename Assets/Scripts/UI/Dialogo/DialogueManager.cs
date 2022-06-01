using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	private PlayerController _playerController;
	private SoundManager _audioSource;

	public Animator animator;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		_playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		_audioSource = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		_playerController.canMove = false;

		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		_audioSource.PlayAudioOneShot(8);
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		_audioSource.PlayAudioOneShot(9);
		

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSecondsRealtime(.01f);
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		_playerController.canMove = true;
	}

}
