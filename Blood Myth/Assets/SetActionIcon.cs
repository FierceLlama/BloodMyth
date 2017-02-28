using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActionIcon : MonoBehaviour
    {
    public enum IconType
        {
        CLIMB,
        DRINK_WATER,
        UNINITIALIZED
        }

    private IconType _type;
    private UnityEngine.UI.Image _image;
    public Sprite climbingIcon;
    public Sprite drinkingIcon;

    void Start()
        {
        this._type = IconType.UNINITIALIZED;
        this._image = this.GetComponent<UnityEngine.UI.Image>();
        this.HideIcon();
        }

    public void DisplayIcon(IconType inType)
        {
        switch (inType)
            {
            case IconType.CLIMB:
                this._image.sprite = this.climbingIcon;
                break;
            case IconType.DRINK_WATER:
                this._image.sprite = this.drinkingIcon;
                break;
            }
        this._image.enabled = true;
        }

    public void HideIcon()
        {
        this._image.enabled = false;
        }
    }
