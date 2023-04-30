using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootManager : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("BootManager/Canvas/Fade").GetComponent<Animator>();
        anim.SetBool("HideScreen", false);
        SceneManager.LoadScene("Splashscreen", LoadSceneMode.Additive);
    }

    public void ChangeScene(string nameOfCurrentScene, string nameOfNewScene)
    {
        Debug.Log("change");
        //anim.SetBool("HideScreen", true);
        SceneManager.LoadScene(nameOfNewScene, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(nameOfCurrentScene);
        //anim.SetBool("HideScreen", false);
    }

    // This method is used only by the quit button of the death screen
    public void ChangeScene()
    {
        //anim.SetBool("HideScreen", true);
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Game");
        //anim.SetBool("HideScreen", false);
    }
    // This method is used only by the restart button of the death screen
    public void ReloadScene()
    {
        SceneManager.LoadScene("Transition", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Game");
    }
}
