using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        // débloquer le curseur
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void LaunchGame()
    {
        Debug.Log("Find");
        GameObject.Find("BootManager").GetComponent<BootManager>().ChangeScene("Menu", "Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
