using UnityEngine;
using System.Collections;

public class WaterTotem : MonoBehaviour
    {
    public GameObject _player;

    void OnTriggerStay2D(Collider2D inPlayer)
        {
        //GameObject.FindWithTag("Actions").GetComponent<SetActionIcon>().DisplayIcon(SetActionIcon.IconType.DRINK_WATER);
#if UNITY_EDITOR
        if (inPlayer.gameObject.tag == "Player" && Input.GetKey(KeyCode.W))
            {
            //this._player.GetComponent<Player>().DrinkingWater();
            }
#endif
#if UNITY_ANDROID
        if (inPlayer.gameObject.tag == "Player" &&
            (this._player.GetComponent<PlayerManager>().getPrimaryTouch().CurrentScreenSection == ScreenSection.Top &&
            this._player.GetComponent<PlayerManager>().getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary)
            || (this._player.GetComponent<PlayerManager>().getSecondaryTouch().CurrentScreenSection == ScreenSection.Top &&
            this._player.GetComponent<PlayerManager>().getSecondaryTouch().getTouchPhase() == TouchPhase.Stationary))
            {
            inPlayer.GetComponent<Player>().DrinkingWater();
            }
#endif
        }

    void OnTriggerExit2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            //GameObject.FindWithTag("Actions").GetComponent<SetActionIcon>().HideIcon();
            }
        }
    }