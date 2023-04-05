using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum EPlayerLife { Live, Dead };
    [SerializeField] public EPlayerLife PlayerLife;
    [SerializeField] public float PlayerWalkSpeed;
    [SerializeField] public float PlayerSprintSpeed;
    [SerializeField] public byte PlayerFearLevel;
}
