using UnityEngine;
using System.Collections;

public struct TouchInputData
{
    //input manager data
    private Touch touchinput;
    public Touch TouchInput { set { touchinput = value; } }

    public Vector2 touchpos;

    //isHolding
    public bool isHolding { get { return touchinput.phase == TouchPhase.Stationary ? true : false;  } }
    public TouchPhase getTouchPhase () { return touchinput.phase;  }

    //Tap counts
    private int TapCount;
    public int getTouchTapCount() { return TapCount; }
}

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public bool toggleStatus;

    [SerializeField]
    bool isJumpActive;
    [SerializeField]
    bool isSprintActive;
    [SerializeField]
    bool isLeftActive;
    [SerializeField]
    bool isRightActive;

    bool drinkWaterActive;
    bool investigateActive;
    bool capeActive;

    private SetActionIcon.IconType _type;
    private SetActionIcon _actionScript;

    private void Awake()
        {
        if (instance == null)
            {
            instance = this;
            }
        }

    void Start()
        {
        this._actionScript = GameObject.FindWithTag("Actions").GetComponent<SetActionIcon>();
        this._type = SetActionIcon.IconType.UNINITIALIZED;

        //this.DeactivateAll();
        //this.DeactivateAllInput();
        }

    public void ActivateLeftButton()
    {
        if (!isLeftActive) isLeftActive = true;

        if (isLeftActive && isRightActive) { isLeftActive = false; isRightActive = false; }

        //Debug.Log("Left is " + (isLeftActive ? "Active" : "Not Active"));
    }
    public void DeActivateLeftButton()
    {
        if (isLeftActive)
        { 
            isLeftActive = false;
            if (toggleStatus) toggleStatus = false;
        }

        //Debug.Log("Left is " + (isLeftActive ? "Active" : "Not Active"));
    }

    public void ActivateRightButton()
    {
        if (!isRightActive) isRightActive = true;

        if (isLeftActive && isRightActive) { isLeftActive = false; isRightActive = false; }

        //Debug.Log("Right is " + (isRightActive ? "Active" : "Not Active"));
    }
    public void DeActivateRightButton()
    {
        if (isRightActive)
        {
            isRightActive = false;
            if (toggleStatus) toggleStatus = false;
        }

        //Debug.Log("Right is " + (isRightActive ? "Active" : "Not Active"));
    }

    public void ActivateJumpButton()
    {
        if (!isJumpActive) isJumpActive = true;

        //Debug.Log("Jump is " + (isJumpActive ? "Active" : "Not Active"));
    }
    public void DeActivateJumpButton()
    {
        if (isJumpActive) isJumpActive = false;

        //Debug.Log("Jump is " + (isJumpActive ? "Active" : "Not Active"));
    }

    public void triggerSprintButton()
    {
        if (isSprintActive)
            isSprintActive = false;
        else if (!isSprintActive && (isLeftActive || isRightActive) )
            isSprintActive = true;

        if (isLeftActive && isRightActive)
            isSprintActive = false;

        //Debug.Log("Sprint is " + (isSprintActive ? "Active" : "Not Active"));
    }

    void DeactivateAllInput()
        {
        this.DeActivateJumpButton();
        this.DeActivateRightButton();
        this.DeActivateRightButton();
        this.triggerSprintButton();
        this.isLeftActive = false;
        this.isRightActive = false;
        this.isJumpActive = false;
        isSprintActive = false;
        }

    public void ActivateActionButton()
        {
        this._type = this._actionScript.GetIconType();
        switch (this._type)
            {
            case SetActionIcon.IconType.CAPE:
                Debug.Log("Cape Clicked");
                this.ActivateCape();
                break;
            case SetActionIcon.IconType.DRINK_WATER:
                Debug.Log("Drink Water Clicked");
                this.ActivateDrinkWater();
                break;
            case SetActionIcon.IconType.INVESTIGATE:
                Debug.Log("Investigate Clicked");
                this.ActivateInvestigate();
                break;
            case SetActionIcon.IconType.UNINITIALIZED:
                Debug.Log("Uninitialized Clicked");
                break;
            default:
                Debug.Log("Clicked");
                break;
            }
        }

    public void DeactivateActionButton()
        {
        this._type = this._actionScript.GetIconType();
        switch (this._type)
            {
            case SetActionIcon.IconType.CAPE:
                this.DeactivateCape();
                break;
            case SetActionIcon.IconType.DRINK_WATER:
                this.DeactivateDrinkWater();
                break;
            case SetActionIcon.IconType.INVESTIGATE:
                this.DeactivateInvestigate();
                break;
            case SetActionIcon.IconType.UNINITIALIZED:
                this.DeactivateAll();
                break;
            default:
                break;
            }
        }

    void ActivateCape()
        {
        if (!capeActive) capeActive = true;
        }

    void DeactivateCape()
        {
        if (capeActive) capeActive = false;
        }

    void ActivateDrinkWater()
        {
        if (!drinkWaterActive) drinkWaterActive = true;
        }

    void DeactivateDrinkWater()
        {
        if (drinkWaterActive) drinkWaterActive = false;
        }

    void ActivateInvestigate()
        {
        if (!investigateActive) investigateActive = true;
        }

    void DeactivateInvestigate()
        {
        if (investigateActive) investigateActive = false;
        }

    void DeactivateAll()
        {
        this.DeactivateCape();
        this.DeactivateDrinkWater();
        this.DeactivateInvestigate();
        this.capeActive = false;
        this.drinkWaterActive = false;
        this.investigateActive = false;
        }

    public void GetButtonsStatus(ref bool Left, ref bool Right, ref bool Jump)
    {
        Left = isLeftActive;
        Right = isRightActive;
        Jump = isJumpActive;

        toggleStatus = isSprintActive;
    }

