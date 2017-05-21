using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VertexHandleUtility
{
    private static List<VertexHandle> vertexList;
    public static List<VertexHandle> VertexList
    {
        get { return vertexList; }
    }

    public static void Initialize()
    {
        if (vertexList == null) vertexList = new List<VertexHandle>();
    }

    public static VertexHandle AddToList(Vector3 inMeshVertex, int indx)
    {
        return AddToList(new VertexHandle(inMeshVertex, indx));
    }

    public static VertexHandle AddToList(VertexHandle inHandle)
    {
        vertexList.Add(inHandle);
        return inHandle;
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

    Vector3 transMeshVertex;
    public Vector3 TransMeshVertex { get { return transMeshVertex; } set { transMeshVertex = value; } }

    int controlID;
    public int ControlID { get { return controlID; } }

    public void CorrectForTransfrom (Vector3 position)
    {
        transMeshVertex = meshVertex; // + position;
    }
}