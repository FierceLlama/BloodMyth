using UnityEngine;
using System.Collections;

public enum PlayerFatigue
{
    NORMAL_FATIGUE = 100000,
    TIRED_FATIGUE,
    EXHAUSTED_FATIGUE
}

public class Player : MonoBehaviour
{
    Material curMat;
    Material otherMat;

    public GameObject tempuratureObj;
    private PlayerFatigue _playerFatigue;
    private PlayerManager _playerManager;
    private float _zero = 0.0f;
    private float _colorDivisor = 10.0f;
    private float _temperatureRangeMid;

    [Header("QA SECTION")]
    private float _currentFatigue;
    private float _currentHydration;
    private float _currentTemperature = 95.0f;

    public float hydrationDownRate = 0.01f;
    public float fatigueDownRate = 0.01f;
    public float temperatureRangeLow = 90.0f;
    public float temperatureRangeHigh = 100.0f;
    public float minTemperature = 85.0f;
    public float maxTemperature = 105.0f;
    public float maxHydration = 15.0f;
    public float maxFatigue = 10.0f;

    public float temperatureEffect = 0.01f;
    public float hydrationEffectSprinting = 0.01f;
    public float hydrationEffectJumping = 0.2f;
    public float hydrationEffectClimbing = 0.03f;
    public float hydrationEffectDrinkingWater = 3.0f;

    public float tiredFatigueRangeHigh = 8.0f;
    public float tiredFatigueRangeLow = 3.0f;

    // Use this for initialization
    void Start () 
    {
        this._playerManager = GetComponent<PlayerManager>();
        curMat = GetComponent<Renderer>().material;
        otherMat = tempuratureObj.GetComponent<Renderer>().material;
        this._playerFatigue = PlayerFatigue.NORMAL_FATIGUE;
        this._currentFatigue = maxFatigue;
        this._currentHydration = maxHydration;
        this._temperatureRangeMid = (this.temperatureRangeHigh + this.temperatureRangeLow) / 2.0f;
    }

    /*
        Need a fixed update step for player movement.  Will eventually need to move this out to a strategy pattern for the different movements (e.g. walking, jumping, sprinting, near crisis)
    */
	
	// Update is called once per frame
	void Update () 
    {
        CheckStatus();
        UpdateMat();

        // Hydration Check
        if (this._currentHydration <= this._zero)
        {
            // Lower Fatigue
            this.LowerFatigue();
        }

        // Temp Check
        if (this._currentTemperature <= this.minTemperature || this._currentTemperature >= this.maxTemperature)
        {
            // Lower Fatigue
            this.LowerFatigue();
        }

        //// Fatigue Check for crisis
        //if (this._currentFatigue <= this._zero)
        //{
        //    // Crisis -- needs better management later
        //    Camera.main.gameObject.GetComponent<SceneController>().RestartScene();
        //}
	}

    void CheckStatus()
    {
        // Gain back Fatigue
        if ((this._currentHydration > this._zero && this._currentTemperature > this.temperatureRangeLow) &&
            (this._currentTemperature < this.temperatureRangeHigh && this._currentFatigue < this.maxFatigue))
        {
            this._currentFatigue += this.fatigueDownRate;
            this.DetermineFatigue();
        }
    }

    void UpdateMat()
    {        
        if (this._currentTemperature > this.maxTemperature)
        {
            otherMat.color = new Color(1, otherMat.color.g, otherMat.color.b,1);
            otherMat.color -= new Color(0, 0.01f, 0.01f, 0); 
        }
        else if (this._currentTemperature < this.minTemperature)
        {
            otherMat.color = new Color(otherMat.color.r, otherMat.color.g, 1, 1);
            otherMat.color -= new Color(0.01f, 0.01f, 0, 0); 
        }
        else
        {
            otherMat.color = new Color(1, 1, 1, 1);
        }

        tempuratureObj.GetComponent<Renderer>().material = otherMat;
        curMat.color = new Color(this._currentHydration/this._colorDivisor, this._currentHydration / this._colorDivisor,
                                 this._currentHydration / this._colorDivisor, this._currentFatigue/ this._colorDivisor);
        GetComponent<Renderer>().material = curMat;
    }

    public void Jumped()
    {
        if (!checkLowHydration())
        {
            this._currentHydration -= this.hydrationEffectJumping;
        }
    }

    public void Sprinting()
    {
        if (this._playerManager.GetMovement() && !checkLowHydration())
        {
            this._currentHydration -= this.hydrationEffectSprinting;
        }
    }

    public void Climbing()
    {
        if (!checkLowHydration())
        {
            this._currentHydration -= this.hydrationEffectClimbing;
        }
    }

    bool checkLowHydration()
    {
        bool hydration0 = false;

        if (this._currentHydration <= this._zero)
        {
            this._currentHydration = this._zero;
            hydration0 = true;
        }

        return hydration0;
    }

    public void DrinkingWater()
    {
        if (this._currentHydration < this.maxHydration)
        {
            this._currentHydration += this.hydrationEffectDrinkingWater;
        }
        else
        {
            this._currentHydration = this.maxHydration;
        }
    }

    public void InShade()
    {
        if (this._currentTemperature > this._temperatureRangeMid)
        {
            this._currentTemperature -= this.temperatureEffect;
        }

        else if (this._currentTemperature < this._temperatureRangeMid)
        {
            this._currentTemperature += this.temperatureEffect;
        }
    }

    public void InHot()
    {
        if (this._currentTemperature < this.maxTemperature)
        {
            this._currentTemperature += this.temperatureEffect;
        }
        else
        {
            this._currentTemperature = this.maxTemperature;
        }
    }

    public void InCold()
    {
        if (this._currentTemperature > this.minTemperature)
        {
            this._currentTemperature -= this.temperatureEffect;
        }
        else
        {
            this._currentTemperature = this.minTemperature;
        }
    }

    public void LowerFatigue()
    {
        if (this.CheckCrisis())
        {
            Camera.main.gameObject.GetComponent<SceneController>().RestartScene();
        }
        else
        {
            this._currentFatigue -= this.fatigueDownRate;
            // I don't like this, I need callbacks for threshold values
            this.DetermineFatigue();
        }
    }

    bool CheckCrisis()
    {
        bool inCrisis = false;
        if (this._currentFatigue <= this._zero)
        {
            inCrisis = true;
        }
        return inCrisis;
    }

    public void DetermineFatigue()
    {
        if (this._currentFatigue > this.tiredFatigueRangeHigh)
        {
            this._playerFatigue = PlayerFatigue.NORMAL_FATIGUE;
            this._playerManager.PlayerIsNormal();
        }
        else if (this._currentFatigue > this.tiredFatigueRangeLow)
        {
            this._playerFatigue = PlayerFatigue.TIRED_FATIGUE;
            this._playerManager.PlayerIsTired();
        }
        else
        {
            this._playerFatigue = PlayerFatigue.EXHAUSTED_FATIGUE;
            this._playerManager.PlayerIsExhausted();
        }        
    }
}