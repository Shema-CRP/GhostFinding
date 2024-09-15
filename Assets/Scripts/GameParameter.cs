using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameParameter : MonoBehaviour
{
    public static GameParameter Instance;
    float SensibilitySliderValue;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    float PlayerSensibilityDefault;
    float GlobalVolumeDefault;

    public float PlayerSensibility;
    public float GeneralVolume;

    private void Start()
    {
        PlayerSensibilityDefault = 5.0f;
        GlobalVolumeDefault = 5.0f;
        SensibilitySliderValue = this.transform.Find("SensibilitySlider").GetComponent<Slider>().value;

        // if there are already a value in the player preference, use it else use default value
        if (PlayerPrefs.HasKey("PlayerSensibility"))
        {
            SensibilitySliderValue = PlayerPrefs.GetFloat("PlayerSensibility");
        }
        else
        {
            SensibilitySliderValue = PlayerSensibilityDefault;
        }

    }

    public void WritePlayerSensibility()
    {
        // save player preference in a save file
        PlayerPrefs.SetFloat("PlayerSensibility", SensibilitySliderValue);
        // updates sensibility
        PlayerState.Instance.PlayerCameraSensibility = SensibilitySliderValue;
    }
}
