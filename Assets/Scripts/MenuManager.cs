using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        // d�bloquer le curseur
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void LaunchGame()
    {
        Debug.Log("Find");
        BootManager.Instance.ChangeScene("Menu", "Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
