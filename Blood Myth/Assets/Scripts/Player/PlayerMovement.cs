using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
    {
    private Player _player;
    private PlayerManager _playerManager;
    // Is player on the ground
    private bool _isGrounded;
    public bool _inAir = false;
    // Player interacting with climbing object
    private bool _canClimb = false;
    // Player's ability to climb/jump based on fatigue state
    private bool _climbingFatigued = false;
    private bool _jumpingFatigued = false;
    private bool _activelyClimbing = false;

    private Animator _animController;

    public Spine.Unity.SkeletonAnimation _skeletonAnimation;
    public bool _isJumping = false;
    public bool _isClimbing = false;

    private bool _facingRight = true;
    public Rigidbody2D rb2D;
    private SpriteRenderer _flipSprite;
    public float move;

    public Transform groundCheck;
    public float _groundRadius = 0.02f;
    public LayerMask whatIsGround;

    public float currentSpeed = 0;
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

    void Start()
        {
        this._skeletonAnimation = this.GetComponent<Spine.Unity.SkeletonAnimation>();
        this._animController = this.gameObject.GetComponent<Animator>();
        this.rb2D = GetComponent<Rigidbody2D>();
        this._flipSprite = GetComponent<SpriteRenderer>();
        this._player = GetComponent<Player>();
        }

    public void CheckOnGround()
        {
        this._isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this._groundRadius, this.whatIsGround);
        }

    public bool GetGrounded()
        {
        if (this._inAir)
            {
            this._isGrounded = !this._inAir;
            }
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
        this.VerifySpriteDirection();
        }

    public bool GetMovement()
        {
        bool moving = false;
        if (Mathf.Abs(this.move) > 0)
            {
            moving = true;
            }
        return moving;
        }

    public void StartedClimbingVertically()
        {
        this.rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        this._activelyClimbing = true;
        }

    public void ClimbingUp()
        {
        this.rb2D.velocity = new Vector2(this.rb2D.velocity.x, this.climbingSpeed);
        }

    public void ClimbingDown()
        {
        this.rb2D.velocity = new Vector2(this.rb2D.velocity.x, -this.climbingSpeed);
        }

    public void StartedClimbingHorizontally()
        {
        this.rb2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        this._activelyClimbing = true;
        }

    public void ClimbingRight()
        {
        this.rb2D.velocity = new Vector2(this.climbingSpeed, this.rb2D.velocity.y);
        }

    public void ClimbingLeft()
        {
        this.rb2D.velocity = new Vector2(-this.climbingSpeed, this.rb2D.velocity.y);
        }

    public void StationaryWhileClimbing()
        {
        this.rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

    public void StoppedClimbing()
        {
        this.rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        this._activelyClimbing = false;
        }

    void VerifySpriteDirection()
        {
        if (this.move < 0 && this._facingRight)
            {
            _skeletonAnimation.skeleton.FlipX = true;
            this._facingRight = false;
            }
        else if (this.move > 0 && !this._facingRight)
            {
            _skeletonAnimation.skeleton.FlipX = false;
            this._facingRight = true;
            }
        }

    public void Jumped()
        {
        this.rb2D.velocity = new Vector2(this.rb2D.velocity.x, this.jumpVelocity);
        this._inAir = true;
        }

    public void SetNormalMovementValues()
        {
        this.currentSpeed = this.normalSpeed;
        this._climbingFatigued = false;
        this._jumpingFatigued = false;
        }

    public void SetTiredMovementValues()
        {
        this.currentSpeed = this.tiredSpeed;
        this._climbingFatigued = true;
        }

    public void SetExhaustedMovementValues()
        {
        this.currentSpeed = this.exhaustedSpeed;
        this._jumpingFatigued = true;
        }

    public void SetSprintingMovementValues()
        {
        this.currentSpeed = this.sprintSpeed;
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

    private void OnCollisionEnter2D(Collision2D collision)
        {
        if (this._inAir && collision.gameObject.tag == "Ground")
            {
            this._inAir = false;
            this._player.DetermineFatigue();
            }
        }
    }