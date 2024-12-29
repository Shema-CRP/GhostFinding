using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum EPauseMenu { Play, Menu, Option };
    EPauseMenu PlayerPauseMenu;

    public static GameManager Instance;
    GameObject Player;
    GameObject EndDoor;
    GameObject ExitDoor;
    DoorStateBehaviour DoorMonitor;
    PlayerState PlayerState;
    MonitorBehaviour MonitorBehaviourScript;
    byte NbTotalGenerator;
    byte NbActiveGenerator;
    int totalChild;

    GameObject PauseMenuScreen;
    GameObject PauseScreen;
    GameObject OptionScreen;
    GameObject[] SpeakersObject;
    SpeakerBehaviour[] Speakers;

    private void Awake()
    {
        if (Instance == null)
        Instance = this;
    }

    private void Start()
    {
        Player = GameObject.Find("Player");

        EndDoor = GameObject.Find("EndDoor");
        EndDoor.SetActive(false);

        ExitDoor = GameObject.Find("ExitDoor");

        PlayerState = Player.GetComponent<PlayerState>();

        DoorMonitor = GameObject.Find("DoorStateView").GetComponent<DoorStateBehaviour>();

        MonitorBehaviourScript = Player.transform.Find("Monitor").GetComponent<MonitorBehaviour>();

        // Count generators
        NbTotalGenerator = (byte) GameObject.Find("GeneratorsList").transform.childCount;
        NbActiveGenerator = 0;

        // Get Speakers scripts
        totalChild = GameObject.Find("SpeakersList").transform.childCount;
        SpeakersObject = new GameObject[totalChild];
        Speakers = new SpeakerBehaviour[totalChild];
        for (int i = 0; i < totalChild; i++)
        {
            SpeakersObject[i] = GameObject.Find("SpeakersList").transform.GetChild(i).gameObject;
            Speakers[i] = SpeakersObject[i].GetComponent<SpeakerBehaviour>();
        }

        PauseMenuScreen = Player.transform.Find("PlayerVision/Canvas/PauseMenuScreen").gameObject;
        PauseScreen = PauseMenuScreen.transform.Find("Background/PauseSection").gameObject;
        OptionScreen = PauseMenuScreen.transform.Find("Background/OptionSection").gameObject;
        PlayerPauseMenu = EPauseMenu.Play;

        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        // check if the player is alive or dead
        switch (PlayerState.PlayerLife)
        {
            case PlayerState.EPlayerLife.Live:
                break;
            case PlayerState.EPlayerLife.Dead:
                Player.GetComponent<PlayerMouvement>().enabled = false;
                Player.GetComponent<PlayerInteract>().enabled = false;
                Player.transform.Find("PlayerVision/Canvas/GameOverScreen").gameObject.SetActive(true);
                PauseGame();
                // débloquer le curseur
                DisplayCursor();
                break;
            case PlayerState.EPlayerLife.Escape:
                BootManager.Instance.ChangeScene("Game", "Menu");
                break;
            default:
                break;
        }

        // Charge the speakers each frames if they are uncharged
        for (int i = 0; i < totalChild; i++)
        {
            if (!Speakers[i].IsSoundPlaying())
                Speakers[i].Charging();
        }

        // charge the radar of monitor
        MonitorBehaviourScript.IncrementBattery();
    }

    public void PowerDoor()
    {
        NbActiveGenerator += 1;
        // update the door icon
        DoorMonitor.ChangeDisplayScreen(NbActiveGenerator,NbTotalGenerator);
        if (NbActiveGenerator >= NbTotalGenerator)
        {
            EndDoor.SetActive(true);
            ExitDoor.SetActive(false);
        }
    }

    /// <summary>
    /// Permet de naviguer de manière ergonomique dans le menu pause en apuyant sur echap
    /// </summary>
    public void PauseNavigate()
    {
        switch (PlayerPauseMenu)
        {
            case EPauseMenu.Play:
                DisplayPauseMenu();
                break;
            case EPauseMenu.Option:
                DisplayPauseMenu();
                break;
            case EPauseMenu.Menu:
                DisplayGameScene();
                break;
            default:
                DisplayPauseMenu();
                break;
        }
    }

    public void DisplayPauseMenu()
    {
        PauseGame();
        PauseMenuScreen.SetActive(true);
        PauseScreen.SetActive(true);
        OptionScreen.SetActive(false);
        PlayerPauseMenu = EPauseMenu.Menu;
        DisplayCursor();
    }

    public void DisplayOption()
    {
        PauseMenuScreen.SetActive(true);
        PauseScreen.SetActive(false);
        OptionScreen.SetActive(true);
        PlayerPauseMenu = EPauseMenu.Option;
    }

    public void DisplayGameScene()
    {
        ResumeGame();
        PauseMenuScreen.SetActive(false);
        PauseScreen.SetActive(false);
        OptionScreen.SetActive(false);
        PlayerPauseMenu = EPauseMenu.Play;
        HideCursor();
    }

    public void GoToMenu()
    {
        BootManager.Instance.ChangeScene("Game", "Menu");
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisplayCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}
