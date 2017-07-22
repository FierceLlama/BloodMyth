using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatigueMonster : MonoBehaviour
    {
    public Spine.Unity.SkeletonAnimation skeletonAnimation;

    void Start()
        {
        this.skeletonAnimation.state.SetAnimation(0, "Idle", true);
        }
    }