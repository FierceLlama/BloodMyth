using UnityEngine;
using System.Collections;

public class TemperatureHazard : MonoBehaviour
{
    public GameObject player;
    public TemperatureEffect hazardEffect;

    void OnTriggerStay2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            // Flag needed to change texture material for arms
            player.GetComponent<Player>().TemperatureHazard(hazardEffect);
        }
    }
}