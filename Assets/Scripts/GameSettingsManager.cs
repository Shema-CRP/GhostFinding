using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    // Propriétés des paramètres
    private const string MouseSensitivityKey = "MouseSensitivity";
    private const string VolumeEffectsKey = "MusicVolume";
    // Valeurs par défaut
    private const float DefaultMouseSensitivity = 0.6f;
    private const float DefaultEffectsVolume = 0.5f;

    public float GetMouseSensivity()
    {
        return PlayerPrefs.GetFloat(MouseSensitivityKey, DefaultMouseSensitivity);
    }

    public float GetEffectVolume()
    {
        return PlayerPrefs.GetFloat(VolumeEffectsKey, DefaultEffectsVolume);
    }


    public void SaveMouseSensivity(float value)
    {
        PlayerPrefs.SetFloat(MouseSensitivityKey, value);
    }

    /// <summary>
    /// aplique le volume des effets sonores mème après la férmeture du jeu
    /// </summary>
    /// <param name="value">La valeur doit être entre 0.0 et 1.0</param>
    public void SaveEffectVolume(float value)
    {
        PlayerPrefs.SetFloat(VolumeEffectsKey, value);
    }
}
