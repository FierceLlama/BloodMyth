using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainEditor : MonoBehaviour {
#if UNITY_EDITOR

    public string MehsName
    {
        get { return _mesh.name; }
        set { _mesh.name = value; }
    }
    private Mesh _mesh;

    public float width = 5;
    public float height = 5 ;

	// Use this for initialization
	void Awake ()
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
#endif

    // Update is called once per frame
    void Update () {
		
	}
}
