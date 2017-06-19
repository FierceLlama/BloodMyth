using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum WorldTotems
    {
    NO_TOTEMS = 0x0,
    // Spring Totems
    SPRING_TOTEMS_1 = 0x00000001,
    SPRING_TOTEMS_2 = 0x00000002,
    // Summer Totems
    SUMMER_TOTEMS_1 = 0x00000010,
    SUMMER_TOTEMS_2 = 0x00000020,
    SUMMER_TOTEMS_3 = 0x00000040,
    // Fall Totems
    FALL_TOTEMS_1   = 0x00000100,
    FALL_TOTEMS_2   = 0x00000200,
    FALL_TOTEMS_3   = 0x00000400,
    FALL_TOTEMS_4   = 0x00000800,
    // Winter Totems
    WINTER_TOTEMS_1 = 0x00001000,
    WINTER_TOTEMS_2 = 0x00002000,
    WINTER_TOTEMS_3 = 0x00004000,
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