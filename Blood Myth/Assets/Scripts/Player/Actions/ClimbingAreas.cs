using UnityEngine;
using System.Collections;

public enum ClimbingDirection
    {
    CLIMB_UP,
    CLIMB_DOWN,
    NOT_CLIMBING
    }

public class ClimbingAreas : MonoBehaviour
{
    private GameObject _player;
    public ClimbingDirection climbDirection;
    public GameObject globalClimbingArea;
    public GameObject otherClimbingArea;
    public GameObject platformToClimbThrough;

    void Start()
    {
        this._player = GameObject.FindWithTag("Player");
        this.globalClimbingArea.GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {            
            //this._player.GetComponent<PlayerMovement>().inClimbingArea();
            //this._player.GetComponent<PlayerManager>().setClimbingDirection(climbDirection);
        }
    }

    void OnTriggerStay2D(Collider2D inPlayer)
    {
        //if (this._player.GetComponent<PlayerMovement>().isActivelyClimbing())
        //{
        //    this.globalClimbingArea.GetComponent<BoxCollider2D>().enabled = true;
        //    // Will be removed when we have actual level design and art assets
        //    if (this.platformToClimbThrough)
        //    {
        //        this.platformToClimbThrough.GetComponent<BoxCollider2D>().enabled = false;
        //    }
        //    this.otherClimbingArea.GetComponent<BoxCollider2D>().enabled = false;
        //    this.globalClimbingArea.GetComponent<ExitingClimbingArea>().setGameObjects(this.platformToClimbThrough, this.otherClimbingArea);
        //}
    }

    private void OnTriggerExit2D(Collider2D inPlayer)
        {
        //if (inPlayer.gameObject.tag == "Player" && !this._player.GetComponent<PlayerMovement>().isActivelyClimbing())
        //    {
        //    this._player.GetComponent<PlayerMovement>().outOfClimbingArea();
        //    }
        }
    }