#if UNITY_STANDALONE_WIN
    bool ismouseontoggle;
#endif

    public bool GetLeftActive()
        {
        return this.isLeftActive;
        }

    public bool GetRightActive()
        {
        return this.isRightActive;
        }

    public bool GetJumpActive()
        {
        return this.isJumpActive;
        }

    public bool GetSprintActive()
        {
        return this.isSprintActive;
        }

    public bool GetCapeActive()
        {
        return this.capeActive;
        }

    public bool GetDrinkingActive()
        {
        return this.drinkWaterActive;
        }

    public bool GetInvestigateActive()
        {
        return this.investigateActive;
        }

    /*
    bool InputHoldSprite(GameObject inObject)
    {

#if UNITY_STANDALONE_WIN
        Vector2 MouseInputVector = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if (inObject.GetComponent<Collider2D>().OverlapPoint(MouseInputVector))
            return true;
#endif

#if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Vector2 TouchInputVector = new Vector2(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y);
            if (inObject.GetComponent<Collider2D>().OverlapPoint(TouchInputVector))
                return true;
         }
#endif

        return false;
    }

    public void GetButtonsStatus(ref bool Left, ref bool Right, ref bool Jump)
    {
        Left    = InputHoldSprite(leftDirectionalButton);
        Right   = InputHoldSprite(rightDirectionalButton);
        Jump    = InputHoldSprite(jumpButton);
        
        toggleStatus = GetHitSprite(sprintButton);

#if UNITY_ANDROID
        if (!Left && !Right) toggleStatus = false;
#endif
    }

    bool GetHitSprite(GameObject inObject)
    {
        
#if UNITY_STANDALONE_WIN
        if (!ismouseontoggle)
        { 
            Vector2 MouseInputVector = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            if (inObject.GetComponent<Collider2D>().OverlapPoint(MouseInputVector))
            {
                ismouseontoggle = true;
                return !toggleStatus ? true : false;   
            }
        }
        else
        {
            ismouseontoggle = InputHoldSprite(sprintButton);
        }

#endif

#if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && Input.GetTouch(i).phase != TouchPhase.Stationary)
            {
                Vector2 TouchInputVector = new Vector2(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y);
                if (inObject.GetComponent<Collider2D>().OverlapPoint(TouchInputVector))
                {   return true; }
            }
        }

#endif

        return toggleStatus;
    }
    
    public int GetInputCount () { return Input.touchCount;  }
    */
}