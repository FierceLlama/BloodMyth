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
    void OnSceneGUI()
    {
        foreach (Vector3 vert in terrainEditor.transVerts)
        {
            Handles.DotCap(0, vert, Quaternion.identity, .3f);
        }

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
            if (Event.current.type == EventType.MouseUp)
                UpdateVertsTransfrom();
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