using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredPlayer : FatigueStateBaseClass
    {
    private Player _player;

    public TiredPlayer(Player inPlayer)
        {
        this._player = inPlayer;
        }

    public override void Enter()
        {
        Debug.Log("Tired State");
        }

    public override void Update()
        {
        this._player.CheckOnGround();
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space) && !this._player.GetJumping() && this._player.GetGrounded())
            {
            this._player.SetJumping(true);
            this._player.SetIHaveChangedState(true);
            this._player.GetRigidbody().velocity = new Vector2(this._player.GetRigidbody().velocity.x, this._player.jumpVelocity);
            }

        this._player.SetMove(Input.GetAxis("Horizontal"));
        this._player.GetRigidbody().velocity = new Vector2(this._player.GetMove() * this._player.GetSpeed(), this._player.GetRigidbody().velocity.y);
        if (Input.GetKey(KeyCode.LeftShift) && this._player.GetMove() != 0 && !this._player.GetSprinting())
            {
            this._player.SetSprinting(true);
            if (!this._player.GetJumping())
                {
                this._player.SetIHaveChangedState(true);
                }
            }
        else if (!Input.GetKey(KeyCode.LeftShift) && this._player.GetSprinting())
            {
            this._player.SetSprinting(false);
            if (!this._player.GetJumping())
                {
                this._player.SetIHaveChangedState(true);
                }
            }
#endif
        this._player.SpriteDirection();
        if (!this._player.GetJumping())
            {
            if (this._player.GetMove() != 0 && !this._player.GetMoving())
                {
                this._player.SetMoving(true);
                this._player.SetIHaveChangedState(true);
                }
            else if (this._player.GetMove() == 0 && this._player.GetMoving())
                {
                this._player.SetMoving(false);
                this._player.SetSprinting(false);
                this._player.SetIHaveChangedState(true);
                }
            }

        if (this._player.GetIHaveChangedState())
            {
            if (this._player.GetJumping())
                {
                this._player.skeletonAnimation.state.SetAnimation(0, "Jump_Tired", false);
                this._player.LowerHydrationForJumping();
                }

            else if (this._player.GetMoving() && !this._player.GetJumping())
                {
                if (this._player.GetSprinting())
                    {
                    this._player.skeletonAnimation.state.SetAnimation(0, "Run_Tired", true);
                    this._player.SetSpeed(this._player.tiredSprintSpeed);
                    }
                else
                    {
                    this._player.skeletonAnimation.state.SetAnimation(0, "Walk_Tired", true);
                    this._player.SetSpeed(this._player.tiredSpeed);
                    }
                }
            else
                {
                this._player.skeletonAnimation.state.SetAnimation(0, "Idle_Tired", true);
                }
            this._player.SetIHaveChangedState(false);
            }
        }
    }
