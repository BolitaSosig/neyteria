using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip[] audios = new AudioClip[5];

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
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

    public void StopMusic()
    {
        audioSource.Stop();
    }


}
