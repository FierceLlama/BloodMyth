using UnityEngine;
using System.Collections;

public class TotemPower : MonoBehaviour
{
    public ParticleSystem totemHalo;
    private Player _player;

    void Start()
    {
        // Might need to figure out actual particle location in z space to determine sorting layer
        //      For now I am ensuring it is in front of player
        this.totemHalo.GetComponent<Renderer>().sortingLayerName = "Foreground";
        this._player = this.gameObject.GetComponent<Player>();
    }

    void Update()
    {
        // This is a terrible unoptimized wa to do this.
        // TODO -- Fix if else crap
        // Active totem power
        if (this._player.getTotemPowers() > 0)
        {
            this.totemHalo.Play();
        }
        else
        {
            this.totemHalo.Stop();
        }
    }
}