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
        Primary = inputman.GetPrimaryInputData();
        Secondary = inputman.GetSecondryInputData();

        // inputman.GetCurrenEnabledScreenSections(out Primary, out Secondary);

        if (Secondary.CurrScrSec == ScreenSection.None)
        { 
            if (Primary.CurrScrSec == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            else if (Primary.CurrScrSec == ScreenSection.Bottom)
                this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            else if(Primary.CurrScrSec == ScreenSection.Left)
                this.gameObject.GetComponent<Renderer>().material.color = Color.black;
            else if(Primary.CurrScrSec == ScreenSection.Right)
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            else
                this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        { 
            if (Primary.CurrScrSec == ScreenSection.Top && Secondary.CurrScrSec == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            else if(Primary.CurrScrSec == ScreenSection.Bottom && Secondary.CurrScrSec == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.gray;
            else if(Primary.CurrScrSec == ScreenSection.Left && Secondary.CurrScrSec == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
            else if(Primary.CurrScrSec == ScreenSection.Right && Secondary.CurrScrSec == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.clear;
            else
                this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
