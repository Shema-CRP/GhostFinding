using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject Player;
    GameObject EndDoor;
    GameObject ExitDoor;
    PlayerState PlayerState;
    MonitorBehaviour MonitorBehaviourScript;
    byte NbTotalGenerator;
    byte NbActiveGenerator;
    int totalChild;

    GameObject[] SpeakersObject;
    SpeakerBehaviour[] Speakers;

    private void Start()
    {
        Player = GameObject.Find("Player");

        EndDoor = GameObject.Find("EndDoor");
        EndDoor.SetActive(false);

        ExitDoor = GameObject.Find("ExitDoor");

        PlayerState = Player.GetComponent<PlayerState>();

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

                // débloquer le curseur
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
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
        Debug.Log("battery : " + NbActiveGenerator + " / " + NbTotalGenerator);
        if (NbActiveGenerator >= NbTotalGenerator)
        {
            EndDoor.SetActive(true);
            ExitDoor.SetActive(false);
        }
    }
}
