using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    //public float normSpeed = 25;//Movement speed
    public float maxMovement = 10;//Movement speed cap
    //public float sprintSpeed = 40;//Movement speed Sprinting
    public float sprintMaxMovement = 15; //Movement speed cap Sprinting
    // Jump velocity
    public float jumpVelocity = 20;

    //float curSpeed = 0; //Current Move speed
    float curMoveRate = 0;//Current move rate

    /*
    public float changeTempMove = 0.0001f;
    public float changeHydroMove = 0.0001f;

    public float changeTempSprint = 0.001f;
    public float changeHydroSprint = 0.001f;

    public float changeTempClimb  = 0.001f;
    public float changeHydroClimb = 0.001f;

    public float changeTempJump = 0.01f;
    public float changeHydroJump = 0.01f;
   //*/

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
        //curSpeed = normSpeed;
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
            //GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<BoxCollider2D>().isTrigger = true;
            this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, this.curMoveRate);
            // Call player script function to decrease hydration for climbing

        }
        else if (Input.GetKey(KeyCode.S) && this._canClimb)//Climbing Down
        {
            //GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<BoxCollider2D>().isTrigger = true;
            this._rb2D.velocity = new Vector2(this._rb2D.velocity.x, -this.curMoveRate);
            // Call player script function to decrease hydration for climbing       
        }
        else
        {
            //GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            curMoveRate = sprintMaxMovement;
            // Call player script function to decrease hydration for sprinting
            this._animController.SetBool("sprinting", true);
        }
        else
        {
            curMoveRate = maxMovement;
            this._animController.SetBool("sprinting", false);
        }

        // Jumping
        if (this._isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S)))
        {
            this._rb2D.AddForce(new Vector2(0, jumpVelocity));
            // Call player script function to decrease hydration for jumping
        }
    }

    /*
    Below code should be moved to separate script or behavior (strategy patterned) ***************************************************************
    */

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Climb")
            this._canClimb = true;

        if (col.gameObject.tag == "Water" && Input.GetKeyDown(KeyCode.W))
            Player.Hydration += 3;

        // Temperature effects
        if (col.gameObject.tag == "Shade")
        {
            if (Player.Tempurature > 95)
                Player.Tempurature -= 0.01f;

            else if (Player.Tempurature < 95)
                Player.Tempurature += 0.01f;
        }
        else if (col.gameObject.tag == "Hot")
        {
            Player.Tempurature += 0.01f;
        }

        else if (col.gameObject.tag == "Cold")
        {
            Player.Tempurature -= 0.01f;
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

    //void OnCollisionStay2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Ground")
    //    {
    //        isGrounded = true;
    //    }
    //}

    //void OnCollisionExit2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Ground")
    //    {
    //        isGrounded = false;
    //    }
    //}

    public bool isFacingRight()
    {
        return this._facingRight;
    }
}