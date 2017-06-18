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
            if (InputManager.instance.GetDrinkingActive())
                {
                this._playerScript.DrinkingWater();
                }
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