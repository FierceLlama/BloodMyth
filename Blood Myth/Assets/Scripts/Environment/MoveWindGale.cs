using UnityEngine;
using System.Collections;

public class MoveWindGale : MonoBehaviour
    {
    public float timeToTake;
    private GameObject _xForm;
    public GameObject start;
    public GameObject end;
    public float stoppingDistance;
    public float delayTime;
    public string clipName;
    public float delayStartUp;

    void Start()
        {
        this._xForm = this.gameObject;
        this._xForm.transform.position = this.start.transform.position;
        StartCoroutine(DelayInitialize());
        }

    void FixedUpdate()
        {
        if (checkDistance(this.end.transform))
            {
            this.MoveTo();
            }
        else
            {
            StartCoroutine(DelayStart());
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
        this._xForm.transform.position = Vector2.MoveTowards(this._xForm.transform.position, end.transform.position, Time.deltaTime * timeToTake);
        }

    IEnumerator DelayStart()
        {
        this.gameObject.SetActive(false);
        Invoke("TurnOn", this.delayTime);
        yield return new WaitForSeconds(this.delayTime);
        }

    void TurnOn()
        {
        this.gameObject.SetActive(true);
        AudioManager.Instance.PlaySound(clipName, AudioType.SFX);
        this._xForm.transform.position = start.transform.position;
        }

    IEnumerator DelayInitialize()
        {
        this.gameObject.SetActive(false);
        Invoke("TurnOn", this.delayStartUp);
        yield return new WaitForSeconds(this.delayStartUp);
        }
    }