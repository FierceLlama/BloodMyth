using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainEd : MonoBehaviour
{

#if UNITY_EDITOR

    Mesh _mesh;
    LineRenderer lr;
    [HideInInspector]
    public Vector3[] OrigVerts;
    [HideInInspector]
    public Vector3[] transVerts;

    int vertCount;
    [HideInInspector]
    public bool EditMode;
    [HideInInspector]
    public bool Generated = false;

    MeshFilter meshFilter;
    MeshRenderer renderer;

    Triangulator triangluator;

    // Use this for initialization
    void Awake()
    {
        triangluator = new Triangulator();
        AddRequiredComponents();
        InitalizeMesh();
    }

    public Vector3 GetVertexOnClick(Vector3 position)
    {
        vertCount = OrigVerts.Length + 1;
        Vector3 V = transform.InverseTransformPoint(new Vector3(position.x, position.y, 1.0f));

        Vector3[] vectTemp = new Vector3[vertCount];
        for (int i = 0; i < OrigVerts.Length; ++i)
            vectTemp[i] = OrigVerts[i];

        vectTemp[OrigVerts.Length] = V;

        OrigVerts = new Vector3[vectTemp.Length];
        Array.Copy(vectTemp, OrigVerts, vectTemp.Length);
        transVerts = new Vector3[vectTemp.Length];
        Array.Copy(vectTemp, transVerts, vectTemp.Length);


        if (triangluator == null)
            triangluator = new Triangulator();
        
        _mesh = triangluator.TriangulateMesh(transVerts);
        meshFilter.sharedMesh = _mesh;
        //_mesh.vertices = transVerts;
        //GenerateMesh();

        return V;
    }
    public void Update()
    {
        if (transform.hasChanged)
        {
            UpdateMeshVertices();
            transform.hasChanged = false;
        }
    }
    public void UpdateMeshVertices()
    {
        for (int i = 0; i < transVerts.Length; ++i)
            transVerts[i] = OrigVerts[i] + transform.position;

        meshFilter.sharedMesh.vertices = transVerts;
    }
    public void UpdateMeshVertices(int indx)
    {
        OrigVerts[indx] = VertexHandleUtility.VertexList[indx].MeshVertex;
        transVerts[indx] = VertexHandleUtility.VertexList[indx].MeshVertex;

        for (int i = 0; i < transVerts.Length; ++i)
            transVerts[i] = OrigVerts[i] + transform.position;

       // _mesh.vertices = transVerts;
       _mesh = triangluator.TriangulateMesh(transVerts);
        meshFilter.sharedMesh = _mesh;
    }

    private void AddRequiredComponents()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        renderer = gameObject.GetComponent<MeshRenderer>();

        if (meshFilter == null)
            meshFilter = (MeshFilter)gameObject.AddComponent<MeshFilter>();

        if (renderer == null)
        {
            renderer = gameObject.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = new Material(Shader.Find("Standard"));

            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.green);
            tex.alphaIsTransparency = false;
            tex.Apply();

            renderer.sharedMaterial.mainTexture = tex;
            renderer.sharedMaterial.color = Color.green;
        }
    }
    private void InitalizeMesh()
    {
        _mesh = new Mesh();
        meshFilter.sharedMesh = _mesh;
        _mesh.name = "ScriptedMesh";

        OrigVerts = new Vector3[0];
        transVerts = new Vector3[0];
        vertCount = 0;
    }

    private void GenerateMesh()
    {
        //Generate Triangles and UVs here.
        if (!Generated)
        {
            //Testing
          /*  if (_mesh.vertexCount >= 4)
            {
                _mesh.uv = new Vector2[]
               {
                    new Vector2 (0, 0),
                    new Vector2 (0, 1),
                    new Vector2(1, 1),
                    new Vector2 (1, 0)
               };
            */
              //  _mesh.triangles = new int[] { 0, 3, 2, 0, 2, 1 };
               // _mesh.RecalculateNormals();
                //This is a reference -- remember this


              //  meshFilter.sharedMesh = triangluator.TriangulateMesh( );
                Generated = true;
            //}
        }
    }

    public void ClearMesh()
    {
        _mesh = new Mesh();
        _mesh.name = "Cleared Terrain";
        meshFilter.sharedMesh = _mesh;

        transVerts = new Vector3[0];
        OrigVerts = new Vector3[0];
        _mesh.vertices = new Vector3[0];

        Generated = false;
        transform.position = Vector3.zero;
    }
#endif
}
