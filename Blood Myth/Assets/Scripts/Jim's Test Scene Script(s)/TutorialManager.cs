using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
    public CanvasGroup interactButton, leftMoveButton, rightMoveButton, jumpButton;
    public PlayerMovement playerMovement;
    public UnityEngine.UI.Text instructionText;
    private bool _walkR = false, _walkL = false, _run = false, _jump = false, _interact = false;

    public GameObject leaf;
    public GameObject player;
    public Transform firstSpawn, secondSpawn;

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
        yield return new WaitForSeconds(5f);
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
        _walkR = true;
        }

    public IEnumerator iStartWalkLeft()
        {
        rightMoveButton.alpha = 0;
        yield return new WaitForSeconds(5f);
        instructionText.text = "Great, now tap the Left side of your screen to move Left";
        yield return new WaitForSeconds(1);
        leftMoveButton.alpha = 1;
        playerMovement._canMoveLeft = true;
        
        }

    public void PassedWalkLeft()
    {
        if (_walkL) return;
        StartCoroutine(iStartJump());
        _walkL = true;
    }

    public IEnumerator iStartJump()
    {
        leftMoveButton.alpha = 0;
        yield return new WaitForSeconds(5f);
        instructionText.text = "Now that you can move around, tap the Bottom of your screen to Jump, Remember you can jump while walking";
        yield return new WaitForSeconds(1);
        jumpButton.alpha = 1;
        playerMovement._canJump = true;
    }

    public void PassedJump()
    {
        if (_jump) return;
        StartCoroutine(iStartRun());
        _jump = true;
    }

    public IEnumerator iStartRun()
    {
        jumpButton.alpha = 0;
        yield return new WaitForSeconds(5f);
        instructionText.text = "To move faster, double-tap either Left or Right to Sprint";
        yield return new WaitForSeconds(1);
        rightMoveButton.alpha = 1;
        leftMoveButton.alpha = 1;
        playerMovement._canRun = true;
        
    }

    public void PassedRun()
    {
        if (_run) return;
        StartCoroutine(iEndRun());
        _run = true;
    }

    public IEnumerator iEndRun()
    {
        leftMoveButton.alpha = 0;
        rightMoveButton.alpha = 0;
        yield return new WaitForSeconds(0.5f);
        instructionText.text = "Now try Leaping over these rocks to continue forward";
        yield return new WaitForSeconds(5f);
        StartInteract();
    }

    public void StartInteract()
    {
        StartCoroutine(iStartInteract());
    }

    public IEnumerator iStartInteract()
    {
        yield return new WaitForSeconds(0.5f);
        instructionText.text = "You are DeHydrated, stand in  front of a waterfall and tap Up on your screen to drink water";
        yield return new WaitForSeconds(1);
        interactButton.alpha = 1;
        playerMovement._canInteract = true;
        
    }

    public void PassedInteract()
    {
        interactButton.alpha = 0;
        if (_interact) return;
        StartCoroutine(iPassed());
        _interact = true;
    }

    public IEnumerator iPassed()
    {
        instructionText.text = "By tapping the Top of your screen, you can interact with certain items in the world";
        yield return new WaitForSeconds(4);
        instructionText.text = "You can also use this to climp ladders by standing in front of the ladder and Tap and Holding the Top of your screen";
        yield return new WaitForSeconds(4);
        instructionText.text = "Great job! Now you can adventure through the story mode";
        yield return new WaitForSeconds(4);
        instructionText.text = "";
        yield return new WaitForSeconds(3);

        StartTut2();
    }
    //public void GameOnLoad(LoadSceneMode mode) { GameManager.Instance.ChangeGameState(GameStateId.Gameplay); }

        //PART 2

    public void StartTut2()
    {
        StartCoroutine(iStartTutorialPartDue());
    }

    public IEnumerator iStartTutorialPartDue()
    {
        player.transform.position = firstSpawn.transform.position;
        instructionText.text = "Now that you know the basics, let's see what is out there!";
        yield return new WaitForSeconds(4);
        instructionText.text = "Be careful of any Monsters!";
        yield return new WaitForSeconds(4);
        instructionText.text = "";
    }

    public void GoToSecondSpawn()
    {
        StartCoroutine(iStartSecondTutorial());
    }

    public IEnumerator iStartSecondTutorial()
    {
        player.transform.position = secondSpawn.transform.position;
        instructionText.text = "Oh no! You were not protected from the Fatigue Monster!";
        yield return new WaitForSeconds(4);
        instructionText.text = "Pick up leaves to gain protection from the Fatigue Monster";
        yield return new WaitForSeconds(4);
        instructionText.text = "";
    }

    public void LeafTutorial()
    {
        StartCoroutine(iLeafTutorial());
    }

    public IEnumerator iLeafTutorial()
    {
        Destroy(leaf, 0.5f);
        instructionText.text = "Great job! Now you are protected from harm!";
        yield return new WaitForSeconds(2);
        instructionText.text = "Continue your adventure without fear!";
        yield return new WaitForSeconds(2);
        instructionText.text = "";
    }

    public void EndTutorial()
    {
        StartCoroutine(iEndTutorial());
    }

    public IEnumerator iEndTutorial()
    {
        instructionText.text = "Great job! Now that you have defeated the Fatigue Monster, you are ready to set out on your adventure!";
        yield return new WaitForSeconds(4);
        instructionText.text = "";
    }
}
