using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitingClimbingArea : MonoBehaviour
{
    public GameObject player;
    private GameObject _platformToClimbThrough;
    private GameObject _otherClimbingArea;

    private void OnTriggerExit2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            this.player.GetComponent<PlayerMovement>().outOfClimbingArea();
            this.player.GetComponent<PlayerManager>().setClimbingDirection(ClimbingDirection.NOT_CLIMBING);
            this._platformToClimbThrough.GetComponent<BoxCollider2D>().enabled = true;
            this._otherClimbingArea.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void setGameObjects(GameObject inPlatformToClimbThrough, GameObject inOtherClimbingArea)
    {
        this._platformToClimbThrough = inPlatformToClimbThrough;
        this._otherClimbingArea = inOtherClimbingArea;
    }
}