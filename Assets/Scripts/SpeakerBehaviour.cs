using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerBehaviour : MonoBehaviour
{
    public enum ESpeakerState { Ready, InUse, Charging, Desactivated };
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip diffuser;

    void Start()
    {
        sound = (AudioSource) Resources.Load("Sounds/SoundsEffects/speakerSound.wav");
        diffuser = this.transform.Find("body/Cube").GetComponent<AudioClip>();
    }

    public void LaunchSound()
    {
        AudioManager.Instance.DiffuseSound(sound, diffuser);
    }

    public void Charging()
    {

    }
}
