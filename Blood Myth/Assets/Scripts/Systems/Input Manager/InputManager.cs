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
    private Touch touchinput;
    public Touch TouchInput { set { touchinput = value; } }

    public Vector2 touchpos;
    public ScreenSection CurrScrSec;

    public TouchPhase getTouchPhase () { return touchinput.phase; }
    public int getTouchTapCount() { return touchinput.tapCount; }
}

public class InputManager : MonoBehaviour
{
    private TouchInputData PrimaryTouch;
    private TouchInputData SecondaryTouch;

    private ScreenCoordinates scrCord;

    void Awake()
    {
        //This needs to go to the Game Manager.
        Screen.orientation = ScreenOrientation.LandscapeLeft;

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


        PrimaryTouch.CurrScrSec = ScreenSection.None;
        SecondaryTouch.CurrScrSec = ScreenSection.None;
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
            TouchData.CurrScrSec = ScreenSection.None;
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
            PrimaryTouch.CurrScrSec = ScreenSection.None;
            PrimaryTouch.touchpos = -Vector2.one;
            
            SecondaryTouch.CurrScrSec = ScreenSection.None;
            SecondaryTouch.touchpos = -Vector2.one;
        }

        //prytouch.touchpos = Input.mousePosition;
        //srytouch.touchpos = Input.mousePosition * -1;
        //CurrentEnabledScreenSections();
    }

    public TouchInputData GetPrimaryInputData() { return PrimaryTouch; }
    public TouchInputData GetSecondryInputData() { return SecondaryTouch; }
    public int GetInputCount () { return Input.touchCount;  }

    public void GetCurrenEnabledScreenSections(out ScreenSection inScreenSect1, out ScreenSection inScreenSect2)
    {
        inScreenSect1 = PrimaryTouch.CurrScrSec;
        inScreenSect2 = SecondaryTouch.CurrScrSec;
    }

    void CurrentEnabledScreenSections()
    {
        PrimaryTouch.CurrScrSec = ScreenSectionEnabled(PrimaryTouch.touchpos);
        SecondaryTouch.CurrScrSec = ScreenSectionEnabled(SecondaryTouch.touchpos);
    }

    private ScreenSection ScreenSectionEnabled( Vector2 inputpos)
    {
        if ( PointInTriangle2(scrCord.TopLeft, scrCord.Middle, scrCord.TopRight, inputpos))
            return ScreenSection.Top;
        else if (PointInTriangle2(scrCord.BottomLeft, scrCord.Middle, scrCord.BottomRight, inputpos))
            return ScreenSection.Bottom;
        else if (PointInTriangle2(scrCord.TopLeft, scrCord.Middle, scrCord.BottomLeft, inputpos))
            return ScreenSection.Left;
        else if (PointInTriangle2(scrCord.TopRight, scrCord.Middle, scrCord.BottomRight, inputpos))
            return ScreenSection.Right;

        return ScreenSection.None;
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