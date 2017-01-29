using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTest : MonoBehaviour {

    int x = 1;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            GameManager.Instance.ChangeGameState(GameStateId.Pause);
            Debug.Log("Paused");
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            GameManager.Instance.ChangeGameState(GameStateId.Gameplay);
            Debug.Log("UnPaused");
        }

        Debug.Log(++x * Time.timeScale);
    }
       
}
