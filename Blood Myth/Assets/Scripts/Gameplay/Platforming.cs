using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforming : MonoBehaviour
    {
    public GameObject trigger;

    void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this.trigger.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

    void OnTriggerExit2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this.trigger.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }