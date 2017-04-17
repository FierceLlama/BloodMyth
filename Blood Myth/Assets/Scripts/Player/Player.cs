using UnityEngine;
using System.Collections;

public enum PlayerFatigue
{
    NORMAL_FATIGUE,
    TIRED_FATIGUE,
    EXHAUSTED_FATIGUE
}

public enum TemperatureEffect
{
    COLD_HAZARD,
    HOT_HAZARD
}

public class Player : MonoBehaviour
{
    // Temperature change stuffies -- will be removed later
    Material curMat;
    Material otherMat;
    public GameObject tempuratureObj;

    private PlayerFatigue _playerFatigue;
    private PlayerManager _playerManager;
    private PlayerMovement _playerMovement;
    private float _zero = 0.0f;
    private float _colorDivisor = 100.0f;
    private int _totemPowers;
    private int _totemZero = 0;

    private float _currentFatigue;
    private float _currentHydration;
    private float _currentTemperature;
    private TemperatureEffect _hazardEffect;
    private bool _temperatureAffectingHydration = false;

    [Header("QA SECTION")]
    public float maxTemperature = 100.0f;
    public float maxHydration = 100.0f;
    public float maxFatigue = 100.0f;
    public float fatigueLoweringThreshold = 50.0f;
    public float temperatureEffect = 25.0f;
    public float hydrationEffectSprinting = 2.0f;
    public float hydrationEffectClimbing = 4.0f;
    public float hydrationEffectJumping = 5.0f;
    public float temperatureEffectSprinting = 3.0f;
    public float temperatureEffectClimbing = 6.0f;
    public float temperatureEffectJumping = 7.5f;
    public float hydrationEffectDrinkingWater = 10.0f;
    public float tiredFatigueRangeHigh = 80.0f;
    public float tiredFatigueRangeLow = 30.0f;
    public float fatigueDownRate = 1.0f;
    public float fatigueRestingRate = 10.0f;
    public float fatigueHazardEffect = 50.0f;

    // Use this for initialization
    void Start () 
    {
        this._playerManager = GetComponent<PlayerManager>();
        this._playerMovement = GetComponent<PlayerMovement>();
        curMat = GetComponent<Renderer>().material;
        otherMat = tempuratureObj.GetComponent<Renderer>().material;
        this._playerFatigue = PlayerFatigue.NORMAL_FATIGUE;
        this._currentFatigue = maxFatigue;
        this._currentHydration = maxHydration;
        this._currentTemperature = maxTemperature;
        
        this.UpdateMat();
    }

    private void Update()
        {
        this.HydrationCheck();
        this.TemperatureCheck();


        }

    void HydrationCheck()
        {
        // Hydration Check
        if (this._currentHydration <= this.fatigueLoweringThreshold)
            {
            // Lower Fatigue
            this.LowerFatigue();
            }
        }

    void TemperatureCheck()
        {
        // Temp Check
        if (this._currentTemperature <= this.fatigueLoweringThreshold)
            {
            // Lower Fatigue
            this.LowerFatigue();
            }
        }

    public void UpdateMat()
    {
        if (this._currentTemperature < this.fatigueLoweringThreshold)
            {
            switch (this._hazardEffect)
                {
                case TemperatureEffect.COLD_HAZARD:
                    otherMat.color = new Color(0, 0, 1, 1);
                    break;
                case TemperatureEffect.HOT_HAZARD:
                    otherMat.color = new Color(1, 0, 0, 1);
                    break;
                }
            }
        else
            {
            otherMat.color = Color.white;
            }

        tempuratureObj.GetComponent<Renderer>().material = otherMat;
        curMat.color = new Color(this._currentHydration/this._colorDivisor, this._currentHydration / this._colorDivisor,
                                 this._currentHydration / this._colorDivisor, this._currentFatigue/ this._colorDivisor);
        GetComponent<Renderer>().material = curMat;
    }

    public void Jumped()
    {
        if (!this.checkLowHydration())
        {
            this._currentHydration -= this.hydrationEffectJumping;
        }
    }

    public void Sprinting()
    {
        if (/*this._playerManager.GetMovement() && */!this.checkLowHydration())
        {
            this._currentHydration -= this.hydrationEffectSprinting * Time.deltaTime;
        }
    }

    public void Climbing()
    {
        if (!this.checkLowHydration())
        {
            this._currentHydration -= this.hydrationEffectClimbing * Time.deltaTime;
        }
        this.DetermineFatigueClimbing();
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
        if (this._currentTemperature < this.maxTemperature)
        {
            this._currentTemperature += this.temperatureEffect;
        }
        else
        {
            this._currentTemperature = this.maxTemperature;
        }
    }

    public void TemperatureHazard(TemperatureEffect inHazard)
    {
        this._hazardEffect = inHazard;
        if (this._currentTemperature > this._zero)
        {
            this._currentTemperature -= this.temperatureEffect;
        }
        else
        {
            this._currentTemperature = this._zero;
        }
        this.UpdateMat();
    }

    public void FatigueHazard()
    {
        this._currentFatigue -= this.fatigueHazardEffect;
        if (this.CheckCrisis())
        {
            Camera.main.gameObject.GetComponent<SceneController>().RestartScene();
        }
        else
        {
            if (this._playerMovement.isActivelyClimbing())
            {
                this.DetermineFatigueClimbing();
            }
            else
            {
                this.DetermineFatigue();
            }
        }
    }

    public void LowerFatigue()
    {
        if (this.CheckCrisis())
        {
            Camera.main.gameObject.GetComponent<SceneController>(). RestartScene();
        }
        else
        {
            this._currentFatigue -= this.fatigueDownRate * Time.deltaTime;
            // I don't like this, I need callbacks for threshold values
            if (this._playerMovement.isActivelyClimbing())
            {
                this.DetermineFatigueClimbing();
            }
            else
            {
                this.DetermineFatigue();
            }
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
            if (this._playerManager.getMoveAction() == MoveActions.SPRINTING_ACTION)
            {
                this._playerManager.PlayerIsSprinting();
            }
            else
            {
                this._playerManager.PlayerIsNormal();
            }
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

    public void DetermineFatigueClimbing()
    {
        if (this._currentFatigue > this.tiredFatigueRangeHigh)
        {
            this._playerFatigue = PlayerFatigue.NORMAL_FATIGUE;
            this._playerManager.DeterminePlayerClimbDirection();
        }
        else if (this._currentFatigue > this.tiredFatigueRangeLow)
        {
            this._playerFatigue = PlayerFatigue.TIRED_FATIGUE;
            this._playerMovement.StoppedClimbing();
            this._playerManager.PlayerIsTired();
        }
    }

    public void addTotemPowers()
    {
        this._totemPowers++;
    }

    public void subtractTotemPowers()
    {
        if (this._totemPowers > this._totemZero)
        {
            this._totemPowers--;
        }
    }

    public int getTotemPowers()
    {
        return this._totemPowers;
    }

    public void Resting()
    {
        if (this._currentFatigue < this.maxFatigue)
        {
            this._currentFatigue += this.fatigueRestingRate * Time.deltaTime;
        }
    }
}