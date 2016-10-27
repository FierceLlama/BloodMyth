using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Debuginfo : MonoBehaviour {

    TestInput inpttest;
    string text;

	// Use this for initialization
	void Start () {

        inpttest = GameObject.Find("TestCube").GetComponent<TestInput>();
      
	}
	
	// Update is called once per frame
	void Update ()
    {
        text = "";
        text += "Primary Screen Section: " + inpttest.Primary.ToString();
        text += "\n";
        text += "Secondary Screen Section: " + inpttest.Secondary.ToString();
        text += "\n";
        text += "Touch Counts: " + Input.touchCount.ToString();
        text += "\n";

        this.GetComponent<Text>().text = text;
	}
}
