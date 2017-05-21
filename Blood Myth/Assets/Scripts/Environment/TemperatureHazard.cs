using UnityEngine;
using System.Collections;

public enum TemperatureEffect
    {
    HOT_HAZARD,
    COLD_HAZARD
    }

public class TemperatureHazard : MonoBehaviour
{
    public GameObject player;
    public TemperatureEffect hazardEffect;
    public float timeDelay = 2.0f;
    private float _currentTime = 0.0f;

    void OnTriggerEnter2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().TemperatureHazard(hazardEffect);
        }
    }

    void OnTriggerStay2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            this._currentTime += Time.deltaTime;
            if (this._currentTime >= this.timeDelay)
            {
                player.GetComponent<Player>().TemperatureHazard(hazardEffect);
                this._currentTime = 0.0f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            this._currentTime = 0.0f;
        }
    }
}