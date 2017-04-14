using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerCloud : MonoBehaviour {

    public bool charIsCovered =false;
    public TutorialManager tP2;

    void OnTriggerEnter2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            if (!charIsCovered)
            {
                tP2.GoToSecondSpawn();
            }
            else
            {
                tP2.EndTutorial();
                Destroy(this.gameObject);
            }
        }
    }
}
