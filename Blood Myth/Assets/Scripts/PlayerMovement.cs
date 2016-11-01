using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;

    private bool _isGrounded; //Is player on the ground
    private bool _canClimb = false; //Can the player climb

    //string curMouseZone = "";//Store mouse zone

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
        if (!this._isGrounded)
        {
            this._currentSpeed = this.normalSpeed / 2.0f;
        }
        else
        {
            this._currentSpeed = this.normalSpeed;
        }
        this.Movement();
    }

    public void SprintingMovement()
    {
        if (!this._isGrounded)
        {
            this._currentSpeed = this.sprintSpeed / 2.0f;
        }
        else
        {
            this._currentSpeed = this.sprintSpeed;
        }
        this.Movement();
    }

    public void TiredMovement()
    {
        if (!this._isGrounded)
        {
            this._currentSpeed = this.tiredSpeed / 2.0f;
        }
        else
        {
            this._currentSpeed = this.tiredSpeed;
        }
        this.Movement();
    }

    public void ExahustedMovement()
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

    /*
    Below code should be moved to separate script or behavior (strategy patterned) ***************************************************************
    */

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Climb")
        {
            this._canClimb = true;
        }

        if (col.gameObject.tag == "Water" && Input.GetKeyDown(KeyCode.W))
        {
            this._player.playerDrinkingWater();
        }

        // Temperature effects
        if (col.gameObject.tag == "Shade")
        {
            this._player.playerInShade();
        }
        else if (col.gameObject.tag == "Hot")
        {
            this._player.playerHot();
        }

        else if (col.gameObject.tag == "Cold")
        {
            this._player.playerCold();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Climb")
        {
            this._canClimb = false;
        }
    }

    /*
    Above code should be moved to separate script or behavior (strategy patterned) *****************************************************************
    */

    public bool canClimb()
    {
        return this._canClimb;
    }

    public bool isFacingRight()
    {
        return this._facingRight;
    }
}