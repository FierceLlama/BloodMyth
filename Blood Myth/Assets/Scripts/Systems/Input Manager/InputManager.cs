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
public struct TouchInputWrapper
{
    public Touch touchinput;
    public Vector2 touchpos;
    public ScreenSection CurrScrSec;
}
public class InputManager : MonoBehaviour
{
    private TouchInputWrapper prytouch;
    private TouchInputWrapper srytouch;

    private ScreenCoordinates scrCord;

    void Awake()
    {
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
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
           
            prytouch.touchinput = Input.GetTouch(0);
     

            if (prytouch.touchinput.phase == TouchPhase.Stationary)
               prytouch.touchpos = prytouch.touchinput.position;
            else if (prytouch.touchinput.phase == TouchPhase.Ended)
            {
                prytouch.CurrScrSec = ScreenSection.None;
                prytouch.touchpos = -Vector2.one;
            }

            if (Input.touchCount > 1)
            { 
                srytouch.touchinput = Input.GetTouch(1);
                if (srytouch.touchinput.phase == TouchPhase.Stationary)
                    srytouch.touchpos = srytouch.touchinput.position;
                else if (srytouch.touchinput.phase == TouchPhase.Ended)
                {
                    srytouch.CurrScrSec = ScreenSection.None;
                    srytouch.touchpos = -Vector2.one;
                }
            }
            CurrenEnabledScreenSections();
        }
        else
        {
            prytouch.touchpos = -Vector2.one;
            srytouch.touchpos = -Vector2.one;
            prytouch.CurrScrSec = ScreenSection.None;
            srytouch.CurrScrSec = ScreenSection.None;
        }

     //prytouch.touchpos = Input.mousePosition;
        //srytouch.touchpos = Input.mousePosition * -1;
        //CurrenEnabledScreenSections();
    }

    public void GetCurrenEnabledScreenSections(out ScreenSection inScreenSect1, out ScreenSection inScreenSect2)
    {
        inScreenSect1 = prytouch.CurrScrSec;
        inScreenSect2 = srytouch.CurrScrSec;
    }

    void CurrenEnabledScreenSections()
    {
        prytouch.CurrScrSec = ScreenSectionEnabled(prytouch.touchpos);
        srytouch.CurrScrSec = ScreenSectionEnabled(srytouch.touchpos);
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
