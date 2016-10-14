using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Material curMat;
    Material otherMat;

    public GameObject tempuratureObj;

    [Header("QA SECTION")]
    public float Fatigue = 10;
    public float Hydration = 10;
    public float Temperature = 95;

    public float HydroDownRate = 0.01f;
    public float FatigueDownRate = 0.01f;
    public float tempLowEnd = 90f;
    public float tempHighEnd = 100f;
    public float tempLowCap = 85f;
    public float tempHighCap = 105f;
    public float hydraHighCap = 15f;
    public float fatigueHighEnd = 10f;

    public float temperatureEffect = 0.01f;
    public float hydrationEffectSprinting = 0.01f;
    public float hydrationEffectJumping = 0.2f;
    public float hydrationEffectClimbing = 0.03f;
    public float hydrationEffectDrinkingWater = 3.0f;

    // Use this for initialization
    void Start () 
    {
        curMat = GetComponent<Renderer>().material;
        otherMat = tempuratureObj.GetComponent<Renderer>().material;
        Fatigue = 10;
        Hydration = 10;
        Temperature = 95;
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
        if (Temperature < tempLowEnd || Temperature > tempHighEnd)
        {
            //Lower Fatigue
            Fatigue -= FatigueDownRate;
        }

        //Fatigue Check
        if (Fatigue <= 0)
        {
            //Crisis -- needs better management later
            Camera.main.gameObject.GetComponent<SceneController>().RestartScene();
        }
	}

    void CheckStatus()
    {
        //Hydration stays in bounds
        //if (Hydration > hydraHighCap)
        //    Hydration = hydraHighCap;
        //else if (Hydration < 0)
        //    Hydration = 0;

        ////Temp stays in bounds
        //if (Temperature < tempLowCap)
        //    Temperature = tempLowCap;
        //else if (Temperature > tempHighCap)
        //    Temperature = tempHighCap;

        //Gain back Fatigue
        if (Hydration > 0 && Temperature > tempLowEnd && Temperature < tempHighEnd && Fatigue < fatigueHighEnd)
            Fatigue += FatigueDownRate;
    }

    void UpdateMat()
    {        
        if (Temperature > tempHighEnd)
        {
            otherMat.color = new Color(1, otherMat.color.g, otherMat.color.b,1);
            otherMat.color -= new Color(0, 0.01f, 0.01f, 0); 
        }
        else if (Temperature < tempLowEnd)
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

    public void playerJumped()
    {
        if (!checkLowHydration())
        {
            this.Hydration -= this.hydrationEffectJumping;
        }
    }

    public void playerSprinting()
    {
        if (!checkLowHydration())
        {
            this.Hydration -= this.hydrationEffectSprinting;
        }
    }

    public void playerClimbing()
    {
        if (!checkLowHydration())
        {
            this.Hydration -= this.hydrationEffectClimbing;
        }
    }

    bool checkLowHydration()
    {
        bool hydration0 = false;

        if (this.Hydration <= 0)
        {
            this.Hydration = 0;
            hydration0 = true;
        }

        return hydration0;
    }

    public void playerDrinkingWater()
    {
        if (this.Hydration < hydraHighCap)
        {
            this.Hydration += this.hydrationEffectDrinkingWater;
        }
        else
        {
            this.Hydration = this.hydraHighCap;
        }
    }

    public void playerInShade()
    {
        if (this.Temperature > 95)
        {
            this.Temperature -= this.temperatureEffect;
        }

        else if (this.Temperature < 95)
        {
            this.Temperature += this.temperatureEffect;
        }
    }

    public void playerHot()
    {
        if (this.Temperature < this.tempHighCap)
        {
            this.Temperature += this.temperatureEffect;
        }
        else
        {
            this.Temperature = this.tempHighCap;
        }
    }

    public void playerCold()
    {
        if (this.Temperature > this.tempLowCap)
        {
            this.Temperature -= this.temperatureEffect;
        }
        else
        {
            this.Temperature = this.tempLowCap;
        }
    }
}
