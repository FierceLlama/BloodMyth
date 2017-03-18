using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestingMeshCreation))]
public class TestingMeshCreationEditor : Editor
{
    TestingMeshCreation terrainEditor;
    
    private void OnEnable()
    {
        terrainEditor = (TestingMeshCreation)target;
    }
    void OnDisable(){}

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
       
        if (GUILayout.Button("Clear Terrain"))
            terrainEditor.ClearMesh();

        if (!terrainEditor.EditMode)
        { 
            if (GUILayout.Button("Turn on Edit Mode"))
            {
                terrainEditor.EditMode = true;
                Tools.current = Tool.None;
            }
        }
        else
        if(GUILayout.Button("Turn off Edit Mode"))
        {
            terrainEditor.EditMode = false;
            Tools.current = Tool.Move;
        }
    }

    //only runs when the referenced object is selected.
    //Locking the inspector makes it work.
    ///////////////////////////////////////////////////////////
    //create a list of handles,
    //Add an index to each handle,
    //To "Drag" loop through the Handles instead of the vertices
    void OnSceneGUI()
    {
        VertexHandle.DragHandleResult dhResult;

        foreach (Vector3 vert in terrainEditor.transVerts)
            VertexHandle.CreateHandle(vert, .3f, Handles.DotCap);

        if (terrainEditor.EditMode)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                Vector3 screenPosition = Event.current.mousePosition;
                screenPosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - screenPosition.y;
                Vector3 vect = Camera.current.ScreenToWorldPoint(screenPosition);
                terrainEditor.GetVertexOnClick(vect);
                UpdateVertsTransfrom();
            }
        }

        if (terrainEditor.EditMode == false && terrainEditor.Generated == true)
        {
            Vector3[] vert = terrainEditor.transVerts;
            for (int i = 0; i < vert.Length; i++)
            {
                Vector3 newPosition = VertexHandle.DragHandle(vert[i], .3f, Handles.SphereCap, Color.red, out dhResult);

                switch (dhResult)
                {
                    case VertexHandle.DragHandleResult.LMBRelease:
                        vert[i] = newPosition;
                        UpdateVertsTransfrom();
                        break;
                }
            }

            if (Event.current.type == EventType.MouseUp)
                UpdateVertsTransfrom();
        }
    }

    void UpdateVertsTransfrom ()
    {
        for (int i = 0; i < terrainEditor.transVerts.Length; ++i)
        {
            terrainEditor.transVerts[i] = terrainEditor.OrigVerts[i]
                + terrainEditor.transform.position;
        }
    }
}

public class VertexHandle
{
    // internal state for DragHandle()
    static int s_DragHandleHash = "VertexHandle".GetHashCode();
    static Vector2 s_DragHandleMouseStart;
    static Vector2 s_DragHandleMouseCurrent;
    static Vector3 s_DragHandleWorldStart;
    static float s_DragHandleClickTime = 0;
    static int s_DragHandleClickID;
    static float s_DragHandleDoubleClickInterval = 0.5f;
    static bool s_DragHandleHasMoved;

    // externally accessible to get the ID of the most resently processed DragHandle
    public static int lastDragHandleID;

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

    public static void CreateHandle(Vector3 position, float handleSize, Handles.DrawCapFunction capFunc)
    {
        int id = GUIUtility.GetControlID(s_DragHandleHash, FocusType.Passive);
        lastDragHandleID = id;

        Vector3 screenPosition = Handles.matrix.MultiplyPoint(position);
        Matrix4x4 cachedMatrix = Handles.matrix;

        Handles.matrix = Matrix4x4.identity;
        capFunc(id, screenPosition, Quaternion.identity, handleSize);
        Handles.matrix = cachedMatrix;
    }

    public static Vector3 DragHandle(Vector3 position, float handleSize, Handles.DrawCapFunction capFunc, Color colorSelected, out DragHandleResult result)
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
