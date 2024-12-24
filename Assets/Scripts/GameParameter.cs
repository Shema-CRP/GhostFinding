using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameParameter : MonoBehaviour
{
    public static GameParameter Instance;
    GameSettingsManager Settings;
    Slider SensibilitySlider;
    Slider EffectVolumeSlider;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Settings = GameObject.Find("GameManager").GetComponent<GameSettingsManager>();
        SensibilitySlider = this.gameObject.transform.GetChild(0).GetComponent<Slider>();
        SensibilitySlider.value = Settings.GetMouseSensivity();
        EffectVolumeSlider = this.gameObject.transform.GetChild(1).GetComponent<Slider>();
        EffectVolumeSlider.value = Settings.GetEffectVolume();
    }


    // Les méthodes WritePlayerSensibility et WriteEffectVolume sont censé être appelé dans le Slider OnChange, mais j'ai pas réussi, donc voila un Update moche

    private void Update()
    {
        if (SensibilitySlider != null && EffectVolumeSlider != null)
        {
            // Si l'ecrant d'option est affiché
            if (this.gameObject.activeSelf)
            {
                if (SensibilitySlider.value != Settings.GetMouseSensivity())
                {
                    Settings.SaveMouseSensivity(SensibilitySlider.value);
                }
                if (EffectVolumeSlider.value != Settings.GetEffectVolume())
                {
                    Settings.SaveEffectVolume(EffectVolumeSlider.value);
                }
            }
        }
    }

    public void WritePlayerSensibility()
    {
        if (SensibilitySlider != null)
        {
            //Settings.SaveMouseSensivity(SensibilitySlider.value);
        }
    }

    public void WriteEffectVolume()
    {
        if (EffectVolumeSlider != null)
        {
            Debug.Log("VOLUME Valeur changée : " + SensibilitySlider.value);
            // Settings.SaveEffectVolume(EffectVolumeSlider.value);
        }
    }
}
