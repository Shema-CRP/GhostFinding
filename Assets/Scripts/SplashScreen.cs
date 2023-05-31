using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float Seconds = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowLogo(Seconds));
    }

    IEnumerator ShowLogo(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        BootManager.Instance.ChangeScene("Splashscreen", "Menu");
    }
}
