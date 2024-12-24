using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    // Propri�t�s des param�tres
    private const string MouseSensitivityKey = "MouseSensitivity";
    private const string VolumeEffectsKey = "MusicVolume";
    // Valeurs par d�faut
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
    /// aplique le volume des effets sonores m�me apr�s la f�rmeture du jeu
    /// </summary>
    /// <param name="value">La valeur doit �tre entre 0.0 et 1.0</param>
    public void SaveEffectVolume(float value)
    {
        PlayerPrefs.SetFloat(VolumeEffectsKey, value);
    }
}
