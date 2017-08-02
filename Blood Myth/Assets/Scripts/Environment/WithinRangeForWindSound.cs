using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinRangeForWindSound : MonoBehaviour
    {
    public GameObject gale;
    private MoveWindGale _script;

    void Start()
        {
        this._script = gale.GetComponent<MoveWindGale>();
        }

    void OnTriggerEnter2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this._script.SetCanHear(true);
            }
        }

    void OnTriggerExit2D(Collider2D inPlayer)
        {
        if (inPlayer.gameObject.tag == "Player")
            {
            this._script.SetCanHear(false);
            }
        }
    }