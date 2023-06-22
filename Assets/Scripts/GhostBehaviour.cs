using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField] Sprite GhostSprite;
    [SerializeField] Camera PlayerCamera;
    [SerializeField] int framesAppears;
    [SerializeField] int framesRoll;
    [SerializeField] int chanceToAppears;
    SpriteRenderer CurrentSprite;
    Camera CurrentCamera;
    int frame;
    int framesAppearsLeft;

    void Start()
    {
        CurrentSprite = this.transform.Find("Body").GetComponent<SpriteRenderer>();
        CurrentCamera = PlayerCamera;
    }

    private void Update()
    {
        frame++;
        frame %= framesRoll;
        if (frame == 0)
        {
            int rndNum = Random.Range(1, 101);
            if (rndNum <= chanceToAppears)
            {
                framesAppearsLeft = framesAppears;
            }
        }

        if (framesAppearsLeft > 0)
            CurrentSprite.sprite = GhostSprite;
        else
            CurrentSprite.sprite = null;

        framesAppearsLeft--;
        if (framesAppearsLeft < 0)
            framesAppearsLeft = 0;
    }

    void LateUpdate()
    {
        this.transform.rotation = PlayerCamera.transform.rotation;
    }
}
