using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    [SerializeField] float loudNoise = 8f;
    [SerializeField] float silenceNoise = 1f;

    Camera PlayerCam;
    Rigidbody rb;
    float normalSpeed;
    float sprintSpeed;
    bool hideCursor = true;
    float mouseX = 0f;
    float mouseY = 0f;
    float lookSpeed;
    GameObject noise;
    GameObject monitor;
    float noiseIntensivity;
    bool monitorActivate = false;


    // Start is called before the first frame update
    void Start()
    {
        PlayerCam = this.transform.Find("PlayerVision").gameObject.GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        normalSpeed = GetComponent<PlayerState>().PlayerWalkSpeed;
        sprintSpeed = GetComponent<PlayerState>().PlayerSprintSpeed;
        lookSpeed = GetComponent<PlayerState>().PlayerCameraSensibility;
        noise = GameObject.Find("PlayerNoise");
        monitor = GameObject.Find("Monitor");
        noiseIntensivity = noise.GetComponent<NoiseState>().Intensity;

        // unset the monitor in the begin of the game
        monitor.SetActive(false);
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
        transform.eulerAngles += lookSpeed * new Vector3(0, mouseX, 0) * Time.deltaTime;

       
        if(inputX == 0 && inputZ == 0)
        {
            // no move no noise
            noise.SetActive(false);
        }
        else
        { 
            // walk noise
            noise.SetActive(true);
            noiseIntensivity = silenceNoise;
        }

        // sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            noiseIntensivity = loudNoise;
            speed = sprintSpeed;
        }

        // activation/desactivation monitor
        if (Input.GetKeyDown(KeyCode.Space))
        {
            monitorActivate = !monitorActivate;
            monitor.SetActive(monitorActivate);
            // Spawn coin
            GameObject coin = Pooler.Instance.GeneratePool(new Vector3(Random.Range(-100, 100), 5, Random.Range(-100, 100)));
            Instantiate(coin);
        }

        // echap
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("BootManager").GetComponent<BootManager>().ChangeScene("Game", "Menu");
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
