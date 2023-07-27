using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStateBehaviour : MonoBehaviour
{
    Sprite EmptyBattery;
    Sprite Battery25P;
    Sprite Battery50P;
    Sprite Battery75P;
    Sprite FullBattery;

    GameObject screen;

    void Start()
    {
        screen = this.transform.Find("ScreenBackground/ScreenDisplay").gameObject;
        EmptyBattery = Resources.Load<Sprite>("Sprites/BatteryEmpty");
        Battery25P = Resources.Load<Sprite>("Sprites/Battery1bar");
        Battery50P = Resources.Load<Sprite>("Sprites/Battery2bar");
        Battery75P = Resources.Load<Sprite>("Sprites/Battery3bar");
        FullBattery = Resources.Load<Sprite>("Sprites/BatteryFull");
    }

    /// <summary>
    /// calculate the pourcentage of energy and display the good icon
    /// </summary>
    /// <param name="currentEnergy"></param>
    /// <param name="maxEnergy"></param>
    public void ChangeDisplayScreen(int currentEnergy, int maxEnergy)
    {
        Sprite icon = null;
        float percentResult = currentEnergy * 100 / maxEnergy;
        if (percentResult >= 100)
        {
            icon = FullBattery;
        }
        else if (percentResult >= 75)
        {
            icon = Battery75P;
        }
        else if (percentResult >= 50)
        {
            icon = Battery50P;
        }
        if (percentResult >= 10)
        {
            icon = Battery25P;
        }
        else
        {
            icon = EmptyBattery;
        }
        screen.GetComponent<SpriteRenderer>().sprite = icon;
    }
}
