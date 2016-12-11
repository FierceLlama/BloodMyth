using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class IOData
{
    [System.NonSerialized]
    private bool _ischanged = false;
    public bool isChanged { get { return this._ischanged; } }

    private string _playername = "Tommy";
    public string PlayerName { get { return this._playername; } set { this._playername = value; _ischanged = true; } }

    /* Game Progress */
    private int _levelscompleted;
    private int LevelsCompleted { get { return this._levelscompleted; } set { this._levelscompleted = value; _ischanged = true; } }

    private int _totemscollected;
    private int TotemsCollected { get { return this._totemscollected; } set { this._totemscollected = value; _ischanged = true; } }
    
    /*System Settings*/
    private float _musiclevels;
    public float MusicLevels { get { return this._musiclevels; } set { this._musiclevels = value; _ischanged = true; } }

    private float _sfxlevels;
    public float SFXLevels { get { return this._sfxlevels; } set { this._sfxlevels = value; _ischanged = true; } }
}