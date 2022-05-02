using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip aud1;
    public AudioClip aud2;
    public AudioClip[] audios = new AudioClip[5];

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioOneShot(int num)
    {
        audioSource.PlayOneShot(audios[num]);
    }

    public void PlayAudioLooping(int num)
    {
        audioSource.PlayOneShot(audios[num]);
        audioSource.loop = true;
    }


}
