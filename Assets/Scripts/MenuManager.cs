using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    GameObject tutoScreen;

    private void Start()
    {
        // play menu jingle
        AudioClip clip = (AudioClip) Resources.Load("Sounds/SoundsEffects/pianoMenuSound");
        AudioSource source = GameObject.Find("Camera").GetComponent<AudioSource>();
        AudioManager.Instance.DiffuseSound(source, clip);
        // débloquer le curseur
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        tutoScreen = GameObject.Find("TutorialScreen");
        UndisplayTutorial();
    }

    public void LaunchGame()
    {
        Debug.Log("Find");
        BootManager.Instance.ChangeScene("Menu", "Game");
    }

    public void DisplayTutorial()
    {
        if (tutoScreen != null)
        {
            tutoScreen.SetActive(true);
        }
        else
        {
            Debug.LogError("Tutorial not found");
        }
    }

    public void UndisplayTutorial()
    {
        if (tutoScreen != null)
        {
            tutoScreen.SetActive(false);
        }
        else
        {
            Debug.LogError("Tutorial not found");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
