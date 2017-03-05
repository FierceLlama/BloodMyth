using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestingMeshCreation))]
public class TestingMeshCreationEditor : Editor
{
    TestingMeshCreation terrainEditor;
    string EditButtonString = "";

    private void OnEnable()
    {
        terrainEditor = (TestingMeshCreation)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
       
        if (GUILayout.Button("Clear Terrain"))
            terrainEditor.ClearMesh();

        if (!terrainEditor.EditMode)
        { 
            if (GUILayout.Button("Turn on Edit Mode"))
                terrainEditor.EditMode = true;    
        }
        else
        if(GUILayout.Button("Turn off Edit Mode"))
                terrainEditor.EditMode = false;

    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmoForTerrainEditor(TerrainEditor src, GizmoType gizmoType)
    {
        Gizmos.DrawSphere(Vector3.zero, 10);
    }

    //only runs when the referenced object is selected.
    //Locking the inspector makes it work.
    void OnSceneGUI()
    {
        Debug.Log("OnScene");
        if (Event.current.type == EventType.MouseDown)
        {
            Vector3 screenPosition = Event.current.mousePosition;
            screenPosition.y = Camera.current.pixelHeight - screenPosition.y;
            Ray ray = Camera.current.ScreenPointToRay(screenPosition);
            
            terrainEditor.GetVertexOnClick(ray.origin);
        }
    }
}