using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
    public CanvasGroup interactButton, leftMoveButton, rightMoveButton, jumpButton;
    public PlayerMovement playerMovement;
    public UnityEngine.UI.Text instructionText;
    private bool _walkR = false, _walkL = false, _run = false, _jump = false, _interact = false;

    // Use this for initialization
    void Start ()
        {
        playerMovement._canMoveRight = false;
        playerMovement._canMoveLeft = false;
        playerMovement._canJump = false;
        playerMovement._canInteract = false;
        StartCoroutine(iStartTutorial());
        }
	
    public IEnumerator iStartTutorial()
        {
        instructionText.text = "Welcome to Blood Myth";
        yield return new WaitForSeconds(4);
        instructionText.text = "Here we will teach you how to play";
        yield return new WaitForSeconds(4);
        instructionText.text = "To start, tap the Right side of your screen to move Right";
        yield return new WaitForSeconds(1);
        rightMoveButton.alpha = 1;
        playerMovement._canMoveRight = true;
        }

    public void PassedWalkRight()
        {
        if (_walkR) return;
        StartCoroutine(iStartWalkLeft());
        }

    public IEnumerator iStartWalkLeft()
        {
        rightMoveButton.alpha = 0;
        yield return new WaitForSeconds(2f);        
        instructionText.text = "Great, now tap the Left side of your screen to move Left";
        yield return new WaitForSeconds(1);
        leftMoveButton.alpha = 1;
        playerMovement._canMoveLeft = true;
        _walkR = true;
        }

    public void PassedWalkLeft()
    {
        if (_walkL) return;
        StartCoroutine(iStartJump());
    }

    public IEnumerator iStartJump()
    {
        leftMoveButton.alpha = 0;
        yield return new WaitForSeconds(0.5f);
        instructionText.text = "Now that you can move around, tap the Bottom of your screen to Jump, Remember you can jump while walking";
        yield return new WaitForSeconds(1);
        jumpButton.alpha = 1;
        playerMovement._canJump = true;
        _jump = true;
    }

    public void PassedJump()
    {
        if (_run) return;
        StartCoroutine(iStartRun());
    }

    public IEnumerator iStartRun()
    {
        jumpButton.alpha = 0;
        yield return new WaitForSeconds(0.5f);
        instructionText.text = "To move faster, double-tap either Left or Right to Sprint";
        yield return new WaitForSeconds(1);
        rightMoveButton.alpha = 1;
        leftMoveButton.alpha = 1;
        playerMovement._canRun = true;
        _run = true;
    }

    public void PassedRun()
    {
        if (_interact) return;
        StartCoroutine(iStartInteract());
    }

    public IEnumerator iStartInteract()
    {
        leftMoveButton.alpha = 0;
        rightMoveButton.alpha = 0;
        yield return new WaitForSeconds(0.5f);
        instructionText.text = "You are DeHydrated, stand in  front of a waterfall and tap Up on your screen to drink water";
        yield return new WaitForSeconds(1);
        interactButton.alpha = 1;
        playerMovement._canInteract = true;
        _interact = true;
    }

    public void PassedInteract()
    {
        if (_interact) return;
        StartCoroutine(iPassed());
    }

    public IEnumerator iPassed()
    {
        instructionText.text = "By tapping the Top of your screen, you can interact with certain items in the world";
        yield return new WaitForSeconds(4);
        instructionText.text = "You can also use this to climp ladders by standing in front of the ladder and Tap and Holding the Top of your screen";
        yield return new WaitForSeconds(4);
        instructionText.text = "Great job! Now you can adventure through the story mode";
        yield return new WaitForSeconds(15);
        GameManager.Instance.LoadGameScene(0);
    }
    //public void GameOnLoad(LoadSceneMode mode) { GameManager.Instance.ChangeGameState(GameStateId.Gameplay); }

    // Update is called once per frame
    void Update ()
        {
		
	    }
}
