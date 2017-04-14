using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leafy : MonoBehaviour {

    public TutorialManager tP2;
    public KillerCloud cloud;

    void OnTriggerEnter2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            cloud.charIsCovered = true;
            tP2.LeafTutorial();
        }
    }
}
