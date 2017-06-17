using UnityEngine;
using System.Collections;

public class WaterTotem : MonoBehaviour
    {
    public GameObject _player;
    private Player _playerScript;
    private SetActionIcon _action;

    void Start()
        {
        this._playerScript = this._player.GetComponent<Player>();
        this._action = GameObject.FindWithTag("Actions").GetComponent<SetActionIcon>();
        }

    void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this._action.DisplayIcon(SetActionIcon.IconType.DRINK_WATER);
            }
        }

    void OnTriggerStay2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.W))
                {
                this._playerScript.DrinkingWater();
                }
#endif

#if UNITY_ANDROID
            if ((this._player.GetComponent<PlayerManager>().getPrimaryTouch().CurrentScreenSection == ScreenSection.Top &&
                this._player.GetComponent<PlayerManager>().getPrimaryTouch().getTouchPhase() == TouchPhase.Stationary)
                || (this._player.GetComponent<PlayerManager>().getSecondaryTouch().CurrentScreenSection == ScreenSection.Top &&
                this._player.GetComponent<PlayerManager>().getSecondaryTouch().getTouchPhase() == TouchPhase.Stationary))
                {
                this._playerScript.DrinkingWater();
                }
#endif
            }
        }

    void OnTriggerExit2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this._action.HideIcon();
            }
        }
    }