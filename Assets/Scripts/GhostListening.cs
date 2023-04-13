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
            GetComponent<GhostAI>().Target = listenSomething[0].transform;
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
