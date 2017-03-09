using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestingMeshCreation))]
public class TestingMeshCreationEditor : Editor
{
    TestingMeshCreation terrainEditor;
    string EditButtonString = "";

    Tool LastTool = Tool.None;
    
    private void OnEnable()
    {
        terrainEditor = (TestingMeshCreation)target;
         LastTool = Tools.current;
        Tools.current = Tool.None;
    }
    void OnDisable()
    {
        Tools.current = LastTool;
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

    //only runs when the referenced object is selected.
    //Locking the inspector makes it work.
    void OnSceneGUI()
    {
        terrainEditor.transform.position = Vector3.zero;

        //Handles.color = Color.magenta;

        // EditorGUI.BeginChangeCheck();
        foreach ( Vector3 vert in terrainEditor.vect)
        {
            Handles.DotCap(0, vert, Quaternion.identity, .3f);
        }
        

        //Vector3 lookTarget = PositionHandle(terrainEditor.transform.position, Quaternion.identity);

        if (terrainEditor.EditMode)
        { 
            if (Event.current.type == EventType.MouseDown)
            {
                Debug.Log(Event.current.mousePosition);
                Vector3 screenPosition = Event.current.mousePosition;
                screenPosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - screenPosition.y;
                Vector3 ray = Camera.current.ScreenToWorldPoint(screenPosition);
           //     Debug.Log(ray);
                terrainEditor.GetVertexOnClick(ray);
            }
        }
    }
}