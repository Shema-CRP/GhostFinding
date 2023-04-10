using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    [SerializeField] Transform Target;
    NavMeshAgent Agent;

    // Start is called before the first frame update
    void Start()
    {
        Agent= GetComponent<NavMeshAgent>();
        Agent.speed = GetComponent<GhostState>().GhostSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Agent.destination = Target.position;
    }
}
