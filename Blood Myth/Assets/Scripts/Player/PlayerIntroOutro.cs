using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntroOutro : MonoBehaviour
{
    public Spine.Unity.SkeletonAnimation skeletonAnimation;
    public GameObject dialogueObject;
    public string diagKey;

    void Start()
    {
        this.skeletonAnimation.state.SetAnimation(0, "Idle", true);
        DialogueManager.Instance.StartIntroOutroDialogue(diagKey, dialogueObject);
    }
}
