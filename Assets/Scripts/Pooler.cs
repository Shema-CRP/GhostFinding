using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] int SizeOfPool;

    public static Pooler Instance;
    GameObject Object;

    List<GameObject> AvailablePool;
    List<GameObject> InUsePool;

    // Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Object = (GameObject) Resources.Load("Prefabs/Coin");
        AvailablePool = new List<GameObject>();
        InUsePool = new List<GameObject>();

        for (int i = 0; i < SizeOfPool; i++)
        {
            GameObject thisGo = Instantiate(Object);
            thisGo.transform.SetParent(this.transform);
            AvailablePool.Add(thisGo);
            thisGo.SetActive(false);
        }
    }

    public GameObject GeneratePool(Vector3 goPosition)
    {
        if (AvailablePool.Count > 0)
        {
            GameObject returnGo = AvailablePool[0];
            AvailablePool.RemoveAt(0);
            InUsePool.Add(returnGo);
            returnGo.transform.position = goPosition;
            returnGo.SetActive(true);
            return returnGo;
        }
        Debug.Log("Empty pool");
        return null;
    }
}
