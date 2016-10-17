using UnityEngine;
using System.Collections;

/// <summary>
/// TODO (OMAR) Research Logging systems ---> will help debugging in this and next projects.
/// TODO (OMAR) Make sure this manager doesn't become a God Object.. Define specific behaviour and never increase its role.
/// TODO (OMAR) Keep Comments Updated.
/// TODO (OMAR) Rework Folder structure (remove systems folder) when done.
/// </summary>
public class GameManager : MonoBehaviour {

    [SerializeField]
    GameStateManager gsManScript;

	// Use this for initialization
	void Awake ()
    {
        gsManScript = GetComponent<GameStateManager>();
	}

    void InitGame()
    {
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
