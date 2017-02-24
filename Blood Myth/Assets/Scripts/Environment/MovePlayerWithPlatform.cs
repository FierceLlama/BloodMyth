using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerWithPlatform : MonoBehaviour
    {
    void OnTriggerStay2D(Collider2D inPlayer)
        {
        //StartCoroutine("FuckMe");

        if (inPlayer.gameObject.tag == "Player")
            {
            GetComponent<MovingPlatform>().MovePlayer(inPlayer.gameObject);
            }
        }

    //IEnumerator FuckMe()
    //    {
    //    yield return new WaitForFixedUpdate();
    //    }
    }