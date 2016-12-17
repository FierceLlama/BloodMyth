using UnityEngine;
using System.Collections;

public class ClimbingAreas : MonoBehaviour
{
    public GameObject player;
    public ClimbingDirection climbDirection;

    void OnTriggerEnter2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().inClimbingArea();
            player.GetComponent<PlayerManager>().setClimbingDirection(climbDirection);
        }
    }

    void OnTriggerExit2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().outOfClimbingArea();
            player.GetComponent<PlayerManager>().setClimbingDirection(ClimbingDirection.NOT_CLIMBING);
        }
    }
}