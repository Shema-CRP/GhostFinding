using UnityEngine;
using UnityEngine.UI;

public class PlayerMouvement : MonoBehaviour
{
    [SerializeField] float loudNoise = 8f;
    [SerializeField] float silenceNoise = 1f;
    
    GameObject[] SpeakersObject;
    SpeakerBehaviour[] Speakers;

    Camera PlayerCam;
    Rigidbody rb;
    float normalSpeed;
    float sprintSpeed;
    float exhaustSpeed;
    float stamina;
    bool hideCursor = true;
    bool exhausted;
    float mouseX = 0f;
    float mouseY = 0f;
    float lookSpeed;
    GameObject noise;
    GameObject monitor;
    float noiseIntensivity;
    bool monitorActivate = false;
    float maxStamina = 1f;
    float drainStamina;
    UnityEngine.UI.Image staminaBar;
    AudioClip exhaustSound;
    AudioSource playerHead;
    AudioClip walkSound1;
    AudioClip walkSound2;
    AudioClip sprintSound1;
    AudioClip sprintSound2;
    AudioClip currentMouvSound;
    AudioSource playerFoot;
    MonitorBehaviour monitorScript;

    // Start is called before the first frame update
    void Start()
    {
        int totalChild;
        PlayerCam = this.transform.Find("PlayerVision").gameObject.GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        normalSpeed = GetComponent<PlayerState>().PlayerWalkSpeed;
        sprintSpeed = GetComponent<PlayerState>().PlayerSprintSpeed;
        lookSpeed = GetComponent<PlayerState>().PlayerCameraSensibility;
        exhaustSpeed = GetComponent<PlayerState>().exhaustSpeed;
        stamina = maxStamina;
        exhausted = false;
        drainStamina = GetComponent<PlayerState>().StaminaDrain;
        noise = GameObject.Find("PlayerNoise");
        monitor = GameObject.Find("Monitor");
        noiseIntensivity = noise.GetComponent<NoiseState>().Intensity;
        staminaBar = GameObject.Find("Canvas/Stamina/Fill").GetComponent<Image>();
        monitorScript = monitor.GetComponent<MonitorBehaviour>();

        // récupère les leurres dans le gameobject Speakerlist ils doivent être dans l'ordre afin de les récuperer correctement dans le tableau
        totalChild = GameObject.Find("SpeakersList").transform.childCount;
        SpeakersObject = new GameObject[totalChild];
        Speakers = new SpeakerBehaviour[totalChild];
        for (int i = 0; i < totalChild; i++)
        {
            SpeakersObject[i] = GameObject.Find("SpeakersList").transform.GetChild(i).gameObject;
            Speakers[i] = SpeakersObject[i].GetComponent<SpeakerBehaviour>();
        }

        // unset the monitor in the begin of the game
        monitor.SetActive(false);

        playerHead = this.transform.Find("PlayerVision").gameObject.GetComponent<AudioSource>();
        playerFoot = this.transform.Find("Body").gameObject.GetComponent<AudioSource>();
        exhaustSound = (AudioClip) Resources.Load("Sounds/SoundsEffects/exhausted");
        walkSound1 = (AudioClip)Resources.Load("Sounds/SoundsEffects/walkstep01");
        walkSound2 = (AudioClip)Resources.Load("Sounds/SoundsEffects/walkstep02");
        sprintSound1 = (AudioClip)Resources.Load("Sounds/SoundsEffects/sprintstep01");
        sprintSound2 = (AudioClip)Resources.Load("Sounds/SoundsEffects/sprintstep02");
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

    private void Update()
    {
        // activation/desactivation monitor
        if (Input.GetKeyDown(KeyCode.Space))
        {
            monitorActivate = !monitorActivate;
            monitor.SetActive(monitorActivate);
        }

        // echap
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BootManager.Instance.ChangeScene("Game", "Menu");
        }

        // enclenchement des leurres
        if (monitorActivate)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                monitorScript.DiffuseRadar();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Speakers[0].LaunchSound();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Speakers[1].LaunchSound();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Speakers[2].LaunchSound();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Speakers[3].LaunchSound();
            }
        }

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            currentMouvSound = null;
        }
        else
        {
            if (!playerFoot.isPlaying)
            {
                // sprint sound
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (currentMouvSound == sprintSound1)
                        currentMouvSound = sprintSound2;
                    else
                        currentMouvSound = sprintSound1;
                }
                else
                {
                    // walk sound
                    if (currentMouvSound == walkSound1)
                        currentMouvSound = walkSound2;
                    else
                        currentMouvSound = walkSound1;
                }

                AudioManager.Instance.DiffuseSound(playerFoot, currentMouvSound);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        mouseY = Mathf.Clamp(mouseY, -85f, 85f);

        float speed = normalSpeed;
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // rotation
        transform.eulerAngles += lookSpeed * new Vector3(0, mouseX, 0) * Time.fixedDeltaTime;

       
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

        // slow the player if he's exhausted and avoid him to sprint
        if (exhausted)
        {
            // exhaust noise
            noise.SetActive(true);
            noiseIntensivity = silenceNoise;
            staminaBar.color = Color.red;
            speed = exhaustSpeed;
            if (stamina < 1)
            {
                stamina += drainStamina;
                staminaBar.fillAmount = stamina;
            }
            else
            {
                exhausted = false;
            }
            AudioManager.Instance.DiffuseSound(playerHead, exhaustSound);
        }
        else
        {
            staminaBar.color = Color.yellow;
            // sprint 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (stamina > 0)
                {
                    stamina -= drainStamina;
                    noiseIntensivity = loudNoise;
                    speed = sprintSpeed;
                    staminaBar.fillAmount = stamina;
                }
                else
                {
                    exhausted = true;
                }
            }
            else
            {
                if (stamina < 1)
                {
                    stamina += drainStamina;
                    staminaBar.fillAmount = stamina;
                }
            }
        }

        // calcul de la direction du joueur
        Vector3 forward = PlayerCam.transform.forward;
        Vector3 right = PlayerCam.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * inputZ + right * inputX).normalized;

        rb.velocity = move * speed;
    }
}
