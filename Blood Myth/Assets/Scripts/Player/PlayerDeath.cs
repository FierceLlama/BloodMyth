using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : FatigueStateBaseClass
    {
    private Player _player;

    public PlayerDeath(Player inPlayer)
        {
        this._player = inPlayer;
        }

    public override void Enter()
        {
        this._player.skeletonAnimation.state.SetAnimation(0, "Death", false);
        }

    public override void Update()
        {

        }
    }