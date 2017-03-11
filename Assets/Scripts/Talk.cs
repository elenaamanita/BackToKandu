using UnityEngine;
using System.Collections;

public class Talk : MonoBehaviour {

    public GameObject player;
    public int iMask;
    TextMesh t;
    //= (TextMesh)gameObject.GetComponent(typeof(TextMesh));

	// Use this for initialization
	void Start () {
        t = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        iMask = player.GetComponent<CharacterController>().wichMask;
        //Debug.Log("Mascarilha" + iMask);
        switch (iMask)
        {
            case 1:
                t.text = "I'am Air Spirit";
                break;
            case 2:
                t.text = "I'am Earth Spirit";
                break;
            case 3:
                t.text = "I'am Fire Spirit";
                break;
            case 4:
                t.text = "I'am Water Spirit";
                break;
        }


    }
}
