using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiController : MonoBehaviour
{
    private Animator _animator;
    private SoundManager _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetTrigger("open");
            _audioSource.PlayAudioOneShot(14);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetTrigger("close");
            _audioSource.PlayAudioOneShot(15);
        }
    }
}
