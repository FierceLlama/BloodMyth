using UnityEngine;
using System.Collections;

public class TestInput : MonoBehaviour {

    public InputManager inputman;
    public ScreenSection Primary;
    public ScreenSection Secondary;

	// Use this for initialization
	void Awake ()
    {
        inputman = GetComponent<InputManager>();
	}

    void Update()
    {
        inputman.GetCurrenEnabledScreenSections(out Primary, out Secondary);

        if (Secondary == ScreenSection.None)
        { 
            if (Primary == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            else if (Primary == ScreenSection.Bottom)
                this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            else if(Primary == ScreenSection.Left)
                this.gameObject.GetComponent<Renderer>().material.color = Color.black;
            else if(Primary == ScreenSection.Right)
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            else
                this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        { 
            if (Primary == ScreenSection.Top && Secondary == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            else if(Primary == ScreenSection.Bottom && Secondary == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.gray;
            else if(Primary == ScreenSection.Left && Secondary == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
            else if(Primary == ScreenSection.Right && Secondary == ScreenSection.Top)
                this.gameObject.GetComponent<Renderer>().material.color = Color.clear;
            else
                this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
	
    /*
	// Update is called once per frame
	void Update ()
    {
        //mytouch = Input.GetTouch(0)
        switch (Input.touchCount)
        {
            case 1:
                {
                    Touch Touch1 = Input.GetTouch(0);

                    if (Touch1.phase == TouchPhase.Began)
                        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    else if (Touch1.phase == TouchPhase.Stationary)
                        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                break;
            case 2:
                {
                    Touch Touch1 = Input.GetTouch(0);
                    Touch Touch2 = Input.GetTouch(1);

                    if (Touch1.phase == TouchPhase.Began && Touch2.phase == TouchPhase.Began)
                        this.gameObject.GetComponent<Renderer>().material.color = Color.black;
                    else if (Touch1.phase == TouchPhase.Stationary && Touch2.phase == TouchPhase.Stationary)
                        this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                }
                break;
            default:
                    this.gameObject.GetComponent<Renderer>().material.color = Color.white;
                break;
        }
    }
    */
}
