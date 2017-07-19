using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringColdWater : MonoBehaviour
    {
    public string clipName;

    private void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            AudioManager.Instance.StopAudio(clipName);
            GameManager.Instance.GetComponent<BM_SceneManager>().ResetScene();
            }
        }
    }