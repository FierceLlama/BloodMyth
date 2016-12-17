using UnityEngine;
using System.Collections;

public enum ScreenSection
{
    Top,
    Bottom,
    Left,
    Right,
    None
}

public struct ScreenCoordinates
{
    public Vector2 TopLeft, BottomLeft, TopRight, BottomRight, Middle;
};

public struct TouchInputData
{
    //input manager data
    private Touch touchinput;
    public Touch TouchInput { set { touchinput = value; } }

    public Vector2 touchpos;

    //
    private ScreenSection CurrScrSec;
    private ScreenSection tempScrSec;
    public ScreenSection CurrentScreenSection
    {
        get { return CurrScrSec; }
        set
        {
            CurrScrSec = value;

            if (tempScrSec != CurrScrSec && tempScrSec != ScreenSection.None)
                TapCount = 0;
            else
                TapCount = touchinput.tapCount;

            tempScrSec = CurrScrSec;
        }
    }

    //isHolding
    public bool isHolding { get { return touchinput.phase == TouchPhase.Stationary ? true : false;  } }
    public TouchPhase getTouchPhase () { return touchinput.phase;  }

    //Tap counts
    private int TapCount;
    public int getTouchTapCount() { return TapCount; }
}

public class InputManager : MonoBehaviour
{
    private TouchInputData PrimaryTouch;
    private TouchInputData SecondaryTouch;

    private ScreenCoordinates scrCord;

    void Awake()
    {
        scrCord.TopLeft.x = 0;
        scrCord.TopLeft.y = Screen.height;

        scrCord.BottomLeft.x = 0;
        scrCord.BottomLeft.y = 0;

        scrCord.BottomRight.x = Screen.width;
        scrCord.BottomRight.y = 0;

        scrCord.TopRight.x = Screen.width;
        scrCord.TopRight.y = Screen.height;

        scrCord.Middle.x = Screen.width / 2;
        scrCord.Middle.y = Screen.height / 2;


        PrimaryTouch.CurrentScreenSection = ScreenSection.None;
        SecondaryTouch.CurrentScreenSection = ScreenSection.None;
    }

    void UpdateTouchData(ref TouchInputData TouchData, int i)
    {
        TouchData.TouchInput = Input.GetTouch(i);

        if (TouchData.getTouchPhase() == TouchPhase.Stationary)
        {
            TouchData.touchpos = Input.GetTouch(i).position;
        }
        else if (TouchData.getTouchPhase() == TouchPhase.Ended)
        {
            TouchData.CurrentScreenSection = ScreenSection.None;
            TouchData.touchpos = -Vector2.one;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            UpdateTouchData(ref PrimaryTouch, 0);
           
            if (Input.touchCount > 1)
            {
                UpdateTouchData(ref SecondaryTouch, 1);
            }

            CurrentEnabledScreenSections();
        }
        else
        {
            PrimaryTouch.CurrentScreenSection = ScreenSection.None;
            PrimaryTouch.touchpos = -Vector2.one;
            
            SecondaryTouch.CurrentScreenSection = ScreenSection.None;
            SecondaryTouch.touchpos = -Vector2.one;
        }

        //PrimaryTouch.touchpos = Input.mousePosition;
        //srytouch.touchpos = Input.mousePosition * -1;
       // CurrentEnabledScreenSections();
    }

    public TouchInputData GetPrimaryInputData() { return PrimaryTouch; }
    public TouchInputData GetSecondryInputData() { return SecondaryTouch; }
    public int GetInputCount () { return Input.touchCount;  }

    public void GetCurrenEnabledScreenSections(out ScreenSection inScreenSect1, out ScreenSection inScreenSect2)
    {
        inScreenSect1 = PrimaryTouch.CurrentScreenSection;
        inScreenSect2 = SecondaryTouch.CurrentScreenSection;
    }

    void CurrentEnabledScreenSections()
    {
        PrimaryTouch.CurrentScreenSection = ScreenSectionEnabled(PrimaryTouch.touchpos);
        SecondaryTouch.CurrentScreenSection = ScreenSectionEnabled(SecondaryTouch.touchpos);
    }

    private ScreenSection ScreenSectionEnabled( Vector2 inputpos)
    {
        ScreenSection section = ScreenSection.None;

        if (PointInTriangle2(scrCord.TopLeft, scrCord.Middle, scrCord.TopRight, inputpos))
        {
            section = ScreenSection.Top;
        }
        else if (PointInTriangle2(scrCord.BottomLeft, scrCord.Middle, scrCord.BottomRight, inputpos))
        {
            section = ScreenSection.Bottom;
        }
        else if (PointInTriangle2(scrCord.TopLeft, scrCord.Middle, scrCord.BottomLeft, inputpos))
        {
            section = ScreenSection.Left;
        }
        else if (PointInTriangle2(scrCord.TopRight, scrCord.Middle, scrCord.BottomRight, inputpos))
        {
            section = ScreenSection.Right;
        }

        return section;
    }

    float sign(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    bool PointInTriangle2(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 pt)
    {
        bool b1, b2, b3;

        b1 = sign(pt, v1, v2) < 0.0f;
        b2 = sign(pt, v2, v3) < 0.0f;
        b3 = sign(pt, v3, v1) < 0.0f;

        return ((b1 == b2) && (b2 == b3));
    }
}