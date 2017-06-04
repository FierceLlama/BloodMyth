using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TestInput : MonoBehaviour {

    public InputManager inputman;
    public TouchInputData Primary;
    public TouchInputData Secondary;

	// Use this for initialization
	void Awake ()
    {
        SceneManager.LoadScene("debug", LoadSceneMode.Additive);
        inputman = GetComponent<InputManager>();
	}

    void Update()
    {
        /*
        Primary = inputman.GetPrimaryInputData();
        Secondary = inputman.GetSecondryInputData();

        // inputman.GetCurrenEnabledScreenSections(out Primary, out Secondary);

        if (Secondary.CurrentScreenSection == ScreenSection.None)
        { 
            if (Primary.CurrentScreenSection == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            else if (Primary.CurrentScreenSection == ScreenSection.Bottom)
                this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            else if(Primary.CurrentScreenSection == ScreenSection.Left)
                this.gameObject.GetComponent<Renderer>().material.color = Color.black;
            else if(Primary.CurrentScreenSection == ScreenSection.Right)
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            else
                this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        { 
            if (Primary.CurrentScreenSection == ScreenSection.Top && Secondary.CurrentScreenSection == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            else if(Primary.CurrentScreenSection == ScreenSection.Bottom && Secondary.CurrentScreenSection == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.gray;
            else if(Primary.CurrentScreenSection == ScreenSection.Left && Secondary.CurrentScreenSection == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
            else if(Primary.CurrentScreenSection == ScreenSection.Right && Secondary.CurrentScreenSection == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.clear;
            else
                this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        */
    }
}