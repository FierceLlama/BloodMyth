using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextScene : MonoBehaviour
    {
    void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this.GetComponent<StopBackgroundMusic>().StopMusic();
            GameManager.Instance.GetComponent<BM_SceneManager>().LoadNextScene();
            }
        }
    }