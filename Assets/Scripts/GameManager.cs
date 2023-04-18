using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    // Update is called once per frame
    void Update()
    {
        // check if the player is alive or dead
        switch (Player.GetComponent<PlayerState>().PlayerLife)
        {
            case PlayerState.EPlayerLife.Live:
                break;
            case PlayerState.EPlayerLife.Dead:
                break;
            case PlayerState.EPlayerLife.Escape:
                break;
            default:
                break;
        }

        // check the state of generator to open the exit door
    }
}
