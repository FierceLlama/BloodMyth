using UnityEngine;
using System.Collections;

public class WaterTotem : MonoBehaviour
{
    public GameObject _player;

    void OnTriggerStay2D(Collider2D player)
    {
#if UNITY_EDITOR
        if (player.gameObject.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            this._player.GetComponent<Player>().DrinkingWater();
        }
#endif
#if UNITY_ANDROID
        if (player.gameObject.tag == "Player" &&
            (this._player.GetComponent<Player>().getPrimaryTouch().CurrentScreenSection == ScreenSection.Top &&
            this._player.GetComponent<Player>().getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary)
            || (this._player.GetComponent<Player>().getSecondaryTouch().CurrentScreenSection == ScreenSection.Top &&
            this._player.GetComponent<Player>().getSecondaryTouch().getTouchPhase() == TouchPhase.Stationary))
            {
            player.GetComponent<Player>().DrinkingWater();
            }
#endif
        }
}