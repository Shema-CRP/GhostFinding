using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float scope;
    GameObject vision;
    bool lastFrameInterupt;
    GameObject lastGeneratorGetted;
    Vector3 StartPoint;

    private void Start()
    {
        vision = GameObject.Find("PlayerVision");
    }


    void Update()
    {
        StartPoint = vision.transform.position;

        Ray ray = new Ray(StartPoint, vision.transform.forward);
        bool isContact = Physics.Raycast(ray, out RaycastHit hitObject, scope);

        if (isContact)
        {
            if (hitObject.collider.CompareTag("ToolGenerator"))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    hitObject.collider.gameObject.GetComponent<GeneratorBehaviour>().InterruptEnergy();
                    lastFrameInterupt = false;
                }

                if (Input.GetMouseButton(0))
                {
                    hitObject.collider.gameObject.GetComponent<GeneratorBehaviour>().IncrementEnergy();
                    lastFrameInterupt = true;
                    lastGeneratorGetted = hitObject.collider.gameObject;
                }
            }
            // verify if the player hold the generator but suddenly doesn't target it, to interrupt it
            else if (lastFrameInterupt)
            {
                lastGeneratorGetted.GetComponent<GeneratorBehaviour>().InterruptEnergy();
                lastFrameInterupt = false;
            }
        }
    }
}
