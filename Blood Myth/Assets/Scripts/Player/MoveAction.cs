using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
    {
    public enum MoveDirection
        {
        MOVE_LEFT,
        MOVE_RIGHT
        }

    public MoveDirection moveDir;
    private Player _playerScript;

    void Start()
        {
        this._playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        }

    public void Moving()
        {
        switch(this.moveDir)
            {
            case MoveDirection.MOVE_LEFT:
                this._playerScript.MovingLeft();
                break;
            case MoveDirection.MOVE_RIGHT:
                this._playerScript.MovingRight();
                break;
            }
        }

    public void NotMoving()
        {
        switch (this.moveDir)
            {
            case MoveDirection.MOVE_LEFT:
                this._playerScript.NotMovingLeft();
                break;
            case MoveDirection.MOVE_RIGHT:
                this._playerScript.NotMovingRight();
                break;
            }
        }
    }