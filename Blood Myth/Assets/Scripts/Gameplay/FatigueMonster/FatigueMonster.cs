using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatigueMonster : MonoBehaviour
    {
    public Spine.Unity.SkeletonAnimation skeletonAnimation;
    public bool flipX;

    void Start()
        {
        flipX = flipX ^ false;
        this.skeletonAnimation.state.SetAnimation(0, "Idle", true);
        this.skeletonAnimation.Skeleton.FlipX = flipX;
        }
    }