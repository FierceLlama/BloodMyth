﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayer : FatigueStateBaseClass
    {
    private Player _player;

    public NormalPlayer(Player inPlayer)
        {
        this._player = inPlayer;
        }

    public override void Enter()
        {
        throw new NotImplementedException();
        }

    public override void Update()
        {
        this._player.CheckOnGround();

#if UNITY_ANDROID
        if ((this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Bottom || this._player.getSecondaryTouch().CurrentScreenSection == ScreenSection.Bottom)
            && !this._player.fatigueForJumping()
            && this._player.GetGrounded())
            {
            this._player.SetJumping(true);
            this._player.SetIHaveChangedState(true);
            this._player.GetRigidbody().velocity = new Vector2(this._player.GetRigidbody().velocity.x, this._player.jumpVelocity);
            }

        if (/*this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Right && this._player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary*/
            this._player.GetMovingRight())
        {
            this._player.SetMove(1.0f);
        }
        else if (/*this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Left && this._player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary*/
                this._player.GetMovingLeft())
        {
            this._player.SetMove(-1.0f);
        }
        else
        {
            this._player.SetMove(0.0f);
        }
        this._player.GetRigidbody().velocity = new Vector2(this._player.GetMove() * this._player.GetSpeed(), this._player.GetRigidbody().velocity.y);

        if ((this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Right || this._player.getPrimaryTouch().CurrentScreenSection == ScreenSection.Left)
                && this._player.getPrimaryTouch().getTouchTapCount() >= 2 && this._player.getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary && !this._player.GetSprinting())
            {
            this._player.SetSprinting(true);
            if (!this._player.GetJumping())
                {
                this._player.SetIHaveChangedState(true);
                }
            }
        // Need to find a way to kill sprint...but this should be a non issue when using the new UI
        else if (this._player.getPrimaryTouch().getTouchTapCount() == 0)
            {
            this._player.SetSprinting(false);
            if (!this._player.GetJumping())
                {
                this._player.SetIHaveChangedState(true);
                }
            }
#endif

#if UNITY_EDITOR
        if ((Input.GetKeyDown(KeyCode.Space) || InputManager.instance.GetJumpActive()) && !this._player.GetJumping() && this._player.GetGrounded())
            {
            this._player.SetJumping(true);
            this._player.SetIHaveChangedState(true);
            this._player.GetRigidbody().velocity = new Vector2(this._player.GetRigidbody().velocity.x, this._player.jumpVelocity);
            }

        //this._player.SetMove(Input.GetAxis("Horizontal"));
        if (InputManager.instance.GetRightActive())
            {
            this._player.SetMove(1.0f);
            }
        else if (InputManager.instance.GetLeftActive())
            {
            this._player.SetMove(-1.0f);
            }
        else
            {
            this._player.SetMove(0.0f);
            }
        this._player.GetRigidbody().velocity = new Vector2(this._player.GetMove() * this._player.GetSpeed(), this._player.GetRigidbody().velocity.y);
        if ((Input.GetKey(KeyCode.LeftShift) || InputManager.instance.GetSprintActive()) && this._player.GetMove() != 0 && !this._player.GetSprinting())
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
                this._player.skeletonAnimation.state.SetAnimation(0, "Jump", false);
                this._player.LowerHydrationForJumping();
                }

            else if (this._player.GetMoving() && !this._player.GetJumping())
                {
                if (this._player.GetSprinting())
                    {
                    this._player.skeletonAnimation.state.SetAnimation(0, "Sprint", true);
                    this._player.SetSpeed(this._player.sprintSpeed);
                    }
                else
                    {
                    this._player.skeletonAnimation.state.SetAnimation(0, "Run", true);
                    this._player.SetSpeed(this._player.normalSpeed);
                    }
                }
            else
                {
                this._player.skeletonAnimation.state.SetAnimation(0, "Idle", true);
                }
            this._player.SetIHaveChangedState(false);
            }

        if (this._player.GetSprinting())
            {
            this._player.LowerHydrationForSprinting();
            }
        }
    }