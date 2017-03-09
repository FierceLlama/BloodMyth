using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[ExecuteInEditMode]
public class TestingMeshCreation : MonoBehaviour {

#if UNITY_EDITOR

    Mesh _mesh;
    LineRenderer lr;
    [HideInInspector]
    public Vector3[] vect;

    int vertCount;
    [HideInInspector]
    public bool EditMode;
    private bool Generated = false;

    MeshFilter meshFilter;
    MeshRenderer renderer;

    // Use this for initialization
    void Awake ()
    {
        AddRequiredComponents();
        InitalizeMesh();
    }

    public void GetVertexOnClick(Vector3 position)
    {
        vertCount = vect.Length + 1;
        Vector3 V = new Vector3(position.x, position.y, 1.0f);
        Debug.Log(V);
        Vector3[] vectTemp = new Vector3[vertCount];
        for (int i = 0; i < vect.Length; ++i)
            vectTemp[i] = vect[i];

        vectTemp[vect.Length] = V;

        vect = vectTemp;
        _mesh.vertices = vect;

        GenerateMesh();
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
        meshFilter.mesh = _mesh;
        _mesh.name = "ScriptedMesh";

        vertCount = 0;
    }

    private void GenerateMesh()
    {
        //Generate Triangles and UVs here.
        if (!Generated)
        {
            //Testing
            if (_mesh.vertexCount >= 4)
            {
                _mesh.uv = new Vector2[]
               {
                    new Vector2 (0, 0),
                    new Vector2 (0, 1),
                    new Vector2(1, 1),
                    new Vector2 (1, 0)
               };

                _mesh.triangles = new int[] { 0, 3, 2, 0, 2, 1 };
                _mesh.RecalculateNormals();
                meshFilter.mesh = _mesh;
                Generated = true;
            }
        }
    }

    public void ClearMesh ()
    {
        _mesh = new Mesh();
        _mesh.name = "Terrain";
        meshFilter.mesh = _mesh;

        vect = _mesh.vertices = new Vector3[0];
        Generated = false;
    }
    
#endif
}
