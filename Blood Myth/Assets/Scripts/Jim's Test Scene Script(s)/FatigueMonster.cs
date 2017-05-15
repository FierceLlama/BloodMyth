using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatigueMonster : MonoBehaviour {

    public float timeToTake;
    private GameObject _xForm;
    public GameObject left;
    public GameObject right;
    public float stoppingDistance = 0.000f;
    private bool goingRight = false;
    private bool goingLeft = false;
    private bool waiting = false;

    void Start()
    {
        goingRight = true;

        this._xForm = this.gameObject;
        this._xForm.transform.position = this.left.transform.position;
    }

    void FixedUpdate()
    {
        if (checkDistance(this.right.transform) && goingRight)
        {
            this.MoveTo();
        }
        else if (!checkDistance(this.right.transform) && goingRight)
        {
            goingRight = false;
            goingLeft = true;
            this._xForm.transform.position = this.right.transform.position;
        }
        if(checkDistance(this.left.transform) && goingLeft)
        {
            this.MoveFrom();
        }
        else if (!checkDistance(this.left.transform) && goingLeft)
        {
            goingLeft = false;
            goingRight = true;
            this._xForm.transform.position = this.left.transform.position;
        }
    }

    bool checkDistance(Transform endPos)
    {
        bool distanceCheck = false;
        if (Vector2.Distance(this._xForm.transform.position, endPos.position) > this.stoppingDistance)
        {
            distanceCheck = true;
        }
        return distanceCheck;
    }

    void MoveTo()
    {
        this._xForm.transform.position = Vector2.MoveTowards(this._xForm.transform.position, right.transform.position, Time.deltaTime * timeToTake);
    }

    void MoveFrom()
    {
        this._xForm.transform.position = Vector2.MoveTowards(this._xForm.transform.position, left.transform.position, Time.deltaTime * timeToTake);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            
        }
    }


}
