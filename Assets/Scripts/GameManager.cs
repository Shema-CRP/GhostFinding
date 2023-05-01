using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject Player;
    GameObject EndDoor;
    GameObject ExitDoor;
    byte NbTotalGenerator;
    byte NbActiveGenerator;

    private void Start()
    {
        Player = GameObject.Find("Player");

        EndDoor = GameObject.Find("EndDoor");
        EndDoor.SetActive(false);

        ExitDoor = GameObject.Find("ExitDoor");

        // Count generators
        NbTotalGenerator = (byte) GameObject.Find("GeneratorList").transform.childCount;
        NbActiveGenerator = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // check if the player is alive or dead
        switch (Player.GetComponent<PlayerState>().PlayerLife)
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
                GameObject.Find("BootManager").GetComponent<BootManager>().ChangeScene("Game", "Menu");
                break;
            default:
                break;
        }
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
