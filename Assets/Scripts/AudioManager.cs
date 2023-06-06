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
        // enclencher le son si il est eteind ou si le son jou� est diff�rent de celui demand�
        if (source.isPlaying == false || source.clip != sound)
        {
            source.clip = sound;
            source.Play();
        }
    }
}
