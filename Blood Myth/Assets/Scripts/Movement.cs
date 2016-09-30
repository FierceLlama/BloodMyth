 using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    
    public float normSpeed = 25;//Movement speed
    public float maxMovement = 10;//Movement speed cap

    public float sprintSpeed = 40;//Movement speed Sprinting
    public float sprintMaxMovement = 15; //Movement speed cap Sprinting

    float curSpeed = 0; //Current Move speed
    float curMoverate = 0;//Current move rate

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

	void Start () 
    {
        curSpeed = normSpeed;
        curMoverate = maxMovement;
        this._animController = this.gameObject.GetComponent<Animator>();
	}
	
	
	void Update () 
    {
        //Keyboard Movement
        //*
        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            curSpeed = sprintSpeed;
            curMoverate = sprintMaxMovement;
            Player.Hydration -= 0.001f;
            Player.Tempurature += 0.001f;
            this._animController.SetBool("Sprinting", true);
        }
        else if (!isGrounded)//Slower in air
        {
            curSpeed = normSpeed / 2;
        }
        else
        {
            curSpeed = normSpeed;
            curMoverate = maxMovement;
            this._animController.SetBool("Sprinting", false);
        }
        //*
        //Moving
        if (Input.GetKey(KeyCode.A) && GetComponent<Rigidbody2D>().velocity.x > -curMoverate)//Moving Left
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-curSpeed, 0));
            this._animController.SetBool("Moving", true);

        }
        else if (Input.GetKey(KeyCode.D) && GetComponent<Rigidbody2D>().velocity.x < curMoverate)//Moving Right
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(curSpeed, 0));
            this._animController.SetBool("Moving", true);
        }

        //Climbing
        if (Input.GetKey(KeyCode.W) && canClimb)//Climbing Up
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, curSpeed * 5));
            Player.Hydration -= 0.01f;
            Player.Tempurature += 0.01f;

        }
        else if (Input.GetKey(KeyCode.S) && canClimb)//Climbing Down
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -curSpeed * 5));
            Player.Hydration -= 0.01f;
            Player.Tempurature += 0.01f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.S) && isGrounded)//Jumping
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, curSpeed * 15));
            Player.Hydration -= 0.1f;
            Player.Tempurature += 0.1f;
            this._animController.SetBool("Jumping", true);
        }
        //*/

        //Mouse Movement
        //*

        // Check mouse position quadrant
        if (Input.GetMouseButton(0))
        {
            CheckZone();
            if (curMouseZone == "Left" && GetComponent<Rigidbody2D>().velocity.x > -curMoverate)//Movement
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-curSpeed, 0));
                this.GetComponent<SpriteRenderer>().flipX = true;
                this._animController.SetBool("Moving", true);
            }
            else if (curMouseZone == "Right" && GetComponent<Rigidbody2D>().velocity.x < curMoverate)//Movement
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(curSpeed, 0));
                this.GetComponent<SpriteRenderer>().flipX = false;
                this._animController.SetBool("Moving", true);
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
                this._animController.SetTrigger("Jumping");
            }

        }

        //this._animController.SetBool("Moving", false);
        //*/
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude) < 1.0f)
        {
            this._animController.SetBool("Moving", false);
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
        
        if(col.gameObject.tag == "Climb")
            canClimb = true;

        if (col.gameObject.tag == "Water" && Input.GetKeyDown(KeyCode.W))
            Player.Hydration += 3;


        if (col.gameObject.tag == "Shade")
        {
            if( Player.Tempurature > 95)
                Player.Tempurature -= 0.01f;  

            else if( Player.Tempurature < 95)
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
        if(col.gameObject.tag == "Climb")
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
