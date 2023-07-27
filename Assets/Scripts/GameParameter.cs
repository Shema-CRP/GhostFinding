using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameter : MonoBehaviour
{
    GameParameter Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    KeyCode EInputForwardDefault;
    KeyCode EInputBackDefault;
    KeyCode EInputLeftDefault;
    KeyCode EInputRightDefault;
    KeyCode EInputInteractDefault;
    KeyCode EInputMonitorDefault;
    KeyCode EInputBait1Default;
    KeyCode EInputBait2Default;
    KeyCode EInputBait3Default;
    KeyCode EInputBait4Default;
    KeyCode EInputRadarDefault;
    float SensibilityDefault;
    float VolumeDefault;

    public KeyCode EInputForward;
    public KeyCode EInputBack;
    public KeyCode EInputLeft;
    public KeyCode EInputRight;
    public KeyCode EInputInteract;
    public KeyCode EInputMonitor;
    public KeyCode EInputBait1;
    public KeyCode EInputBait2;
    public KeyCode EInputBait3;
    public KeyCode EInputBait4;
    public KeyCode EInputRadar;
    public float Sensibility;
    public float Volume;

    private void Start()
    {
        EInputForwardDefault = KeyCode.W;
        EInputLeftDefault = KeyCode.A;
        EInputBackDefault = KeyCode.S;
        EInputRightDefault = KeyCode.D;
        EInputInteractDefault = KeyCode.Mouse0;
        EInputMonitorDefault = KeyCode.Space;
        EInputRadarDefault = KeyCode.Q;
        EInputBait1Default = KeyCode.Alpha1;
        EInputBait2Default = KeyCode.Alpha2;
        EInputBait3Default = KeyCode.Alpha3;
        EInputBait4Default = KeyCode.Alpha4;
        SensibilityDefault = 5.0f;
        VolumeDefault = 5.0f;
    }
}
