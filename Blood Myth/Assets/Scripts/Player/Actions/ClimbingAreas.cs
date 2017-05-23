using UnityEngine;
using System.Collections;



public class ClimbingAreas : MonoBehaviour
{
    public enum ClimbingDirection
        {
        CLIMBING_UP,
        CLIMBING_DOWN,
        CLIMBING_RIGHT,
        CLIMBING_LEFT,
        NOT_CLIMBING
        }

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
            this._player.GetComponent<Player>().inClimbingArea();
            //this._player.GetComponent<Player>().setClimbingDirection(climbDirection);
            }
    }

    void OnTriggerStay2D(Collider2D inPlayer)
    {
        GameObject.FindWithTag("Actions").GetComponent<SetActionIcon>().DisplayIcon(SetActionIcon.IconType.CLIMB);
        //if (this._player.GetComponent<Player>().isActivelyClimbing())
        //    {
        //    this.globalClimbingArea.GetComponent<BoxCollider2D>().enabled = true;
        //    // Will be removed when we have actual level design and art assets
        //    if (this.platformToClimbThrough)
        //        {
        //        this.platformToClimbThrough.GetComponent<BoxCollider2D>().enabled = false;
        //        }
        //    this.otherClimbingArea.GetComponent<BoxCollider2D>().enabled = false;
        //    this.globalClimbingArea.GetComponent<ExitingClimbingArea>().setGameObjects(this.platformToClimbThrough, this.otherClimbingArea);
        //    }
        }

    private void OnTriggerExit2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player" /*&& !this._player.GetComponent<Player>().isActivelyClimbing()*/)
            {
            this._player.GetComponent<Player>().outOfClimbingArea();
            }
        if (inPlayer.gameObject.tag == "Player")
            {
            GameObject.FindWithTag("Actions").GetComponent<SetActionIcon>().HideIcon();
            }
        }
    }