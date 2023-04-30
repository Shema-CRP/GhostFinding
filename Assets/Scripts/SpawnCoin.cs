using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject coin = Pooler.Instance.GeneratePool(new Vector3(Random.Range(-100,100),8,Random.Range(-100,100)));
            Instantiate(coin);
        }
    }
}
