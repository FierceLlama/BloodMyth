using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
    {
    public GameObject gameMan;

    private void Awake()
        {
        GameObject gObj = GameObject.FindWithTag("GameManager");
        if (gObj)
            {
            DestroyImmediate(gObj);
            }
        Instantiate(gameMan);
        }
    }