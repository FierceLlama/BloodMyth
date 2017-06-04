using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Debuginfo : MonoBehaviour
{
    TestInput inpttest;
    string text;

    public TouchInputData Primary;
    public TouchInputData Secondary;

    // Use this for initialization
    void Start ()
    {
        inpttest = GameObject.Find("TestCube").GetComponent<TestInput>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*      //inpttest.inputman.GetCurrenEnabledScreenSections(out Primary, out Secondary);

              Primary = inpttest.inputman.GetPrimaryInputData();
              Secondary = inpttest.inputman.GetSecondryInputData();

              text = "";
              text += "Primary Screen Section: " + inpttest.Primary.CurrentScreenSection.ToString();
              text += "\n";
              text += "Secondary Screen Section: " + inpttest.Secondary.CurrentScreenSection.ToString();
              text += "\n";
              text += "Touch Counts: " + Input.touchCount.ToString();
              text += "\n";

      #if UNITY_ANDROID
              text += "TouchTaps First Input: " + Primary.getTouchTapCount();
              text += "\n";

              text += "TouchTaps Second Input: " + Secondary.getTouchTapCount();
              text += "\n";       
      #endif
              this.GetComponent<Text>().text = text;
              */
    }

}