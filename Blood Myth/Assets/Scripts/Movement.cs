using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
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

    private float _currentMoveRate = 0;
    [Header("Movement Variables:")]
    // Movement speed cap
    public float maxMovement = 10;
    // Sprinting speed cap
    public float sprintMaxMovement = 15;
    // Jump velocity
    public float jumpVelocity = 20;

    void Start()
    {
        this._animController = this.gameObject.GetComponent<Animator>();
        this._rb2D = GetComponent<Rigidbody2D>();
        this._flipSprite = GetComponent<SpriteRenderer>();
    }

    public void CheckOnGround()
    {
        this._isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this._groundRadius, this.whatIsGround);
        this._animController.SetBool("grounded", this._isGrounded);
    }

    public void NormalMovement()
    {
        if (!this._isGrounded)
        {
            this._currentMoveRate = this.maxMovement / 2.0f;
        }
        this.PlayerMovement();
    }

    public void SprintingMovement()
    {
        if (!this._isGrounded)
        {
            this._currentMoveRate = this.sprintMaxMovement / 2.0f;
        }
        this.PlayerMovement();
    }

    void PlayerMovement()
    {
        this._move = Input.GetAxis("Horizontal");
        this._animController.SetFloat("speed", Mathf.Abs(this._move));

        this.VerifySpriteDirection();

        // Movement is velocity in the x direction up to current move rate
        this._rb2D.velocity = new Vector2(this._move * this._currentMoveRate, this._rb2D.velocity.y);
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

    public void PlayerJumped()
    {
        this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, this.jumpVelocity);
    }

    public void SetNormalMovementValues()
    {
        this._currentMoveRate = this.maxMovement;
    }

    public void SetTiredMovementValues()
    {
    }

    public void SetExhaustedMovementValues()
    {
    }

    public void SetSprintingMovementValues()
    {
        this._currentMoveRate = this.sprintMaxMovement;
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

    public bool isFacingRight()
    {
        return this._facingRight;
    }
}