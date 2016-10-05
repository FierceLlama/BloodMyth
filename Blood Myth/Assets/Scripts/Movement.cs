using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{


    public float normSpeed = 25;//Movement speed
    public float maxMovement = 10;//Movement speed cap

    public float sprintSpeed = 40;//Movement speed Sprinting
    public float sprintMaxMovement = 15; //Movement speed cap Sprinting
    // Jump velocity
    public float jumpVelocity = 20;

    float curSpeed = 0; //Current Move speed
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


    bool isGrounded = false; //Is player on the ground
    bool canClimb = false; //Can the player climb

    string curMouseZone = "";//Store mouse zone

    private Animator _animController;
    private bool _facingRight = true;
    private bool _sprinting = false;
    private Rigidbody2D _rb2D;
    private SpriteRenderer _flip;

    public enum MoveStates
    {
        Idle = 0,
        Walking = 1,
        Sprinting = 2,
        Jumping = 3,
    };
    private MoveStates moveState = MoveStates.Idle;

    void Start()
    {
        curSpeed = normSpeed;
        curMoveRate = maxMovement;
        this._animController = this.gameObject.GetComponent<Animator>();
        this._animController.SetInteger("moveState", (int)moveState);
        this._rb2D = GetComponent<Rigidbody2D>();
        this._flip = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        //Keyboard Movement
        //*
        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            this._sprinting = true;
            curSpeed = sprintSpeed;
            curMoveRate = sprintMaxMovement;
            Player.Hydration -= 0.001f;
            Player.Tempurature += 0.001f;
            moveState = MoveStates.Sprinting;
            this._animController.SetInteger("moveState", (int)moveState);
        }
        else
        {
            curSpeed = normSpeed;
            curMoveRate = maxMovement;
        }
        //*
        //Moving
        //if (Input.GetKey(KeyCode.A)/* && GetComponent<Rigidbody2D>().velocity.x > -curMoveRate*/)//Moving Left
        //{
        //    GetComponent<Rigidbody2D>().AddForce(-transform.right * curSpeed);
        //    GetComponent<SpriteRenderer>().flipX = true;
        //    moveState = MoveStates.Walking;
        //    this._animController.SetInteger("moveState", (int)moveState);

        //}
        //else if (Input.GetKey(KeyCode.D)/* && GetComponent<Rigidbody2D>().velocity.x < curMoveRate*/)//Moving Right
        //{
        //    GetComponent<Rigidbody2D>().AddForce(transform.right * curSpeed);
        //    GetComponent<SpriteRenderer>().flipX = false;
        //    moveState = MoveStates.Walking;
        //    this._animController.SetInteger("moveState", (int)moveState);
        //}

        float move = Input.GetAxis("Horizontal");
        // Sprite orientation
        if (move < 0 && this._facingRight)
        {
            this._flip.flipX = true;
            this._facingRight = false;
        }
        else if (move > 0 && !this._facingRight)
        {
            this._flip.flipX = false;
            this._facingRight = true;
        }
        // Add velocity up to max move rate
        this._rb2D.velocity = new Vector2(move * this.curMoveRate, this._rb2D.velocity.y);
        // Need to fiddle with these values for the movement shit (probably want to change the state machine to better reflect what we want (i.e. sprint bool and move bool)
        if (Mathf.Abs(this._rb2D.velocity.x) > 0 && !this._sprinting)
        {
            moveState = MoveStates.Walking;
        }
        // Update animations
        this._animController.SetInteger("moveState", (int)moveState);

        //Climbing
        if (Input.GetKey(KeyCode.W) && canClimb)//Climbing Up
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().AddForce(transform.up * curSpeed);
            Player.Hydration -= 0.01f;
            Player.Tempurature += 0.01f;

        }
        else if (Input.GetKey(KeyCode.S) && canClimb)//Climbing Down
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().AddForce(-transform.up * curSpeed);
            Player.Hydration -= 0.01f;
            Player.Tempurature += 0.01f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        //Jumping
        //if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S)))//Jumping
        //{
        //    GetComponent<Rigidbody2D>().AddForce(transform.up * curSpeed);
        //    Player.Hydration -= 0.1f;
        //    Player.Tempurature += 0.1f;
        //    moveState = MoveStates.Jumping;
        //    this._animController.SetInteger("moveState", (int)moveState);
        //}
        //*/

        

        //Mouse Movement
        //*

        // Check mouse position quadrant
        if (Input.GetMouseButton(0))
        {
            CheckZone();
            if (curMouseZone == "Left"/* && GetComponent<Rigidbody2D>().velocity.x > -curMoveRate*/)//Movement
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-curSpeed, 0));
                GetComponent<SpriteRenderer>().flipX = true;
                moveState = MoveStates.Walking;
                this._animController.SetInteger("moveState", (int)moveState);
            }
            else if (curMouseZone == "Right" /*&& GetComponent<Rigidbody2D>().velocity.x < curMoveRate*/)//Movement
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(curSpeed, 0));
                GetComponent<SpriteRenderer>().flipX = false;
                moveState = MoveStates.Walking;
                this._animController.SetInteger("moveState", (int)moveState);
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (curMouseZone == "Top")//Action Button
            {
                //Always Climbing
                //                if (canClimb)//Climbing Up
                //                {
                //                    GetComponent<Rigidbody2D>().gravityScale = 0;
                //                    GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                //                    GetComponent<BoxCollider2D>().isTrigger = true;
                //                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, curSpeed * 5));
                //                    Player.Hydration -= 0.01f;
                //                    Player.Tempurature += 0.01f;
                //
                //                }


            }
            else if (curMouseZone == "Bottom" && isGrounded)//Jump Button
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, curSpeed * 15));
                Player.Hydration -= 0.1f;
                Player.Tempurature += 0.01f;
                this.moveState = MoveStates.Jumping;
                this._animController.SetInteger("moveState", (int)this.moveState);
            }

        }

        //this._animController.SetBool("Moving", false);
        //*/
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude) < 1.0f && isGrounded)
        {
            this.moveState = MoveStates.Idle;
            this._animController.SetInteger("moveState", (int)this.moveState);
        }
    }

    void Update()
    {
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S)))
        {
            this.moveState = MoveStates.Jumping;
            this._animController.SetInteger("moveState", (int)this.moveState);
            this._rb2D.AddForce(new Vector2(0, jumpVelocity));
        }

        if (!isGrounded)
        {
            curMoveRate = normSpeed / 2;
            this.moveState = MoveStates.Jumping;
            this._animController.SetInteger("moveState", (int)this.moveState);
        }
    }

    void CheckZone()
    {
        if (Input.mousePosition.y > ((float)Screen.height / (float)Screen.width) * Input.mousePosition.x)
        {

            if (Input.mousePosition.y > (-(float)Screen.height / (float)Screen.width) * Input.mousePosition.x + Screen.height) // Top Button
            {
                curMouseZone = "Top";
            }
            else // Left Button
            {
                //Debug.Log("Left");
                //GetComponent<Rigidbody2D>().AddForce(new Vector2(-curSpeed, 0));
                curMouseZone = "Left";
            }

        }
        else
        {

            if (Input.mousePosition.y > (-(float)Screen.height / (float)Screen.width) * Input.mousePosition.x + Screen.height)// Right Button
            {
                //Debug.Log("Right");
                //GetComponent<Rigidbody2D>().AddForce(new Vector2(curSpeed, 0));
                curMouseZone = "Right";

            }
            else  // Bottom button
            {
                //Debug.Log("Bottom");
                curMouseZone = "Bottom";
            }

        }
    }


    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Climb")
            canClimb = true;

        if (col.gameObject.tag == "Water" && Input.GetKeyDown(KeyCode.W))
            Player.Hydration += 3;


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
            canClimb = false;

    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}