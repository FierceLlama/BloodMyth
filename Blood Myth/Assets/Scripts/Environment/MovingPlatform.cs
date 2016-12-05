using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public GameObject leftStart;
    public GameObject rightStart;
    public float platformSpeed;
    public float pauseTime;
    public float stoppingDistance;

    private GameObject _xForm;
    private bool _movingRight = true;
    private float _currentTime;
    private float _zero = 0.0f;

    void Start()
    {
        this._xForm = this.gameObject;
    }

    void Update()
    {
        if (this._movingRight)
        {
            if (!this.checkDistance(this.rightStart.transform))
            {
                this._xForm.transform.position = this.rightStart.transform.position;
                this.holdPlatform();
            }
            else
            {
                this.movePlatform(this.rightStart.transform);
            }
        }
        else
        {
            if (!this.checkDistance(this.leftStart.transform))
            {
                this._xForm.transform.position = this.leftStart.transform.position;
                this.holdPlatform();
            }
            else
            {
                this.movePlatform(this.leftStart.transform);
            }
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

    void movePlatform(Transform endPos)
    {
        this._xForm.transform.position = Vector2.MoveTowards(this._xForm.transform.position, endPos.position, Time.deltaTime * this.platformSpeed);
    }

    void holdPlatform()
    {
        this._currentTime += Time.deltaTime;
        if (this._currentTime >= this.pauseTime)
        {
            this._currentTime = this._zero;
            this._movingRight = !this._movingRight;
        }
    }
}