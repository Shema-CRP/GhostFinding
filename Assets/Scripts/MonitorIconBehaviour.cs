using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorIconBehaviour : MonoBehaviour
{
    Animator animController;

    // Start is called before the first frame update
    void Start()
    {
        animController = GameObject.Find("Ghost/MonitorIcon").gameObject.GetComponent<Animator>();
    }

    public void EndRevealAnimation()
    {
        animController.SetBool("radarActive", false);
    }
}
