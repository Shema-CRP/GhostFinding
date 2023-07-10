using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorBehaviour : MonoBehaviour
{
    [SerializeField] float ChargingSpeed;
    [SerializeField] public float RadarCharge;
    [SerializeField] public float MaxRadarCharge = 100;
    [SerializeField] private float secondsOfNoise = 2.5f;
    GameObject noise;
    Animator animController;
    SpriteRenderer RadarStateColor;
    IEnumerator CoroutineActivateNoise;

    // Start is called before the first frame update
    void Start()
    {
        noise = transform.Find("MonitorNoise").gameObject;
        RadarCharge = MaxRadarCharge;
        animController = GameObject.Find("Ghost/MonitorIcon").gameObject.GetComponent<Animator>();
        RadarStateColor = transform.Find("RadarState").gameObject.GetComponent<SpriteRenderer>();
        RadarStateColor.color = Color.green;
        CoroutineActivateNoise = ActivateNoise(secondsOfNoise);
    }

    private IEnumerator ActivateNoise(float timeToWait)
    {
        // acitate the noise immediately
        noise.SetActive(true);
        yield return new WaitForSeconds(timeToWait);
        // desactivate the noise after waiting
        noise.SetActive(false);
    }

    private void OnDestroy()
    {
        if (CoroutineActivateNoise != null)
            StopCoroutine(CoroutineActivateNoise);
    }

    public void DiffuseRadar()
    {
        if (RadarCharge >= MaxRadarCharge)
        {
            animController.SetBool("radarActive", true);
            RadarStateColor.color = Color.red;
            RadarCharge = 0;
            StartCoroutine(ActivateNoise(secondsOfNoise));
        }
    }

    public void IncrementBattery()
    {
        RadarStateColor.color = Color.green;
        if (RadarCharge < MaxRadarCharge)
        {
            RadarCharge += ChargingSpeed;
            RadarStateColor.color = Color.red;
        }
    }
}
