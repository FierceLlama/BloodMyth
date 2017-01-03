using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum WorldTotems
{
    NO_TOTEMS       = 0x00000001,
    // Fall Totems -- values will change based on spring and summer totems
    FALL_TOTEMS_1   = 0x00000002,
    FALL_TOTEMS_2   = 0x00000004
};

[Serializable]
public class IOData
{
    [System.NonSerialized]
    private bool _ischanged = false;
    public bool isChanged
    {
        get
        {
            return this._ischanged;
        }
        set
        {
            this._ischanged = value;
        }
    }

    private string _playername = "Tommy";
    public string PlayerName
    {
        get
        {
            return this._playername;
        }
        set
        {
            this._playername = value;
            _ischanged = true;
        }
    }

    /* Game Progress */
    private int _levelscompleted;
    public int LevelsCompleted
    {
        get
        {
            return this._levelscompleted;
        }
        set
        {
            this._levelscompleted = value;
            _ischanged = true;
        }
    }

    private int _totemscollected;
    public int TotemsCollected
    {
        get
        {
            return this._totemscollected;
        }
        set
        {
            this._totemscollected = value;
            _ischanged = true;
        }
    }

    private int _worldTotems = (int)WorldTotems.NO_TOTEMS;
    public int worldTotems
        {
        get
        {
        return this._worldTotems;
        }
        set
        {
            this._worldTotems = value;
            this._ischanged = true;
        }
        }

    /*System Settings*/
    private float _musiclevels;
    public float MusicLevels
    {
        get
        {
            return this._musiclevels;
        }
        set
        {
            this._musiclevels = value;
            _ischanged = true;
        }
    }

    private float _sfxlevels;
    public float SFXLevels
    {
        get
        {
            return this._sfxlevels;
        }
        set
        {
            this._sfxlevels = value;
            _ischanged = true;
        }
    }

    public void TotemCollected(WorldTotems inTotem)
    {
        this.worldTotems = this._worldTotems | (int)inTotem;
    }
}