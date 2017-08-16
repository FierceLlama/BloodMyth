using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBreakGround : MonoBehaviour
    {
    private static bool doneThisShitAlready = false;

    void Start()
        {
        if (doneThisShitAlready)
            {
            this.gameObject.SetActive(false);
            }
        }

    void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player" && !doneThisShitAlready)
            {
            doneThisShitAlready = true;
            AudioManager.Instance.PlaySound("StoneCrumbling", AudioType.SFX);
            this.gameObject.SetActive(false);
            }
        }
    }