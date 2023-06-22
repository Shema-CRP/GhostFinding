using UnityEngine;

public class PlayerFear : MonoBehaviour
{
    [SerializeField] float RadiusFear;
    [SerializeField] float RadiusNearFear;
    [SerializeField] LayerMask GhostLayer;
    AudioClip fearHeartBeat;
    AudioClip panicHeartBeat;
    AudioSource heart;
    Vector3 pos;
    byte stressedOut = 3;
    byte afraid = 2;
    byte calm = 1;

    private void Start()
    {
        heart = GetComponent<AudioSource>();
        fearHeartBeat = (AudioClip) Resources.Load("Sounds/SoundsEffects/heartbeat_fear");
        panicHeartBeat = (AudioClip) Resources.Load("Sounds/SoundsEffects/heartbeat_panic");
    }

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
            AudioManager.Instance.DiffuseSound(heart ,panicHeartBeat);
        }
        else if (fear)
        {
            GetComponent<PlayerState>().PlayerFearLevel = afraid;
            AudioManager.Instance.DiffuseSound(heart ,fearHeartBeat);
        }
        else
        {
            // eteind le clip si le joueur n'a pas peur
            GetComponent<PlayerState>().PlayerFearLevel = calm;
            heart.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pos, RadiusFear);
    }


}
