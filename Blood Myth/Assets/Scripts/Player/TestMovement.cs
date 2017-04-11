using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
    {
    public CharacterController controller;
    public Vector2 velocity;
    public float jumpSpeed = 25.0f;
    public float gravity = 65.0f;
    public Spine.Unity.SkeletonAnimation skeletonAnimation;
    public bool jumping;

    void Awake()
        {
        this.controller = GetComponent<CharacterController>();
        }

    void Update()
        {
        //control inputs
        float x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
            velocity.y = jumpSpeed;
            //skeletonAnimation.AnimationName = "Jump_Normal";
            //jumping = true;
            //StartCoroutine(Jump());
            }


        if (x != 0)
            {
            //walk or run
            velocity.x = x * 15.0f;
            }

        //apply gravity F = mA (Learn it, love it, live it)
        velocity.y -= gravity * Time.deltaTime;

        //move
        controller.Move(new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime);

        if (controller.isGrounded)
            {
            //cancel out Y velocity if on ground
            velocity.y = -gravity * Time.deltaTime;
            }

        //graphics updates
        if (controller.isGrounded && !jumping)
            {
            if (x == 0) //idle
                skeletonAnimation.AnimationName = "Idle_Normal";
            else //move
                skeletonAnimation.AnimationName = "Run_Normal";
            }
        else
            {
            if (velocity.y > 0) //jump
                {
                skeletonAnimation.AnimationName = "Jump_Normal";
                }
            }
        }

    IEnumerator Jump()
        {
        yield return new WaitForSeconds(.5f);
        velocity.y = jumpSpeed;
        jumping = false;
        }
    }