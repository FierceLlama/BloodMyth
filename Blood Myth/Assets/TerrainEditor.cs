using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainEditor : MonoBehaviour {
#if UNITY_EDITOR

    public string meshName;
    public float width, height;

    private bool Generated = false;
    private Mesh _mesh;

    public void UpdateTerrain()
    {
        _mesh.name = meshName;
    }
    public void GenerateTerrain()
    {
       if (!Generated)
       { 
            MeshFilter meshFilter = (MeshFilter)gameObject.AddComponent<MeshFilter>();
            _mesh = meshFilter.mesh = CreateMesh(5, 5);

            MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = new Material(Shader.Find("Standard"));

            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.green);
            tex.alphaIsTransparency = false;
            tex.Apply();

            renderer.sharedMaterial.mainTexture = tex;
            renderer.sharedMaterial.color = Color.green;

            Generated = true;
        }
    }
    public void ClearTerrain()
    {
        if (Generated)
        { 
            DestroyImmediate(gameObject.GetComponent<MeshRenderer>());
            DestroyImmediate(gameObject.GetComponent<MeshFilter>());

            Generated = false;
        }
    }

    Mesh CreateMesh (float width, float height)
    {
        Mesh m = new Mesh();
        m.name = "ScriptedMesh";
        m.vertices = new Vector3[] 
        {
             new Vector3(-width, -height, 1.0f),
             new Vector3(width, -height, 1.0f),
             new Vector3(width, height, 1.0f),
             new Vector3(-width, height, 1.0f)
        };

        m.uv = new Vector2[] 
        {
             new Vector2 (0, 0),
             new Vector2 (0, 1),
             new Vector2(1, 1),
             new Vector2 (1, 0)
        };

        m.triangles = new int[] { 0, 3, 2, 0, 2, 1 };
        m.RecalculateNormals();

       // Debug.Log(m.normals);

        return m;
    }

    // Update is called once per frame
    void Update ()
    {
        if (width < 1) width = 1;
        if (height < 1) height = 1;	
	}

    void OnDrawGizmos()
    {
        
    }

#endif
}
