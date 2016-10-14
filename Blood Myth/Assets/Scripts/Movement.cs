using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    private Player _player;
    public float maxMovement = 10;//Movement speed cap
    public float sprintMaxMovement = 15; //Movement speed cap Sprinting
    // Jump velocity
    public float jumpVelocity = 20;
    float curMoveRate = 0;//Current move rate

    private bool _isGrounded; //Is player on the ground
    private bool _canClimb = false; //Can the player climb

    //string curMouseZone = "";//Store mouse zone

    private Animator _animController;
    private bool _facingRight = true;
    private Rigidbody2D _rb2D;
    private SpriteRenderer _flip;
    private float _move;

    public Transform groundCheck;
    private float _groundRadius = 0.2f;
    public LayerMask whatIsGround;

    void Start()
    {
        this._player = GetComponent<Player>();
        curMoveRate = maxMovement;
        this._animController = this.gameObject.GetComponent<Animator>();
        this._rb2D = GetComponent<Rigidbody2D>();
        this._flip = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Check if player grounded
        this._isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this._groundRadius, this.whatIsGround);
        this._animController.SetBool("grounded", this._isGrounded);
        if (!this._isGrounded)
        {
            curMoveRate = maxMovement / 2;
        }

        this._move = Input.GetAxis("Horizontal");
        this._animController.SetFloat("speed", Mathf.Abs(this._move));
        // Sprite orientation
        if (this._move < 0 && this._facingRight)
        {
            this._flip.flipX = true;
            this._facingRight = false;
        }
        else if (this._move > 0 && !this._facingRight)
        {
            this._flip.flipX = false;
            this._facingRight = true;
        }
        // Movement is velocity in the x direction up to current move rate
        this._rb2D.velocity = new Vector2(this._move * this.curMoveRate, this._rb2D.velocity.y);
    }

    void Update()
    {
        //Climbing
        if (Input.GetKey(KeyCode.W) && this._canClimb)//Climbing Up
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, this.curMoveRate);
            this._player.playerClimbing();

        }
        else if (Input.GetKey(KeyCode.S) && this._canClimb)//Climbing Down
        {            
            GetComponent<BoxCollider2D>().isTrigger = true;
            this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, -this.curMoveRate);
            this._player.playerClimbing();
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            curMoveRate = sprintMaxMovement;
            this._player.playerSprinting();
            this._animController.SetBool("sprinting", true);
        }
        else
        {
            curMoveRate = maxMovement;
            this._animController.SetBool("sprinting", false);
        }

        // Jumping
        if (this._isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            this._rb2D.AddForce(new Vector2(0, jumpVelocity));
            this._player.playerJumped();
        }
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