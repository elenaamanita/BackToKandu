using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    public float inputDelay = 0.1f;
    public float forwardVelocity = 12;
    public float rotateVelocity = 100;

    Quaternion targetRotation; //just to hold the next position to turn to
    Rigidbody rigidBody;
    float forwardInput, turnInput;

    bool jump = false;
    public float jumpForce = 100f;
    public float comingForce = 20f;
    public bool grounded = true;
    public int wichMask = 1;
    public Transform groundCheck;

    public bool isMoveble = true;

    public GameObject[] mask;

    public AudioSource airSound;
    public AudioSource earthSound;
    public AudioSource fireSound;
    public AudioSource waterSound;

    public bool playerIsDead = false;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    void Start()
    {
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rigidBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("The character needs a rigidbody.");

        forwardInput = turnInput = 0;

        TurnOffMasks();

        //initiates with mask of same type of square
        mask[0].GetComponent<Renderer>().enabled = true;
        Renderer[] renderers = mask[0].GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
            r.enabled = true;

        airSound.Play();

        grounded = false;
    }

    void GetInput()
    {
        if (!playerIsDead)
        {
            forwardInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");

            RaycastHit hit;
            Ray feetRay = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(feetRay, out hit, .6f) && !jump)
                if (hit.collider.tag == "GroundStart" ||
                    hit.collider.tag == "GroundEnd"   ||
                    hit.collider.tag == "Ground"      ||
                    hit.collider.tag == "GroundFire"  ||
                    hit.collider.tag == "GroundWind"  ||
                    hit.collider.tag == "GroundWater" ||
                    hit.collider.tag == "GroundEarth")
                    grounded = true;

            if (Input.GetButtonDown("Jump") && grounded)
            {
                jump = true;
                grounded = false;
            }
        }
    }

    void Update()
    {
        GetInput();
        Turn();
        GetMasks();

        if (playerIsDead)
            ComeDown();

        //Debug.Log("player Is DEad" + playerIsDead);
    }

    void TurnOffMasks()
    {
        for (int i = 0; i < mask.Length; i++)
        {
            mask[i].GetComponent<Renderer>().enabled = false;

            Renderer[] renderers = mask[i].GetComponentsInChildren<Renderer>();

            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }
        }

    }

    void StopSounds()
    {
        airSound.Stop();
        earthSound.Stop();
        fireSound.Stop();
        waterSound.Stop();
    }

    void GetMasks()
    {

        if (Input.GetKeyDown("1")) // Air/ wind
        {
            TurnOffMasks();
            mask[0].GetComponent<Renderer>().enabled = true;
            Renderer[] renderers = mask[0].GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
            wichMask = 1;
            StopSounds();
            airSound.Play();
        }
        if (Input.GetKeyDown("2")) // Earth
        {
            TurnOffMasks();
            mask[1].GetComponent<Renderer>().enabled = true;
            Renderer[] renderers = mask[1].GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
            wichMask = 2;
            StopSounds();
            earthSound.Play();
        }
        if (Input.GetKeyDown("3")) // Fire
        {
            TurnOffMasks();
            mask[2].GetComponent<Renderer>().enabled = true;
            Renderer[] renderers = mask[2].GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
            wichMask = 3;
            StopSounds();
            fireSound.Play();
        }
        if (Input.GetKeyDown("4")) // Water
        {
            TurnOffMasks();
            mask[3].GetComponent<Renderer>().enabled = true;
            Renderer[] renderers = mask[3].GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
            wichMask = 4;
            StopSounds();
            waterSound.Play();
        }
    }

    void FixedUpdate()
    {
        Run();
        Jump();
        if (!grounded)
            ComeDown();
    }

    public void ComeDown()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity * comingForce, ForceMode.Acceleration);
    }

    void Jump()
    {
        if (jump)
        {
            //anim.SetTrigger("Jump");
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
            jump = false;
        }
    }

    void Run ()
    {
        if (Mathf.Abs(forwardInput) > inputDelay) // move
            rigidBody.velocity = transform.forward * forwardInput * forwardVelocity;
        else //zero velocity
            rigidBody.velocity = Vector3.zero;
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputDelay)
            targetRotation *= Quaternion.AngleAxis(rotateVelocity * turnInput * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;
    }
}
