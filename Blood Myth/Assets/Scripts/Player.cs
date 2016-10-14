using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Material curMat;
    Material otherMat;

    public GameObject tempuratureObj;

    [Header("QA SECTION")]
    public float Fatigue = 10;
    public static float Hydration = 10;
    public static float Tempurature = 95;

    public float HydroDownRate = 0.0001f;
    public float FatigueDownRate = 0.01f;
    public float tempLowEnd = 90f;
    public float tempHighEnd = 100f;
    public float tempLowCap = 85f;
    public float tempHighCap = 105f;
    public float hydraHighCap = 15f;
    public float fatigueHighEnd = 10f;

    // Use this for initialization
    void Start () 
    {
        curMat = GetComponent<Renderer>().material;
        otherMat = tempuratureObj.GetComponent<Renderer>().material;
        Fatigue = 10;
        Hydration = 10;
        Tempurature = 95;
	}

    /*
        Need a fixed update step for player movement.  Will eventually need to move this out to a strategy pattern for the different movements (e.g. walking, jumping, sprinting, near crisis)
    */
	
	// Update is called once per frame
	void Update () 
    {
        CheckStatus();
        UpdateMat();

        //Constant Dehydration
        //Hydration -= HydroDownRate;

        //Hydration Check
        if (Hydration <= 0)
        {
            //Lower Fatigue
            Fatigue -= FatigueDownRate;
        }

        //Temp Check
        if (Tempurature < tempLowEnd || Tempurature > tempHighEnd)
        {
            //Lower Fatigue
            Fatigue -= FatigueDownRate;
        }

        //Fatigue Check
        if (Fatigue <= 0)
        {
            //Crisis
            Camera.main.gameObject.GetComponent<SceneController>().RestartScene();
        }

        //Check position
        if(gameObject.transform.position.y < -25)
            Camera.main.gameObject.GetComponent<SceneController>().RestartScene();

        /*
        if (Input.GetKeyDown(KeyCode.KeypadEnter))//Change to when at water source
            DrinkWater();
        //*/
	}

    void CheckStatus()
    {
        //Hydration stays in bounds
        if (Hydration > hydraHighCap)
            Hydration = hydraHighCap;
        else if (Hydration < 0)
            Hydration = 0;

        //Temp stays in bounds
        if (Tempurature < tempLowCap)
            Tempurature = tempLowCap;
        else if (Tempurature > tempHighCap)
            Tempurature = tempHighCap;

        //Gain back Fatigue
        if (Hydration > 0 && Tempurature > tempLowEnd && Tempurature < tempHighEnd && Fatigue < fatigueHighEnd)
            Fatigue += FatigueDownRate;
    }

    void UpdateMat()
    {        
        if (Tempurature > tempHighEnd)
        {
            otherMat.color = new Color(1, otherMat.color.g, otherMat.color.b,1);
            otherMat.color -= new Color(0, 0.01f, 0.01f, 0); 
        }
        else if (Tempurature < tempLowEnd)
        {
            otherMat.color = new Color(otherMat.color.r, otherMat.color.g, 1, 1);
            otherMat.color -= new Color(0.01f, 0.01f, 0, 0); 
        }
        else
        {
            otherMat.color = new Color(1, 1, 1, 1);
        }

        tempuratureObj.GetComponent<Renderer>().material = otherMat;
        curMat.color = new Color(Hydration/10, Hydration/10, Hydration/10, Fatigue/10);
        GetComponent<Renderer>().material = curMat;
    }
}
