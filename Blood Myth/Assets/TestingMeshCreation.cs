using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[ExecuteInEditMode]
public class TestingMeshCreation : MonoBehaviour {

#if UNITY_EDITOR

    Mesh _mesh;
    LineRenderer lr;
    Vector3[] vect;

	// Use this for initialization
	void Awake ()
    {
        lr = gameObject.GetComponent<LineRenderer>();

        _mesh = new Mesh();
        _mesh.name = "ScriptedMesh";
    }

    /*
	// Update is called once per frame
	public void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
             
            GetVertexOnClick(Input.mousePosition);

        }
    }*/

    public void GetVertexOnClick(Vector3 position)
    {
        Vector3 V = new Vector3(position.x, position.y, 1.0f);

        Vector3[] vectTemp;

        if (vect == null)
        {
            vectTemp = new Vector3[1];
            vectTemp[0] = V;
            vect = vectTemp;
        }
        else
        {
            vectTemp = new Vector3[vect.Length + 1];
            for (int i = 0; i < vect.Length; ++i)
                vectTemp[i] = vect[i];

            vectTemp[vect.Length] = V;
        }
       
        _mesh.vertices = vectTemp;
        vect = vectTemp;

        DrawTriangle();
        
        for (int i = 0; i < _mesh.vertices.Length; ++i)
        {
            Debug.Log(_mesh.vertices[i]);
        }

    }

    void DrawTriangle()
    {
        LineRenderer lr = gameObject.GetComponent<LineRenderer>();
        lr.numPositions = _mesh.vertexCount;

        lr.SetPositions(_mesh.vertices);
        lr.startColor = Color.red;
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
    }

    public void ClearTerrain ()
    {
        vect = _mesh.vertices = new Vector3[0];
        lr.SetPositions(new Vector3[0]);
        lr.numPositions = 0;

        Debug.Log(_mesh.vertexCount);
        Debug.Log(lr.numPositions);
    }
#endif
}
