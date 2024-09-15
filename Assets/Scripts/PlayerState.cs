using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public enum EPlayerLife { Live, Dead, Escape };
    [SerializeField] public EPlayerLife PlayerLife;
    [SerializeField] public float PlayerWalkSpeed;
    [SerializeField] public float PlayerSprintSpeed;
    [SerializeField] public byte PlayerFearLevel;
    [SerializeField] public float PlayerCameraSensibility;
    [SerializeField] public float StaminaDrain;
    [SerializeField] public float exhaustSpeed;
}
