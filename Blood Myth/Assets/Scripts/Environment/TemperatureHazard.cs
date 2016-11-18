using UnityEngine;
using System.Collections;

public class TemperatureHazard : MonoBehaviour
{
    public GameObject player;
    public TemperatureEffect hazardEffect;

    void OnTriggerEnter2D(Collider2D inPlayer)
    {
        if (inPlayer.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().TemperatureHazard(hazardEffect);
        }
    }
}