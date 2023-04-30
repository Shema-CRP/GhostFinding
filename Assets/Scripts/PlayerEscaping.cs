using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscaping : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerState>().PlayerLife = PlayerState.EPlayerLife.Escape;
        }
    }
}
