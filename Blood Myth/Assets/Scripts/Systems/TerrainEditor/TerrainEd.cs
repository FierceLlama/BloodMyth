using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum TerrainEditorMode
{
    VERTEXEDIT,
    ADDVERTEX,
    TRANFORMEDIT
}

[ExecuteInEditMode]
public class TerrainEd : MonoBehaviour
{

#if UNITY_EDITOR

    #region Class Members

    [HideInInspector]
    public Vector3[] VList;
    [HideInInspector]
    public TerrainEditorMode Mode;
    
    Mesh _mesh;
    MeshFilter meshFilter;
    MeshRenderer _renderer;
    Triangulator triangluator;

    int vertCount;

    #endregion
    // Use this for initialization
    void Awake()
    {
        triangluator = new Triangulator();
        AddRequiredComponents();
        InitalizeMesh();
    }

    public Vector3 GetVertexOnClick(Vector3 position)
    {

        if (triangluator == null)
            triangluator = new Triangulator();
        
        Vector3 V = transform.InverseTransformPoint(new Vector3(position.x, position.y, 1.0f));

        VertexHandle VertHand = VertexHandleUtility.AddToList(V, VertexHandleUtility.VertexList.Count);
        VertHand.CorrectForTransfrom(transform.position);
        
        GenerateMesh();
      
        return V;
    }

    private void GenerateMesh()
    {
        _mesh = triangluator.TriangulateMesh(GenerateListFromHandles());

        GenereateMeshUVS();

        if (_mesh != null)
            meshFilter.sharedMesh = _mesh;
    }

    private void GenereateMeshUVS()
    {
        Vector2[] UV = { };

        if (_mesh != null)
        { 
            if (_mesh.vertexCount > 3)
            { 
                UV = UvCalculator.CalculateUVs(_mesh.vertices, 1);

            foreach (Vector2 textureUV in UV)
                textureUV.Normalize();
            }
        
            _mesh.uv = UV;
        }
    }

    public Vector3 [] GenerateListFromHandles()
    {
        VList = new Vector3[VertexHandleUtility.VertexList.Count];

        for (int i = 0; i < VList.Length; i++)
        {
            VList[i] = VertexHandleUtility.VertexList[i].TransMeshVertex;
        }

  
        return VList;
    }

    public void ApplyTransformtoVertiexList()
    {
        if (VertexHandleUtility.VertexList != null)
            for (int i = 0; i < VertexHandleUtility.VertexList.Count; i++)
                VertexHandleUtility.VertexList[i].CorrectForTransfrom(transform.position);
    }

    public void Update()
    {
        if (transform.hasChanged)
        {
            ApplyTransformtoVertiexList();
            transform.hasChanged = false;
        }
    }
 
    public void UpdateMeshVertices(int indx)
    {
        VertexHandleUtility.VertexList[indx].CorrectForTransfrom(transform.position);
        VList[indx] = VertexHandleUtility.VertexList[indx].TransMeshVertex;
        
       _mesh = triangluator.TriangulateMesh(VList);
        GenereateMeshUVS();
        meshFilter.sharedMesh = _mesh;
    }

    private void AddRequiredComponents()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        _renderer = gameObject.GetComponent<MeshRenderer>();

        if (meshFilter == null)
            meshFilter = (MeshFilter)gameObject.AddComponent<MeshFilter>();

        if (_renderer == null)
        {
            _renderer = gameObject.AddComponent<MeshRenderer>();
            _renderer.sharedMaterial = new Material(Shader.Find("Standard"));

            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.green);
            tex.alphaIsTransparency = false;
            tex.Apply();

            _renderer.sharedMaterial.mainTexture = tex;
            _renderer.sharedMaterial.color = Color.green;
        }
    }

    private void InitalizeMesh()
    {
        _mesh = new Mesh();
        meshFilter.sharedMesh = _mesh;
        _mesh.name = "ScriptedMesh";

        VList = new Vector3[0];
        vertCount = 0;
    }

    public void ClearMesh()
    {
        _mesh = new Mesh();
        _mesh.name = "Cleared Terrain";
        meshFilter.sharedMesh = _mesh;

        VList = new Vector3[0];
        _mesh.vertices = new Vector3[0];

        transform.position = Vector3.zero;
    }
#endif
}
