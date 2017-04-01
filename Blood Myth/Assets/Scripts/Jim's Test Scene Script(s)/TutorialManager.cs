using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
    public CanvasGroup interactButton, leftMoveButton, rightMoveButton, jumpButton;
    public PlayerManager playerManager;

    // Use this for initialization
    void Start ()
        {
        playerManager._isMoveRight = false;
        playerManager._isMoveLeft = false;
        playerManager._isJump = false;
        playerManager._isInteract = false;
        }
	
    public IEnumerator iStartTutorial()
        {

        yield return new WaitForSeconds(1);
        }

    //public void GameOnLoad(LoadSceneMode mode) { GameManager.Instance.ChangeGameState(GameStateId.Gameplay); }

    // Update is called once per frame
    void Update ()
        {
		
	    }
}
