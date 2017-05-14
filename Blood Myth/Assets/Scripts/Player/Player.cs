using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
    {
    public enum PlayerClimbingDirection
        {
        CLIMBING_UP,
        CLIMBING_DOWN,
        CLIMBING_RIGHT,
        CLIMBING_LEFT,
        NOT_CLIMBING
        }

    //Player Movement script
    private float _currentSpeed = 0;
    [Header("Movement Variables:")]
    // Movement speed
    public float normalSpeed = 10.0f;
    // Sprinting speed
    public float sprintSpeed = 15.0f;
    // Tired speed
    public float tiredSpeed = 5.0f;
    // Exhausted speed
    public float exhaustedSpeed = 2.5f;
    // Climbing speed
    public float climbingSpeed = 10.0f;
    // Jump velocity
    public float jumpVelocity = 15.0f;
    private PlayerClimbingDirection _climbDirection;

    //
    //Player
    private TouchInputData _primaryTouch;
    private TouchInputData _secondaryTouch;

    private float _currentFatigue;
    private float _currentHydration;
    private float _currentTemperature;
    private TemperatureEffect _hazardEffect;
    private bool _temperatureAffectingHydration = false;
    private int _totemPowers;
    private int _totemZero = 0;
    private Rigidbody2D _rb2D;
    private bool _climbingFatigued = false;
    private bool _jumpingFatigued = false;
    private bool _activelyClimbing = false;

    [Header("QA SECTION")]
    public float maxTemperature = 100.0f;
    public float maxHydration = 100.0f;
    public float maxFatigue = 100.0f;
    public float fatigueLoweringThreshold = 50.0f;
    public float temperatureAffectOnHydration = 75.0f;
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

    private float _zero = 0.00f;
    //

    private bool _facingRight;
    private Rigidbody2D _rigidBody;
    private float _move;
    private bool _iHaveChangedState = false, _moving = false, _sprinting = false, _jumping = false, _climbing = false, _canClimb = true;
    private bool _isGrounded;

    public Spine.Unity.SkeletonAnimation skeletonAnimation;
    public Transform groundCheck;
    public float _groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public FatigueStateBaseClass fatigueState;
    public NormalPlayer normalFatigue;
    public ExhaustedPlayer exhaustedFatigue;
    public TiredPlayer tiredFatigue;


    private void Start()
        {
        this._rb2D = this.GetComponent<Rigidbody2D>();
        normalFatigue = new NormalPlayer(this);
        tiredFatigue = new TiredPlayer(this);
        exhaustedFatigue = new ExhaustedPlayer(this);
        fatigueState = normalFatigue;
        this._rigidBody = this.GetComponent<Rigidbody2D>();
        this._facingRight = true;
        this.skeletonAnimation.state.SetAnimation(0, "Idle", true);
        }

    private void Update()
        {
        fatigueState.Update();
        }

    public void CheckOnGround()
        {
        this._isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this._groundRadius, this.whatIsGround);
        if(this._jumping && this._isGrounded)
            {
            this._jumping = false;
            this._iHaveChangedState = true;
            }
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

    public bool GetClimbing()
        {
        return this._climbing;
        }

    public void SetClimbing(bool climbing)
        {
        this._climbing = climbing;
        }

    public bool GetCanClimb()
        {
        return this._canClimb;
        }

    public void SetCanClimb(bool canClimb)
        {
        this._canClimb = canClimb;
        }

    //This will need to be refactored

    public void Jumped()
        {
        if (!this.checkLowHydration())
            {
            if (!this._temperatureAffectingHydration)
                {
                this._currentHydration -= this.hydrationEffectJumping;
                }
            else
                {
                this._currentHydration -= this.temperatureEffectJumping;
                }
            }
        }

    public void Sprinting()
        {
        if (this.GetMoving() && !this.checkLowHydration())
            {
            if (!this._temperatureAffectingHydration)
                {
                this._currentHydration -= this.hydrationEffectSprinting * Time.deltaTime;
                }
            else
                {
                this._currentHydration -= this.temperatureEffectSprinting * Time.deltaTime;
                }
            }
        }

    public void Climbing()
        {
        if (!this.checkLowHydration())
            {
            if (!this._temperatureAffectingHydration)
                {
                this._currentHydration -= this.hydrationEffectClimbing * Time.deltaTime;
                }
            else
                {
                this._currentHydration -= this.hydrationEffectClimbing * Time.deltaTime;
                }
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
            if (this._activelyClimbing)
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
            Camera.main.gameObject.GetComponent<SceneController>().RestartScene();
            }
        else
            {
            this._currentFatigue -= this.fatigueDownRate * Time.deltaTime;
            // I don't like this, I need callbacks for threshold values
            if (this._activelyClimbing)
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
            this.fatigueState = normalFatigue;
            }
        else if (this._currentFatigue > this.tiredFatigueRangeLow)
            {
            this.fatigueState = tiredFatigue;
            }
        else
            {
            this.fatigueState = exhaustedFatigue;
            }
        }

    public void DetermineFatigueClimbing()
        {
        if (this._currentFatigue > this.tiredFatigueRangeHigh)
            {
            this.fatigueState = normalFatigue;
            this.DeterminePlayerClimbDirection();
            }
        else if (this._currentFatigue > this.tiredFatigueRangeLow)
            {
            this.fatigueState = tiredFatigue;
            this.StoppedClimbing();
            this.PlayerIsTired();
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

    public TouchInputData getPrimaryTouch()
        {
        return this._primaryTouch;
        }

    public TouchInputData getSecondaryTouch()
        {
        return this._secondaryTouch;
        }

    //PlayerMovement Script
    public void StartedClimbingVertically()
        {
        this._rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        this._activelyClimbing = true;
        }

    public void ClimbingUp()
        {
        this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, this.climbingSpeed);
        }

    public void ClimbingDown()
        {
        this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, -this.climbingSpeed);
        }

    public void StartedClimbingHorizontally()
        {
        this._rb2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        this._activelyClimbing = true;
        }

    public void ClimbingRight()
        {
        this._rb2D.velocity = new Vector2(this.climbingSpeed, this._rb2D.velocity.y);
        }

    public void ClimbingLeft()
        {
        this._rb2D.velocity = new Vector2(-this.climbingSpeed, this._rb2D.velocity.y);
        }

    public void StationaryWhileClimbing()
        {
        this._rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

    public void StoppedClimbing()
        {
        this._rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        this._activelyClimbing = false;
        }

    //PlayerManager

    public void DeterminePlayerClimbDirection()
        {
        switch (this._climbDirection)
            {
            case Player.PlayerClimbingDirection.CLIMBING_UP:
                this.ClimbingUp();
                break;
            case Player.PlayerClimbingDirection.CLIMBING_DOWN:
                this.ClimbingDown();
                break;
            case Player.PlayerClimbingDirection.CLIMBING_RIGHT:
                this.ClimbingRight();
                break;
            case Player.PlayerClimbingDirection.CLIMBING_LEFT:
                this.ClimbingLeft();
                break;
            default:
                break;
            }
        }

    public void PlayerIsTired()
        {
        //_isTired = true;
        //_isExhausted = false;
        //this.SwitchPlayerState(this._tiredMovement);
        }

    public void inClimbingArea()
        {
        this._canClimb = true;
        GameObject.FindWithTag("Actions").GetComponent<SetActionIcon>().DisplayIcon(SetActionIcon.IconType.CLIMB);
        }

    public void outOfClimbingArea()
        {
        this._canClimb = false;
        GameObject.FindWithTag("Actions").GetComponent<SetActionIcon>().HideIcon();
        }

    public bool isFacingRight()
        {
        return this._facingRight;
        }

    public bool isActivelyClimbing()
        {
        return this._activelyClimbing;
        }

    public void setClimbingDirection(ClimbingAreas.ClimbingDirection inClimbDirection)
        {
        switch (inClimbDirection)
            {
            case ClimbingAreas.ClimbingDirection.CLIMBING_UP:
                this._climbDirection = PlayerClimbingDirection.CLIMBING_UP;
                break;
            case ClimbingAreas.ClimbingDirection.CLIMBING_DOWN:
                this._climbDirection = PlayerClimbingDirection.CLIMBING_DOWN;
                break;
            case ClimbingAreas.ClimbingDirection.CLIMBING_RIGHT:
                this._climbDirection = PlayerClimbingDirection.CLIMBING_RIGHT;
                break;
            case ClimbingAreas.ClimbingDirection.CLIMBING_LEFT:
                this._climbDirection = PlayerClimbingDirection.CLIMBING_LEFT;
                break;
            default:
                break;
            }
        }
    }