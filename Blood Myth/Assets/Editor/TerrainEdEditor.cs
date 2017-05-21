using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TerrainEd)), CanEditMultipleObjects]
public class TerrainEdEditor : Editor
{
    TerrainEd terrainEditor;
    private static Vector3 pointSnap = Vector3.one * 0.1f;

    private void OnEnable()
    {
        terrainEditor = (TerrainEd)target;
        VertexHandleUtility.Initialize();
    }
    void OnDisable() { }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        switch ( terrainEditor.Mode)
        {
            case TerrainEditorMode.VERTEXEDIT:

                if (GUILayout.Button("Set Transform Mode On"))
                {
                    terrainEditor.Mode = TerrainEditorMode.TRANFORMEDIT;
                    Tools.current = Tool.Move;
                }
                if (GUILayout.Button("Turn on Add Vertex Mode"))
                {
                    terrainEditor.Mode = TerrainEditorMode.ADDVERTEX;
                    Tools.current = Tool.Move;
                }

                break;

            case TerrainEditorMode.ADDVERTEX:

                if (GUILayout.Button("Set Transform Mode On"))
                {
                    terrainEditor.Mode = TerrainEditorMode.TRANFORMEDIT;
                    Tools.current = Tool.Move;
                }
                if (GUILayout.Button("Turn off Add Vertex Mode"))
                {
                    terrainEditor.Mode = TerrainEditorMode.VERTEXEDIT;
                    Tools.current = Tool.Move;
                }

                break;

            case TerrainEditorMode.TRANFORMEDIT:

                if (GUILayout.Button("Set Transform Mode Off"))
                {
                    terrainEditor.Mode = TerrainEditorMode.VERTEXEDIT;
                    Tools.current = Tool.Move;
                }
                if (GUILayout.Button("Turn on Add Vertex Mode"))
                {
                    terrainEditor.Mode = TerrainEditorMode.ADDVERTEX;
                    Tools.current = Tool.Move;
                }

                break;
        }

        if (GUILayout.Button("Clear Terrain"))
        {
            terrainEditor.ClearMesh();
            VertexHandleUtility.ClearList();
        }
    }

    void OnSceneGUI()
    {
        switch ( terrainEditor.Mode)
        {
            case TerrainEditorMode.VERTEXEDIT:
                ExecuteNonEditMode();
            break;

            case TerrainEditorMode.ADDVERTEX:
                ExecuteEditMode();
            break;

            case TerrainEditorMode.TRANFORMEDIT:
                ExecuteTransformMode();
            break;
        }
    }

    void ExecuteEditMode()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            Vector3 screenPosition = Event.current.mousePosition;
            screenPosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - screenPosition.y;

            Vector3 vect = Camera.current.ScreenToWorldPoint(screenPosition);

            terrainEditor.GetVertexOnClick(vect); 
        }

        foreach (VertexHandle VertHand in VertexHandleUtility.VertexList)
        {
            Vector3 oldPoint = terrainEditor.transform.TransformPoint(Quaternion.identity * VertHand.MeshVertex),
                    newPoint = Handles.FreeMoveHandle(oldPoint, Quaternion.identity, .3f, pointSnap, Handles.DotCap);
        }
    }

    void ExecuteNonEditMode()
    {
        foreach (VertexHandle VertHand in VertexHandleUtility.VertexList)
        {
            Vector3 oldPoint = terrainEditor.transform.TransformPoint(Quaternion.identity * VertHand.MeshVertex),
                    newPoint = Handles.FreeMoveHandle(oldPoint, Quaternion.identity, .3f, pointSnap, Handles.DotCap);

            if (newPoint != VertHand.MeshVertex)
            { 
                VertHand.MeshVertex = Quaternion.Inverse(Quaternion.identity) *
                    terrainEditor.transform.InverseTransformPoint(newPoint);

                terrainEditor.UpdateMeshVertices(VertHand.ControlID);
            }
        }
    }

    void ExecuteTransformMode()
    {
        foreach (VertexHandle VertHand in VertexHandleUtility.VertexList)
        {
           Handles.FreeMoveHandle(VertHand.TransMeshVertex, Quaternion.identity, .3f, pointSnap, Handles.DotCap);
        }
    }
        
}

