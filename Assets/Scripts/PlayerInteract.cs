using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] int scope;
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


        Debug.DrawRay(ray.origin, ray.direction, Color.blue);
    }
}
