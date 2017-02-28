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

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
       
        if (GUILayout.Button("Clear Terrain"))
            terrainEditor.ClearTerrain();
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