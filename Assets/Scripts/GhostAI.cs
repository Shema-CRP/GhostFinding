using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    [SerializeField] GameObject GhostDefaultTargetName;
    public Transform Target;
    NavMeshAgent Agent;
    public Transform currentDefaultTarget;
    [SerializeField] List<Vector3> PositionProbability;
    int totalChild;

    // Start is called before the first frame update
    void Start()
    {

        Agent= GetComponent<NavMeshAgent>();
        Agent.speed = GetComponent<GhostState>().GhostSpeed;
        PositionProbability = new List<Vector3>();

        // Get the default target in the scene
        totalChild = GameObject.Find(GhostDefaultTargetName.name).transform.childCount;
        for (int i = 0; i < totalChild; i++)
        {
            // Add all default ghost's position
            PositionProbability.Add(GameObject.Find(GhostDefaultTargetName.name).transform.GetChild(i).transform.position);
        }
        currentDefaultTarget = new GameObject().transform;
        currentDefaultTarget.name = "GhostTarget";
        currentDefaultTarget.position = PositionProbability[0];
    }

    // Update is called once per frame
    void Update()
    {
        Agent.destination = Target.position;
    }

    private void OnTriggerStay(Collider other)
    {
        // If he doesn't hear anything he'll randomly target positions on the scene
        if (other.gameObject.CompareTag("GhostPosition"))
        {
            // if the GhostPosition touched is the current target, get another target
            if (other.gameObject.transform.position == currentDefaultTarget.position)
            {
                int randomNumber = Random.Range(0, totalChild);
                currentDefaultTarget.position = PositionProbability[randomNumber];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If touch the player he kill him
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerState>().PlayerLife = PlayerState.EPlayerLife.Dead;
        }        
    }
}
