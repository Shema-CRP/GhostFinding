using UnityEngine;

public class PlayerFear : MonoBehaviour
{
    [SerializeField] float RadiusFear;
    [SerializeField] float RadiusNearFear;
    [SerializeField] LayerMask GhostLayer;
    Vector3 pos;
    byte stressedOut = 3;
    byte afraid = 2;
    byte calm = 1;

    // Update is called once per frame
    void Update()
    {
        pos = GetComponent<Transform>().position;
        // Create colliders around the player for the fear level
        bool fear = Physics.CheckSphere(pos, RadiusFear, GhostLayer);
        bool nearFear = Physics.CheckSphere(pos, RadiusNearFear, GhostLayer);


        // If the ghost is very near
        if (nearFear)
        {
            GetComponent<PlayerState>().PlayerFearLevel = stressedOut;
        }
        else if (fear)
        {
            GetComponent<PlayerState>().PlayerFearLevel = afraid;
        }
        else
        {
            GetComponent<PlayerState>().PlayerFearLevel = calm;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pos, RadiusFear);
    }
}
