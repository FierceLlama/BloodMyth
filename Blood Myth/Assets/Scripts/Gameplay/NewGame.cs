using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
    {
    public void StartNewGame()
        {
        GameManager.Instance.GetComponent<BM_SceneManager>().StartNewGame();
        }
    }