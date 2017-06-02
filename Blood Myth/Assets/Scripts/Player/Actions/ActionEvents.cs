using UnityEngine;
using UnityEngine.EventSystems;

public class ActionEvents : MonoBehaviour
    {
    private SetActionIcon.IconType _type;
    private SetActionIcon _actionScript;
    void Start()
        {
        this._actionScript = GetComponent<SetActionIcon>();
        this._type = SetActionIcon.IconType.UNINITIALIZED;
        }

    public void TestingShit()
        {
        this._type = this._actionScript.GetIconType();
        switch(this._type)
            {
            case SetActionIcon.IconType.CAPE:
                Debug.Log("Cape Clicked");
                break;
            case SetActionIcon.IconType.DRINK_WATER:
                Debug.Log("Drink Water Clicked");
                break;
            case SetActionIcon.IconType.INVESTIGATE:
                Debug.Log("Investigate Clicked");
                break;
            case SetActionIcon.IconType.UNINITIALIZED:
                Debug.Log("Uninitialized Clicked");
                break;
            default:
                Debug.Log("Clicked");
                break;
            }
        }
    }