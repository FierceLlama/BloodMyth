using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSpriteTesting : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

#if UNITY_STANDALONE_WIN
        if (Input.GetKey(KeyCode.Mouse0))
        {
            bool left = false, right = false, jump = false;
            InputManager.instance.GetButtonsStatus(ref left, ref right, ref jump);
            
            Debug.Log("Left " + left + " Right " + right + " Jump " + jump + " Toggle " + InputManager.instance.toggleStatus);
       }
#endif

#if UNITY_ANDROID

        bool left = false, right = false, jump = false;
        InputManager.instance.GetButtonsStatus(ref left, ref right, ref jump);
        
        if (left)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (right)
        {
            this.GetComponent<SpriteRenderer>().color = Color.black;
        }

        if (jump)
        {
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }
   
        if (InputManager.instance.toggleStatus)
        {
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            //this.GetComponent<SpriteRenderer>().color = Color.white;
        }

#endif

    }
}