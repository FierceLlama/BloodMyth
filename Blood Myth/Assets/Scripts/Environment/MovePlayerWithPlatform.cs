using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerWithPlatform : MonoBehaviour
    {
    void OnTriggerStay2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            float xOffset = inPlayer.gameObject.transform.position.x - this.gameObject.transform.position.x;
            float yOffset = inPlayer.gameObject.transform.position.y - this.gameObject.transform.position.y;
            Vector3 distance = inPlayer.gameObject.transform.position - this.gameObject.transform.position;
            GetComponent<MovingPlatform>().MovePlayer(inPlayer.gameObject, xOffset, yOffset);
            }
        }
    }