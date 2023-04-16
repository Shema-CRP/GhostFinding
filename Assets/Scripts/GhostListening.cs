using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostListening : MonoBehaviour
{
    [SerializeField] float RadiusLisening;
    [SerializeField] LayerMask NoiseLayer;
    Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        Collider[] listenSomething = Physics.OverlapSphere(pos, RadiusLisening, NoiseLayer);

        if (listenSomething.Length > 0)
        {
            // targetNoise will be changed by the louder noise detected
            GameObject targetNoise = this.gameObject;
            float louderNoise = -1;
            foreach (Collider hitNoise in listenSomething)
            {
                GameObject noise = hitNoise.gameObject;
                float thisNoiseIntensivity = noise.GetComponent<NoiseState>().Intensity;
                if(thisNoiseIntensivity > louderNoise)
                {
                    louderNoise = thisNoiseIntensivity;
                    targetNoise = noise;
                }
            }
            GetComponent<GhostAI>().Target = targetNoise.transform;
        }
        else
        {
            GetComponent<GhostAI>().Target = GetComponent<GhostAI>().currentDefaultTarget;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pos, RadiusLisening);
    }
}
