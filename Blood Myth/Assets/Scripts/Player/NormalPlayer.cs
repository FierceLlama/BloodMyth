using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayer : MonoBehaviour
    {
    private Player player;

    private void Start()
        {
        player = this.GetComponent<Player>();
        }


    private void Update()
        {
        this.player.CheckOnGround();
#if UNITY_EDITOR
        this.player.SetMove(Input.GetAxis("Horizontal"));
        this.player.GetRigidbody().velocity = new Vector2(this.player.GetMove() * 25, this.player.GetRigidbody().velocity.y);
        if (Input.GetKey(KeyCode.LeftShift) && this.player.GetMove() != 0 && !this.player.GetSprinting())
            {
            this.player.SetSprinting(true);
            if (!this.player.GetJumping())
                {
                this.player.SetIHaveChangedState(true);
                }
            }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && this.player.GetMoving())
            {
            this.player.SetSprinting(false);
            if (!this.player.GetJumping())
                {
                this.player.SetIHaveChangedState(true);
                }
            }

        if (Input.GetKeyDown(KeyCode.Space) && !this.player.GetJumping() && this.player.GetGrounded())
            {
            this.player.SetJumping(true);
            this.player.SetIHaveChangedState(true);
            this.player.GetRigidbody().velocity = new Vector2(this.player.GetRigidbody().velocity.x, 20);
            }
#endif
        this.player.SpriteDirection();
        if (!this.player.GetJumping())
            {
            if (this.player.GetMove() != 0 && !this.player.GetMoving())
                {
                this.player.SetMoving(true);
                this.player.SetIHaveChangedState(true);
                }
            else if (this.player.GetMove() == 0 && this.player.GetMoving())
                {
                this.player.SetMoving(false);
                this.player.SetIHaveChangedState(true);
                }
            }

        if (this.player.GetIHaveChangedState())
            {
            if (this.player.GetJumping())
                {
                this.player.skeletonAnimation.state.SetAnimation(0, "Jump", false);
                }

            else if (this.player.GetMoving() && !this.player.GetJumping())
                {
                if (this.player.GetSprinting())
                    {
                    this.player.skeletonAnimation.state.SetAnimation(0, "Sprint", true);
                    }
                else
                    {
                    this.player.skeletonAnimation.state.SetAnimation(0, "Run", true);
                    }
                }
            else
                {
                this.player.skeletonAnimation.state.SetAnimation(0, "Idle", true);
                }
            this.player.SetIHaveChangedState(false);
            }
        }
    }