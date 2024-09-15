using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionManager : MonoBehaviour
{
    bool ReadKey;
    public PlayerInput playerInput;

    void Start()
    {
        ReadKey = false;
    }

    // Reads the player's input when he changes his key controls
    void Update()
    {
        if (ReadKey)
        {
            // playerInput.actions
        }
    }

    /// <summary>
    /// In option, the player will activate
    /// </summary>
    public void DecideToChangeKey()
    {
        ReadKey = true;
    }

    public void SaveSensibility()
    {
        PlayerPrefs.GetFloat("PlayerSensibility", 1);
    }

    public void UpdateKey()
    {

    }
}
