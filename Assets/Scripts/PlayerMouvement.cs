using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    Rigidbody rb;
    float normalSpeed;
    float sprintSpeed;
    bool hideCursor = true;
    float mouseX = 0f;
    float mouseY = 0f;
    float lookSpeed;

    [SerializeField] Camera PlayerCam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        normalSpeed = GetComponent<PlayerState>().PlayerWalkSpeed;
        sprintSpeed = GetComponent<PlayerState>().PlayerSprintSpeed;
        lookSpeed = GetComponent<PlayerState>().PlayerCameraSensibility;
    }

    private void LateUpdate()
    {
        //centrer la souris chaque frame
        if (hideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        mouseY = Mathf.Clamp(mouseY, -85f, 85f);

        float speed = normalSpeed;
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // rotation
        //Quaternion turn = Quaternion.Euler(mouseY, mouseX, 0f);
        transform.eulerAngles += lookSpeed * new Vector3(0, mouseX, 0) * Time.deltaTime;
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + mouseX * lookSpeed * Time.deltaTime, transform.rotation.z, transform.rotation.w);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }

        // calcul de la direction du joueur
        Vector3 forward = PlayerCam.transform.forward;
        Vector3 right = PlayerCam.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * inputZ + right * inputX).normalized;

        rb.MovePosition(rb.position + move * speed * Time.deltaTime);
    }
}
