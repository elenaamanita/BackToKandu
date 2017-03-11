using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public AudioSource drumSound;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        drumSound.Play();
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hut");
            Application.LoadLevel(0);
        }
    }
}
