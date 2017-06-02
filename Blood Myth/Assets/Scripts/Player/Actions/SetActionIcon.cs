using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActionIcon : MonoBehaviour
    {
    public enum IconType
        {
        CAPE,
        DRINK_WATER,
        INVESTIGATE,
        UNINITIALIZED
        }

    private IconType _type;
    private UnityEngine.UI.Image _image;
    public Sprite capeIcon;
    public Sprite drinkingIcon;
    public Sprite investigateIcon;

    void Start()
        {
        this._type = IconType.UNINITIALIZED;
        this._image = this.GetComponent<UnityEngine.UI.Image>();
        this.DisplayIcon(IconType.CAPE);
        }

    public void DisplayIcon(IconType inType)
        {
        switch (inType)
            {
            case IconType.CAPE:
                this._image.sprite = this.capeIcon;
                break;
            case IconType.DRINK_WATER:
                this._image.sprite = this.drinkingIcon;
                break;
            case IconType.INVESTIGATE:
                this._image.sprite = this.investigateIcon;
                break;
            }
        this._image.enabled = true;
        }

    public void HideIcon()
        {
        this._image.enabled = false;
        }
    }
