using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Animator anim;

    private void Update()
    {
        int playerFear = Player.GetComponent<PlayerState>().PlayerFearLevel;
        // State of Player
        switch (playerFear)
        {
            case 1:
                anim.SetInteger("levelOfFear", 1);
                break;
            case 2:
                anim.SetInteger("levelOfFear", 2);
                break;
            case 3:
                anim.SetInteger("levelOfFear", 3);
                break;
            default:
                anim.SetInteger("levelOfFear", 1);
                break;
        }
    }
}
