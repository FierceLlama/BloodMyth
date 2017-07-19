using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBreakGround : MonoBehaviour
    {
    static bool doneThisBefore = false;

    void Start()
        {
        if (doneThisBefore)
            {
            this.gameObject.SetActive(false);
            }
        }

    void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player" && !doneThisBefore)
            {
            doneThisBefore = true;
            this.gameObject.SetActive(false);
            }
        }
    }