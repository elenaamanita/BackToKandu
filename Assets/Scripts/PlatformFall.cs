using UnityEngine;
using System.Collections;

public class PlatformFall : MonoBehaviour
{

    public float fallDelay = -1f;
    int myMask;

    private Rigidbody rbGround;

    public AudioSource eQuakeSound;

    void Awake()
    {
        rbGround = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        GameObject myPlayer = GameObject.Find("Player");
        CharacterController characterScript = myPlayer.GetComponent<CharacterController>();

        if (other.gameObject.CompareTag("Player"))
        {
            myMask = characterScript.wichMask;

            if (gameObject.CompareTag("GroundStart"))
            {
                //Debug.Log("starting game");
                fallDelay = 10f;
                characterScript.grounded = false;
                characterScript.ComeDown();
                Invoke("Fall", fallDelay);
            }
            if (gameObject.CompareTag("GroundEnd"))
            {
                //Debug.Log("reached the end");
                fallDelay = 100f; // Go Next Level
                characterScript.ComeDown();
                Invoke("Fall", fallDelay);
            }
            if ((gameObject.CompareTag("GroundFire") && !(myMask == 3)) ||
                (gameObject.CompareTag("GroundWind") && !(myMask == 1)) ||
                (gameObject.CompareTag("GroundEarth") && !(myMask == 2)) ||
                (gameObject.CompareTag("GroundWater") && !(myMask == 4)))
            {
                characterScript.playerIsDead = true;
                Invoke("Fall", fallDelay);
            }

        }
    }

    void OnCollisionExit(Collision other)
    {

        GameObject myPlayer = GameObject.Find("Player");
        CharacterController characterScript = myPlayer.GetComponent<CharacterController>();
        if (other.gameObject.CompareTag("Player"))
        {
            //characterScript.playerIsDead = true;
            characterScript.grounded = false;
            characterScript.ComeDown();
        }
    }


    void Fall()
    {
        eQuakeSound.Play();
        rbGround.isKinematic = false;
        rbGround.GetComponent<MeshCollider>().enabled = false;
    }
}
