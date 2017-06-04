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

#if UNITY_STANDALONE_WIN
    bool ismouseontoggle;
#endif


    public  GameObject leftDirectionalButton;
    public  GameObject rightDirectionalButton;
    public  GameObject jumpButton;
    public  GameObject sprintButton;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

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
}