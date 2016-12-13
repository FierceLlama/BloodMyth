using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemCollected : MonoBehaviour
{
    private WorldTotems _worldTotem;

    void Start()
    {
        this._worldTotem = this.gameObject.GetComponent<TotemState>().GetTotemEnum();
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            IOSystem.Instance.data.TotemCollected(this._worldTotem);
            IOSystem.Instance.AutoSave();
        }
    }
}