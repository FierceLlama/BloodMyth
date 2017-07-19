using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBreakGround : MonoBehaviour
    {
    void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this.gameObject.SetActive(false);
            }
        }
    }