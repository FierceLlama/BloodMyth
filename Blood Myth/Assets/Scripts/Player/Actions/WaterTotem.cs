using UnityEngine;
using System.Collections;

public class WaterTotem : MonoBehaviour
{
    public GameObject player;

    void OnTriggerStay2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.W))
        {
            player.GetComponent<Player>().DrinkingWater();
        }
    }
}