using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void DiffuseSound(AudioSource source, AudioClip sound)
    {
        // activate the sound if it is off or if the sound played is different from that requested
        if (source.isPlaying == false || source.clip != sound)
        {
            source.clip = sound;
            source.Play();
        }
    }
}
