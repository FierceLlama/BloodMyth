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
    }
    void OnDisable() { }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Clear Terrain"))
        {
            terrainEditor.ClearMesh();
            VertexHandleUtility.ClearList();
        }

        if (!terrainEditor.EditMode)
        {
            if (GUILayout.Button("Turn on Edit Mode"))
            {
                terrainEditor.EditMode = true;
                Tools.current = Tool.None;
            }
        }
        else
        if (GUILayout.Button("Turn off Edit Mode"))
        {
            terrainEditor.EditMode = false;
            Tools.current = Tool.Move;
        }
    }

    void OnSceneGUI()
    {
        if (terrainEditor.EditMode)
            ExecuteEditMode();
        else
            ExecuteNonEditMode();
    }

    void ExecuteEditMode()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            Vector3 screenPosition = Event.current.mousePosition;
            screenPosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - screenPosition.y;

            Vector3 vect = Camera.current.ScreenToWorldPoint(screenPosition);

            VertexHandleUtility.AddToList(terrainEditor.GetVertexOnClick(vect),
                VertexHandleUtility.VertexList.Count);
        }

        foreach (VertexHandle VertHand in VertexHandleUtility.VertexList)
        {
            Handles.FreeMoveHandle(VertHand.MeshVertex, Quaternion.identity, .3f, pointSnap, Handles.DotCap);
        }
    }

    void ExecuteNonEditMode()
    {
        foreach (VertexHandle VertHand in VertexHandleUtility.VertexList)
        {
            Vector3 NewPos = VertHand.MeshVertex;

            NewPos = Handles.FreeMoveHandle(VertHand.MeshVertex, Quaternion.identity, .3f, pointSnap, Handles.DotCap);

            if (NewPos != VertHand.MeshVertex)
            {
                VertHand.MeshVertex = NewPos;
                UpdateVertsTransfrom(VertHand.ControlID);
            }
        }
        
    }

    void UpdateVertsTransfrom(int indx)
    {
        terrainEditor.UpdateMeshVertices(indx);
    }
    /*
        void UpdateVertsTransfrom()
        {
            for (int i = 0; i < terrainEditor.transVerts.Length; ++i)
            {
                terrainEditor.transVerts[i] = terrainEditor.OrigVerts[i]
                    + terrainEditor.transform.position;
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
            DragHandleResult dhResult;

            foreach (Vector3 vert in terrainEditor.transVerts)
                HandlesList.Add(VertexHandleFactory.CreateVertexHandle(vert, .3f, Handles.DotCap));

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
                foreach (VertexHandle Vert in HandlesList)
                {
                    Vert.DragHandle(out dhResult);

                    switch (dhResult)
                    {
                        case DragHandleResult.LMBRelease:
                            //Vert.UpdatePosition();
                            UpdateVertsTransfrom();
                            break;
                    }

                }
            }
        }


        */
}

