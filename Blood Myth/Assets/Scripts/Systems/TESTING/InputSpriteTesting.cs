using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSpriteTesting : MonoBehaviour
{
	// Use this for initialization
	void Start () { }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID

        bool left = false, right = false, jump = false;
        InputManager.instance.GetButtonsStatus(ref left, ref right, ref jump);
        GameObject.Find("Right").GetComponent<Image>().color = Color.white;
        GameObject.Find("Left").GetComponent<Image>().color = Color.white;
        GameObject.Find("Sprint").GetComponent<Image>().color = Color.white;
        GameObject.Find("Jump").GetComponent<Image>().color = Color.white;


        if (left)
            GameObject.Find("Left").GetComponent<Image>().color = Color.red;
        if (right)
            GameObject.Find("Right").GetComponent<Image>().color = Color.red;
        if (jump)
            GameObject.Find("Jump").GetComponent<Image>().color = Color.red;
        if (InputManager.instance.toggleStatus)
            GameObject.Find("Sprint").GetComponent<Image>().color = Color.red;
 
#endif
    }
}