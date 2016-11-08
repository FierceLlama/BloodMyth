using UnityEngine;
using System.Collections;

public class ClimbingAreas : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().inClimbingArea();
        }
    }

    void OnTriggerExit2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().outOfClimbingArea();
        }
    }
}