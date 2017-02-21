using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TerrainEditor))]
public class TerrainEditorEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TerrainEditor terrainEditor = (TerrainEditor)target;

        if (GUILayout.Button("Generate New Terrain"))
            terrainEditor.GenerateTerrain();

        if (GUILayout.Button("Update Terrain"))
            terrainEditor.UpdateTerrain();

        if (GUILayout.Button("Clear Terrain"))
            terrainEditor.ClearTerrain();
    }
}
