using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
    {
    private bool _facingRight;
    private Rigidbody2D _rigidBody;
    private float _move;
    private bool _currentState = false, _moving = false, _sprinting = false, _jumping = false;
    private bool _isGrounded;

    public Spine.Unity.SkeletonAnimation skeletonAnimation;
    public Transform groundCheck;
    public float _groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private void Start()
        {
        this._rigidBody = this.GetComponent<Rigidbody2D>();
        this._facingRight = true;
        this.skeletonAnimation.state.SetAnimation(0, "Idle", true);
        }

    private void Update()
        {
        this.CheckOnGround();
#if UNITY_EDITOR
        this._move = Input.GetAxis("Horizontal");
        this._rigidBody.velocity = new Vector2(this._move * 25, this._rigidBody.velocity.y);
        if (Input.GetKey(KeyCode.LeftShift) && this._move != 0 && !this._sprinting)
            {
            this._sprinting = true;
            this._currentState = true;
            }
        else if(Input.GetKeyUp(KeyCode.LeftShift) && this._moving)
            {
            this._sprinting = false;
            if (!this._jumping)
                {
                this._currentState = true;
                }
            }

        if (Input.GetKeyDown(KeyCode.Space)&& !this._jumping && this.GetGrounded())
            {
            this._jumping = true;
            _currentState = true;
            this._rigidBody.velocity = new Vector2(this._rigidBody.velocity.x, 20);
            }
#endif
        this.SpriteDirection();
        if (!this._jumping)
            {
            if (this._move != 0 && !this._moving)
                {
                this._moving = true;
                this._currentState = true;
                }
            else if (this._move == 0 && this._moving)
                {
                this._moving = false;
                this._currentState = true;
                }
            }

        if (this._currentState)
            {
            if (this._jumping)
                {
                this.skeletonAnimation.state.SetAnimation(0, "Jump", false);
                }
            else if (this._moving && !this._jumping)
                {
                if (this._sprinting)
                    {
                    this.skeletonAnimation.state.SetAnimation(0, "Sprint", true);
                    }
                else
                    {
                    this.skeletonAnimation.state.SetAnimation(0, "Run", true);
                    }
                }
            else
                {
                this.skeletonAnimation.state.SetAnimation(0, "Idle", true);
                }
            this._currentState = false;
            }

        //this._lastState = this._currentState;
        }

    public void CheckOnGround()
        {
        this._isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this._groundRadius, this.whatIsGround);
        if(this._jumping && this._isGrounded)
            {
            this._jumping = false;
            this._currentState = true;
            }
        }

    public bool GetGrounded()
        {
        return this._isGrounded;
        }

    private void SpriteDirection()
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
    }