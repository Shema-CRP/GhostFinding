using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    // Cette transition existe car je n'ai pas trouv� d'autre moyen simple de recharger la sc�ne de jeu
    void Awake()
    {
        GameObject.Find("BootManager").GetComponent<BootManager>().ChangeScene("Transition", "Game");
    }
}
