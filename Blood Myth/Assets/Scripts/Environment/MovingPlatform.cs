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
    private Vector3 _lastPos;

    void Start()
    {
        this._xForm = this.gameObject;
    }

    void FixedUpdate()
    {
        this._lastPos = this._xForm.transform.position;

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

    public void MovePlayer(GameObject inPlayer)
        {
        Vector2 local = inPlayer.GetComponent<Rigidbody2D>().position;
        Vector2 platform = this._xForm.transform.position;
        //float distance = local.x - platform.x;
        //inPlayer.transform.position.Set(this._xForm.transform.position.x, this._xForm.transform.position.y, this._xForm.transform.position.z);
        //local.x = platform.x + inXOffset;
        //local.y = platform.y + inYOffset;
        //local = new Vector2(platform.x/* + inXOffset*/, local.y/* + inYOffset*/);
        //inPlayer.transform.position.Set(local.x, local.y, inPlayer.transform.position.z);
        //inPlayer.GetComponent<Rigidbody2D>().MovePosition(local);
        //inPlayer.GetComponent<Rigidbody2D>().position = new Vector2(platform.x  + distance/*+ inXOffset*/, inPlayer.GetComponent<Rigidbody2D>().position.y/* + inYOffset*/);

        Vector2 distanceBWPlayerPlatform = inPlayer.transform.position - this._lastPos;
        inPlayer.transform.position = new Vector2(platform.x + distanceBWPlayerPlatform.x, platform.y + distanceBWPlayerPlatform.y);
        //Vector2 target = new Vector2(platform.x + distanceBWPlayerPlatform.x, platform.y + distanceBWPlayerPlatform.y);
        //inPlayer.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(inPlayer.GetComponent<Rigidbody2D>().position, target, Time.deltaTime * this.platformSpeed);
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