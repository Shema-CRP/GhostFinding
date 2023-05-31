using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float scope;
    GameObject vision;
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
            if (Input.GetMouseButton(0))
            {
                if (hitObject.collider.CompareTag("ToolGenerator"))
                {
                    hitObject.collider.gameObject.GetComponent<GeneratorBehaviour>().IncrementEnergy();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (hitObject.collider.CompareTag("ToolGenerator"))
                {
                    hitObject.collider.gameObject.GetComponent<GeneratorBehaviour>().InterruptEnergy();
                }
            }
        }
    }
}
