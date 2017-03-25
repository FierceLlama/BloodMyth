using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum DragHandleResult
{
    none = 0,

    LMBPress,
    LMBClick,
    LMBDoubleClick,
    LMBDrag,
    LMBRelease,

    RMBPress,
    RMBClick,
    RMBDoubleClick,
    RMBDrag,
    RMBRelease,
};

public class VertexHandleUtility
{
    private static List<VertexHandle> vertexList;
    public static List<VertexHandle> VertexList
    {
        get {
            if (vertexList == null) vertexList = new List<VertexHandle>();
            return vertexList;
        }
    }

    public static void AddToList(Vector3 inMeshVertex, int indx)
    {
        AddToList(new VertexHandle(inMeshVertex, indx));
    }

    public static void AddToList(VertexHandle inHandle)
    {
        vertexList.Add(inHandle);
    }

    public static void ClearList()
    {
        vertexList.Clear();
    }
}

public class VertexHandle
{
    public VertexHandle(Vector3 inVect, int inID)
    {
        meshVertex = inVect;
        controlID = inID;
    }
    Vector3 meshVertex;
    public Vector3 MeshVertex { get { return meshVertex; } set { meshVertex = value; } }

    int controlID;
    public int ControlID { get { return controlID; } }
}

/*
public class VertexHandleFactory
{
    public static VertexHandle CreateVertexHandle(Vector3 position, float handleSize, Handles.DrawCapFunction capFunc)
    {
        VertexHandle VertHand = new VertexHandle(position, handleSize, capFunc);
        return VertHand;
    }
}
*/
/*
public class VertexHandles
{
    // internal state for DragHandle()
    int s_DragHandleHash = "VertexHandle".GetHashCode();
    Vector2 s_DragHandleMouseStart;
    Vector2 s_DragHandleMouseCurrent;
    Vector3 s_DragHandleWorldStart;
    float s_DragHandleClickTime = 0;
    int s_DragHandleClickID;
    float s_DragHandleDoubleClickInterval = 0.5f;
    bool s_DragHandleHasMoved;

    // externally accessible to get the ID of the most resently processed DragHandle
    public static int lastDragHandleID;

    Vector3 position;
    float handleSize;
    Handles.DrawCapFunction capFunc;
    Color colorSelected; 

    public VertexHandles(Vector3 Inposition, float InhandleSize, Handles.DrawCapFunction IncapFunc)
    {
        int id = GUIUtility.GetControlID(s_DragHandleHash, FocusType.Passive);
        lastDragHandleID = id;

        Vector3 screenPosition = Handles.matrix.MultiplyPoint(Inposition);
        Matrix4x4 cachedMatrix = Handles.matrix;

        Handles.matrix = Matrix4x4.identity;
        IncapFunc(id, screenPosition, Quaternion.identity, InhandleSize);
        Handles.matrix = cachedMatrix;

        position = Inposition;
        handleSize = InhandleSize;
        capFunc = IncapFunc;
        colorSelected = Color.white;
    }

    public Vector3 DragHandle( out DragHandleResult result)
    {
        int id = GUIUtility.GetControlID(s_DragHandleHash, FocusType.Passive);
        lastDragHandleID = id;

        Vector3 screenPosition = Handles.matrix.MultiplyPoint(position);
        Matrix4x4 cachedMatrix = Handles.matrix;

        result = DragHandleResult.none;

        switch (Event.current.GetTypeForControl(id))
        {
            case EventType.MouseDown:
                if (HandleUtility.nearestControl == id && (Event.current.button == 0 || Event.current.button == 1))
                {
                    GUIUtility.hotControl = id;
                    s_DragHandleMouseCurrent = s_DragHandleMouseStart = Event.current.mousePosition;
                    s_DragHandleWorldStart = position;
                    s_DragHandleHasMoved = false;

                    Event.current.Use();
                    EditorGUIUtility.SetWantsMouseJumping(1);

                    if (Event.current.button == 0)
                        result = DragHandleResult.LMBPress;
                    else if (Event.current.button == 1)
                        result = DragHandleResult.RMBPress;
                }
                break;

            case EventType.MouseUp:
                if (GUIUtility.hotControl == id && (Event.current.button == 0 || Event.current.button == 1))
                {
                    GUIUtility.hotControl = 0;
                    Event.current.Use();
                    EditorGUIUtility.SetWantsMouseJumping(0);

                    if (Event.current.button == 0)
                        result = DragHandleResult.LMBRelease;
                    else if (Event.current.button == 1)
                        result = DragHandleResult.RMBRelease;

                    if (Event.current.mousePosition == s_DragHandleMouseStart)
                    {
                        bool doubleClick = (s_DragHandleClickID == id) &&
                            (Time.realtimeSinceStartup - s_DragHandleClickTime < s_DragHandleDoubleClickInterval);

                        s_DragHandleClickID = id;
                        s_DragHandleClickTime = Time.realtimeSinceStartup;

                        if (Event.current.button == 0)
                            result = doubleClick ? DragHandleResult.LMBDoubleClick : DragHandleResult.LMBClick;
                        else if (Event.current.button == 1)
                            result = doubleClick ? DragHandleResult.RMBDoubleClick : DragHandleResult.RMBClick;
                    }
                }
                break;

            case EventType.MouseDrag:
                if (GUIUtility.hotControl == id)
                {
                    s_DragHandleMouseCurrent += new Vector2(Event.current.delta.x, -Event.current.delta.y);
                    Vector3 position2 = Camera.current.WorldToScreenPoint(Handles.matrix.MultiplyPoint(s_DragHandleWorldStart))
                        + (Vector3)(s_DragHandleMouseCurrent - s_DragHandleMouseStart);
                    position = Handles.matrix.inverse.MultiplyPoint(Camera.current.ScreenToWorldPoint(position2));

                    if (Camera.current.transform.forward == Vector3.forward || Camera.current.transform.forward == -Vector3.forward)
                        position.z = s_DragHandleWorldStart.z;
                    if (Camera.current.transform.forward == Vector3.up || Camera.current.transform.forward == -Vector3.up)
                        position.y = s_DragHandleWorldStart.y;
                    if (Camera.current.transform.forward == Vector3.right || Camera.current.transform.forward == -Vector3.right)
                        position.x = s_DragHandleWorldStart.x;

                    if (Event.current.button == 0)
                        result = DragHandleResult.LMBDrag;
                    else if (Event.current.button == 1)
                        result = DragHandleResult.RMBDrag;

                    s_DragHandleHasMoved = true;

                    GUI.changed = true;
                    Event.current.Use();
                }
                break;

            case EventType.Repaint:
                Color currentColour = Handles.color;
                if (id == GUIUtility.hotControl && s_DragHandleHasMoved)
                    Handles.color = colorSelected;

                Handles.matrix = Matrix4x4.identity;
                capFunc(id, screenPosition, Quaternion.identity, handleSize);
                Handles.matrix = cachedMatrix;

                Handles.color = currentColour;
                break;

            case EventType.Layout:
                Handles.matrix = Matrix4x4.identity;
                HandleUtility.AddControl(id, HandleUtility.DistanceToCircle(screenPosition, handleSize));
                Handles.matrix = cachedMatrix;
                break;
        }

        return position;
    }
}

    */