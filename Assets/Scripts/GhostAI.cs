using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    [SerializeField] GameObject FirstDefaultTarget;
    [SerializeField] GameObject SecondDefaultTarget;
    [SerializeField] public Transform Target;
    NavMeshAgent Agent;
    public Transform currentDefaultTarget;

    // Start is called before the first frame update
    void Start()
    {
        Agent= GetComponent<NavMeshAgent>();
        Agent.speed = GetComponent<GhostState>().GhostSpeed;
        currentDefaultTarget = FirstDefaultTarget.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Agent.destination = Target.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstGhostPoint"))
        {
            currentDefaultTarget = SecondDefaultTarget.transform;
        }
        if (other.gameObject.CompareTag("SecondGhostPoint"))
        {
            currentDefaultTarget = FirstDefaultTarget.transform;
        }    
    }
}
