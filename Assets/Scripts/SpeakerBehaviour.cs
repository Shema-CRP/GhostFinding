using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeakerBehaviour : MonoBehaviour
{
    public enum ESpeakerState { Ready, InUse, Charging, Desactivated };
    [SerializeField] ESpeakerState CurrentState;
    [SerializeField] AudioClip sound;
    [SerializeField] AudioSource diffuser;
    [SerializeField] int ChargingPeak;
    [SerializeField] int CurrentCharge;
    GameObject Logo;
    GameObject Noise;
    MeshRenderer CameraLogoColor;

    void Start()
    {
        sound = (AudioClip) Resources.Load("Sounds/SoundsEffects/speakerSound");
        diffuser = this.transform.Find("body/Cube").GetComponent<AudioSource>();
        CurrentState = ESpeakerState.Ready;
        Logo = this.transform.Find("CameraViewLogo").gameObject;
        CameraLogoColor = Logo.GetComponent<MeshRenderer>();
        CameraLogoColor.materials[0].color = Color.green;
        CurrentCharge = ChargingPeak;
        Noise = this.transform.Find("Noise").gameObject;
    }

    /// <summary>
    /// Execute the sound of speaker if he is ready
    /// </summary>
    public void LaunchSound()
    {
        if (CurrentState == ESpeakerState.Ready)
        {
            CurrentState = ESpeakerState.InUse;
            CameraLogoColor.materials[0].color = Color.yellow;
            Noise.SetActive(true);
            AudioManager.Instance.DiffuseSound(diffuser, sound);
        }
        else
        {
            Debug.Log("Speaker not ready");
        }
    }

    /// <summary>
    /// Change the state of the speaker at the end of the sound
    /// </summary>
    public void InterruptSound()
    {
        CurrentCharge = 0;
        CurrentState = ESpeakerState.Charging;
        CameraLogoColor.materials[0].color = Color.red;
        Noise.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsSoundPlaying()
    {
        if (diffuser.isPlaying)
        {
            return true;
        }
        // After the sound is stopped, turn the state of this speaker in charging mode
        if (CurrentState == ESpeakerState.InUse)
        {
            InterruptSound();
        }
        return false;
    }

    public void Charging()
    {
        CurrentCharge++;
        if (CurrentCharge >= ChargingPeak)
        {
            CurrentState = ESpeakerState.Ready;
            CameraLogoColor.materials[0].color = Color.green;
            CurrentCharge = ChargingPeak;
        }
    }
}
