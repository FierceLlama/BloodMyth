using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private PlayerManager _playerManager;
    private CharacterController _controller;
    private Vector2 velocity;
    // Is player on the ground
    private bool _isGrounded;
    // Player interacting with climbing object
    private bool _canClimb = false;
    // Player's ability to climb/jump based on fatigue state
    private bool _climbingFatigued = false;
    private bool _jumpingFatigued = false;
    private bool _activelyClimbing = false;

    private Animator _animController;

    public Spine.Unity.SkeletonAnimation _skeletonAnimation;
    public bool _isSprinting = false;
    public bool _isJumping = false;
    public bool _isClimbing = false;

    private bool _facingRight = true;
    private SpriteRenderer _flipSprite;
    private float _x;
    private float _y;

    public Transform groundCheck;
    public float _groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private float _currentSpeed = 0;
    [Header("Movement Variables:")]
    // Movement speed
    public float normalSpeed = 10.0f;
    // Sprinting speed
    public float sprintSpeed= 15.0f;
    // Tired speed
    public float tiredSpeed = 5.0f;
    // Exhausted speed
    public float exhaustedSpeed = 2.5f;
    // Climbing speed
    public float climbingSpeed = 10.0f;
    // Jump velocity
    public float jumpVelocity = 15.0f;
    public float gravity = 20.0f;

    void Start()
    {
        this._skeletonAnimation = this.GetComponent<Spine.Unity.SkeletonAnimation>();
        this._animController = this.gameObject.GetComponent<Animator>();
        this._flipSprite = GetComponent<SpriteRenderer>();
        this._player = GetComponent<Player>();
        this._playerManager = GetComponent<PlayerManager>();
        this._controller = GetComponent<CharacterController>();
        }

    public void CheckOnGround()
    {
        this._isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this._groundRadius, this.whatIsGround);
        //this._animController.SetBool("grounded", this._isGrounded);

        }

    public bool GetGrounded()
    {
        return this._controller.isGrounded;
    }

    public void NormalMovement()
    {
        this.Movement();
    }

    public void SprintingMovement()
    {
        this.Movement();
    }

    public void TiredMovement()
    {
        this.Movement();
    }

    public void ExhaustedMovement()
    {
        this.Movement();
    }

    public void Movement()
    {
        //*
#if UNITY_ANDROID
        if (this._playerManager.getPrimaryTouch().CurrentScreenSection == ScreenSection.Right && this._playerManager.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary)
        {
            this._x = 1.0f;
        }
        else if (this._playerManager.getPrimaryTouch().CurrentScreenSection == ScreenSection.Left && this._playerManager.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary)
        {
            this._x = -1.0f;
        }
        else
        {
            this._x = 0.0f;
        }
#endif//*/

#if UNITY_EDITOR
        this._x = Input.GetAxis("Horizontal");
        this._y = Input.GetAxis("Vertical");
#endif

        //this._animController.SetFloat("speed", Mathf.Abs(this._move));
        if (Input.GetKeyDown(KeyCode.Space)
        && !this.fatigueForJumping()
        && this.GetGrounded())
            {
            this._playerManager.PlayerIsJumping();
            this._isJumping = true;
            }

        this.VerifySpriteDirection();

        // Movement is velocity in the x direction up to current move rate
        //this._rb2D.velocity = new Vector2(this._move * this._currentSpeed, this._rb2D.velocity.y);
        if (this._x != 0)
            {
            this.velocity.x = this._x;
            }

        this.velocity.y -= this.gravity * Time.fixedDeltaTime;
        this._controller.Move(new Vector3(velocity.x, velocity.y, 0) * Time.fixedDeltaTime);

        if (this._controller.isGrounded)
            {
            this.velocity.y = -this.gravity * Time.fixedDeltaTime;
            }

        if (this._isJumping && this._controller.isGrounded)
            {
            this._isJumping = false;
            this._playerManager.CheckPlayerFatigue();
            }
    }

    public bool GetMovement()
    {
        bool moving = false;
        if (Mathf.Abs(this._x) > 0)
        {
            moving = true;
        }
        return moving;
    }

    public void StartedClimbingVertically()
    {
        //this._rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        this._activelyClimbing = true;
    }

    public void ClimbingUp()
    {
        //this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, this.climbingSpeed);
        this._controller.Move(new Vector3(0, this.climbingSpeed, 0) * Time.fixedDeltaTime);
    }

    public void ClimbingDown()
    {
        //this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, -this.climbingSpeed);
        this._controller.Move(new Vector3(0, -this.climbingSpeed, 0) * Time.fixedDeltaTime);
        }

    public void StartedClimbingHorizontally()
    {
        //this._rb2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        this._activelyClimbing = true;
    }

    public void ClimbingRight()
    {
        //this._rb2D.velocity = new Vector2(this.climbingSpeed, this._rb2D.velocity.y);
        this._controller.Move(new Vector3(this.climbingSpeed, 0, 0) * Time.fixedDeltaTime);
        }

    public void ClimbingLeft()
    {
        //this._rb2D.velocity = new Vector2(-this.climbingSpeed, this._rb2D.velocity.y);
        this._controller.Move(new Vector3(-this.climbingSpeed, 0, 0) * Time.fixedDeltaTime);
        }

    public void StationaryWhileClimbing()
    {
        //this._rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public void StoppedClimbing()
    {
        //this._rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        this._activelyClimbing = false;
    }

    void VerifySpriteDirection()
    {
        if (this._x < 0 && this._facingRight)
        {
            //this._flipSprite.flipX = true;
            _skeletonAnimation.skeleton.FlipX = true;
            this._facingRight = false;
        }
        else if (this._x > 0 && !this._facingRight)
        {
            //this._flipSprite.flipX = false;
            _skeletonAnimation.skeleton.FlipX = false;
            this._facingRight = true;
        }
    }

    public void Jumped()
    {
        //this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, this.jumpVelocity);
        this._controller.Move(new Vector3(0, this.jumpVelocity, 0) * Time.fixedDeltaTime);
    }

    public void SetNormalMovementValues()
    {
        this._currentSpeed = this.normalSpeed;
        this._climbingFatigued = false;
        this._jumpingFatigued = false;
    }

    public void SetTiredMovementValues()
    {
        this._currentSpeed = this.tiredSpeed;
        this._climbingFatigued = true;
    }

    public void SetExhaustedMovementValues()
    {
        this._currentSpeed = this.exhaustedSpeed;
        this._jumpingFatigued = true;
    }

    public void SetSprintingMovementValues()
    {
        this._currentSpeed = this.sprintSpeed;
        //this._animController.SetBool("sprinting", true);
    }

    public void StoppedSprinting()
    {
        //this._animController.SetBool("sprinting", false);
    }

    public bool canClimb()
    {
        return this._canClimb;
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

    public bool fatigueForClimbing()
    {
        return this._climbingFatigued;
    }

    public bool fatigueForJumping()
    {
        return this._jumpingFatigued;
    }
}