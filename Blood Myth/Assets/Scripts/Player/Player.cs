using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
    {
    //Player Movement script
    private float _currentSpeed = 0;
    [Header("Movement Variables:")]
    // Movement speed
    public float normalSpeed = 25.0f;
    // Sprinting speed
    public float sprintSpeed = 35.0f;
    // Tired speed
    public float tiredSpeed = 12.5f;
    // Tired Sprint speed
    public float tiredSprintSpeed = 22.5f;
    // Exhausted speed
    public float exhaustedSpeed = 6.25f;
    // Jump velocity
    public float jumpVelocity = 25.0f;

    private float _currentFatigue;
    private float _currentHydration;
    private float _currentTemperature;
    private TemperatureEffect _hazardEffect;
    private int _totemPowers;
    private int _totemZero = 0;
    private Rigidbody2D _rb2D;
    private bool _jumpingFatigued = false;

    [Header("QA SECTION")]
    public float maxTemperature = 100.0f;
    public float maxHydration = 100.0f;
    public float maxFatigue = 100.0f;
    public float fatigueLoweringThreshold = 50.0f;
    public float temperatureEffect = 25.0f;
    public float hydrationEffectSprinting = 2.0f;
    public float hydrationEffectJumping = 7.0f;
    public float temperatureEffectSprinting = 3.0f;
    public float temperatureEffectClimbing = 6.0f;
    public float temperatureEffectJumping = 7.5f;
    public float hydrationEffectDrinkingWater = 10.0f;
    public float tiredFatigueRangeHigh = 80.0f;
    public float tiredFatigueRangeLow = 30.0f;
    public float fatigueDownRate = 1.0f;
    public float fatigueRestingRate = 1.0f;
    public float fatigueHazardEffect = 50.0f;

    private float _zero = 0.00f;
    //

    private bool _facingRight;
    private Rigidbody2D _rigidBody;
    private float _move;
    private bool _iHaveChangedState = false, _moving = false, _sprinting = false, _jumping = false;
    private bool _isGrounded;
    private bool _lastIsGrounded;

    public Spine.Unity.SkeletonAnimation skeletonAnimation;
    public Transform groundCheck;
    public float _groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public FatigueStateBaseClass fatigueState;
    public NormalPlayer normalFatigue;
    public ExhaustedPlayer exhaustedFatigue;
    public TiredPlayer tiredFatigue;
    public FatigueStateBaseClass oldFatigueState;

    private bool _hydrationFX;
    private bool _tempFX;
    private EffectsManager _fxMan;

    private void Start()
        {
        this._currentFatigue = this.maxFatigue;
        this._currentHydration = this.maxHydration;
        this._currentTemperature = this.maxTemperature;
        this._rb2D = this.GetComponent<Rigidbody2D>();
        this.normalFatigue = new NormalPlayer(this);
        this.tiredFatigue = new TiredPlayer(this);
        this.exhaustedFatigue = new ExhaustedPlayer(this);
        this.fatigueState = this.normalFatigue;
        this.oldFatigueState = this.fatigueState;
        this._rigidBody = this.GetComponent<Rigidbody2D>();
        this._facingRight = true;
        this.skeletonAnimation.state.SetAnimation(0, "Idle", true);
        this._isGrounded = this._lastIsGrounded = false;
        this._hydrationFX = false;
        this._tempFX = false;
        this._fxMan = this.GetComponent<EffectsManager>();
        }

    private void Update()
        {
        this.CheckFatigue();
        this.fatigueState.Update();
        }

    public void CheckOnGround()
        {
        this._isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this._groundRadius, this.whatIsGround);
        if (this._jumping && this._isGrounded && (this._isGrounded != this._lastIsGrounded))
            {
            this._jumping = false;
            this._iHaveChangedState = true;
            }
        this._lastIsGrounded = this._isGrounded;
        }

    public bool GetGrounded()
        {
        return this._isGrounded;
        }

    public void SpriteDirection()
        {
        if (this._move < 0 && this._facingRight)
            {
            this.skeletonAnimation.skeleton.FlipX = true;
            this._facingRight = false;
            }
        else if (this._move > 0 && !this._facingRight)
            {
            this.skeletonAnimation.skeleton.FlipX = false;
            this._facingRight = true;
            }
        }

    public bool IsFacingRight()
        {
        return this._facingRight;
        }

    public void SetIHaveChangedState(bool changedState)
        {
        this._iHaveChangedState = changedState;
        }

    public bool GetIHaveChangedState()
        {
        return this._iHaveChangedState;
        }

    public float GetMove()
        {
        return this._move;
        }

    public void SetMove(float move)
        {
        this._move = move;
        }

    public Rigidbody2D GetRigidbody()
        {
        return this._rigidBody;
        }

    public bool GetJumping()
        {
        return this._jumping;
        }
    public void SetJumping(bool jumping)
        {
        this._jumping = jumping;
        }

    public bool GetMoving()
        {
        return this._moving;
        }

    public void SetMoving(bool moving)
        {
        this._moving = moving;
        }

    public bool GetSprinting()
        {
        return this._sprinting;
        }

    public void SetSprinting(bool sprinting)
        {
        this._sprinting = sprinting;
        }

    //This will need to be refactored

    public void LowerHydrationForJumping()
        {
        if (!this.checkLowHydration())
            {
            this._currentHydration -= this.hydrationEffectJumping;
            }
        }

    public void LowerHydrationForSprinting()
        {
        if (this.GetMoving() && !this.checkLowHydration())
            {
            this._currentHydration -= this.hydrationEffectSprinting * Time.deltaTime;
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

        if (this._hydrationFX && this._currentHydration >= this.fatigueLoweringThreshold)
            {
            this._hydrationFX = false;
            this._fxMan.ClearDehydrationFX();
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

        if (this._tempFX && this._currentTemperature >= this.fatigueLoweringThreshold)
            {
            this._tempFX = false;
            this._fxMan.ClearTempFX();
            }
        }

    public void TemperatureHazard(TemperatureEffect inHazard)
        {
        // This is for changing character effects
        this._hazardEffect = inHazard;
        if (this._currentTemperature > this._zero)
            {
            this._currentTemperature -= this.temperatureEffect;
            }
        else
            {
            this._currentTemperature = this._zero;
            }
        if (this._tempFX)
            {
            this.SetTempFX();
            }
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
            this.DetermineFatigueState();
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
            this._currentFatigue -= this.fatigueDownRate * Time.deltaTime;
            // I don't like this, I need callbacks for threshold values
            this.DetermineFatigueState();
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

    public void DetermineFatigueState()
        {
        if (this._currentFatigue > this.tiredFatigueRangeHigh)
            {
            this.fatigueState = this.normalFatigue;
            }
        else if (this._currentFatigue > this.tiredFatigueRangeLow)
            {
            this.fatigueState = this.tiredFatigue;
            }
        else
            {
            this.fatigueState = this.exhaustedFatigue;
            }

        if (this.oldFatigueState != this.fatigueState && !this._jumping)
            {
            this.SetIHaveChangedState(true);
            this.oldFatigueState = this.fatigueState;
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
            this.DetermineFatigueState();
            }
        else
            {
            this._currentFatigue = this.maxFatigue;
            }
        }

    public void CheckFatigue()
        {
        if (this._currentHydration <= this.fatigueLoweringThreshold)
            {
            this.LowerFatigue();
            if (!this._hydrationFX)
                {
                this._hydrationFX = true;
                this._fxMan.SetDehydrationFX();
                }
            }
        if (this._currentTemperature <= this.fatigueLoweringThreshold)
            {
            this.LowerFatigue();
            if (!this._tempFX)
                {
                this._tempFX = true;
                this.SetTempFX();
                }
            }
        }

    void SetTempFX()
        {
        switch (this._hazardEffect)
            {
            case TemperatureEffect.COLD_HAZARD:
                this._fxMan.SetColdFX();
                break;
            case TemperatureEffect.HOT_HAZARD:
                this._fxMan.SetHotFX();
                break;
            }
        }

    public bool isFacingRight()
        {
        return this._facingRight;
        }

    public void SetSpeed(float speed)
        {
        this._currentSpeed = speed;
        }

    public float GetSpeed()
        {
        return this._currentSpeed;
        }

    public bool fatigueForJumping()
        {
        return this._jumpingFatigued;
        }
    }