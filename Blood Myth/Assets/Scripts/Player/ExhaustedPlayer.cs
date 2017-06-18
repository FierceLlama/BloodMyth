using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustedPlayer : FatigueStateBaseClass
    {
    private Player _player;

    public ExhaustedPlayer(Player inPlayer)
        {
        this._player = inPlayer;
        }

    public override void Enter()
        {
        Debug.Log("Exhausted State");
        }

    public override void Update()
        {
        this._player.CheckOnGround();
        
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

        this._player.SpriteDirection();

        if (this._player.GetMove() != 0 && !this._player.GetMoving())
            {
            this._player.SetMoving(true);
            this._player.SetIHaveChangedState(true);
            }
        else if (this._player.GetMove() == 0 && this._player.GetMoving())
            {
            this._player.SetMoving(false);
            this._player.SetIHaveChangedState(true);
            }

        if (this._player.GetIHaveChangedState())
            {
            if (this._player.GetMoving())
                {
                this._player.skeletonAnimation.state.SetAnimation(0, "Walk_Exhausted", true);
                }
            else
                {
                this._player.skeletonAnimation.state.SetAnimation(0, "Idle_Exhausted", true);
                }
            this._player.SetIHaveChangedState(false);
            }
        }
    }