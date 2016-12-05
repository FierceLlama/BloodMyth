using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    // Is player on the ground
    private bool _isGrounded;
    // Player interacting with climbing object
    private bool _canClimb = false;
    // Player's ability to climb based on fatigue state
    private bool _climbingFatigue = false;
    private bool _activelyClimbing = false;

    private Animator _animController;
    private bool _facingRight = true;
    private Rigidbody2D _rb2D;
    private SpriteRenderer _flipSprite;
    private float _move;

    public Transform groundCheck;
    private float _groundRadius = 0.2f;
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

    void Start()
    {
        this._animController = this.gameObject.GetComponent<Animator>();
        this._rb2D = GetComponent<Rigidbody2D>();
        this._flipSprite = GetComponent<SpriteRenderer>();
        this._player = GetComponent<Player>();
    }

    public void CheckOnGround()
    {
        this._isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this._groundRadius, this.whatIsGround);
        this._animController.SetBool("grounded", this._isGrounded);
    }

    public bool GetGrounded()
    {
        return this._isGrounded;
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

    void Movement()
    {
        this._move = Input.GetAxis("Horizontal");
        this._animController.SetFloat("speed", Mathf.Abs(this._move));

        this.VerifySpriteDirection();

        // Movement is velocity in the x direction up to current move rate
        this._rb2D.velocity = new Vector2(this._move * this._currentSpeed, this._rb2D.velocity.y);
    }

    public bool GetMovement()
    {
        bool moving = false;
        if (Mathf.Abs(this._move) > 0)
        {
            moving = true;
        }
        return moving;
    }

    public void StartedClimbing()
    {
        this._rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        this._activelyClimbing = true;
    }

    public void ClimbingVerticallyUp()
    {
        this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, this.climbingSpeed);
    }

    public void ClimbingVerticallyDown()
    {
        this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, -this.climbingSpeed);
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

    void VerifySpriteDirection()
    {
        if (this._move < 0 && this._facingRight)
        {
            this._flipSprite.flipX = true;
            this._facingRight = false;
        }
        else if (this._move > 0 && !this._facingRight)
        {
            this._flipSprite.flipX = false;
            this._facingRight = true;
        }
    }

    public void Jumped()
    {
        this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, this.jumpVelocity);
    }

    public void SetNormalMovementValues()
    {
        this._currentSpeed = this.normalSpeed;
    }

    public void SetTiredMovementValues()
    {
        this._currentSpeed = this.tiredSpeed;
        this._climbingFatigue = true;
    }

    public void SetExhaustedMovementValues()
    {
        this._currentSpeed = this.exhaustedSpeed;
    }

    public void SetSprintingMovementValues()
    {
        this._currentSpeed = this.sprintSpeed;
        this._animController.SetBool("sprinting", true);
    }

    public void StoppedSprinting()
    {
        this._animController.SetBool("sprinting", false);
    }

    public bool canClimb()
    {
        return this._canClimb;
    }

    public void inClimbingArea()
    {
        this._canClimb = true;
    }

    public void outOfClimbingArea()
    {
        this._canClimb = false;
    }

    public bool isFacingRight()
    {
        return this._facingRight;
    }

    public bool isActivelyClimbing()
    {
        return this._activelyClimbing;
    }
}