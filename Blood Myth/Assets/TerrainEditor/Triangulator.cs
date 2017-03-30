using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//First pass at Code structure. Probably going to rewrite it all not sure how to structure it so just 
//typing code for now.

public class TriangulatorUtility
{
    //The list of Verticies.
    public static Vector3[] VertexList;

    ///not sure if I need this, still figuring out what info I'll need overall for the algo.
    public static Edge[] EdgeList;
     
    static public float GetPointsDistance ( int indxA, int indxB)
    {
        return Vector3.Distance(VertexList[indxA], VertexList[indxB]);
    }
    static public float GetPointsDistance (Edge InEdge)
    {
       return GetPointsDistance(InEdge.IndxA, InEdge.IndxB);
    }

    //Use this to create a CircumCircle. Calculates it based on 3 verts (from triangle).
    //Calculates the center of the Circumcircle and the Radius.
    static public CircumCircle CalculateCircumCircle (int[] InVerts)
    {
        Vector3 v1 = VertexList[InVerts[0]], v2 = VertexList[InVerts[1]] , v3 = VertexList[InVerts[2]];

        float A2 = Mathf.Pow(v1.x, 2) + Mathf.Pow(v1.y, 2);
        float B2 = Mathf.Pow(v2.x, 2) + Mathf.Pow(v2.y, 2);
        float C2 = Mathf.Pow(v3.x, 2) + Mathf.Pow(v3.y, 2);

        float circumX = (A2 * (v3.y - v2.y) + B2 * (v1.y - v3.y) + C2 * (v2.y - v1.y)) /
                (v1.x * (v3.y - v2.y) + v2.x * (v1.y - v3.y) + v3.x * (v2.y - v1.y)) / 2;
        float circumY = (A2 * (v3.x - v2.x) + B2 * (v1.x - v3.x) + C2 * (v2.x - v1.x)) /
                        (v1.y * (v3.x - v2.x) + v2.y * (v1.x - v3.x) + v3.y * (v2.x - v1.x)) / 2;
        float circumRadius = Mathf.Sqrt(Mathf.Pow(v1.x - circumX, 2) + Mathf.Pow(v1.y - circumY, 2));

        return new CircumCircle (circumX, circumY, circumRadius);
    }

    //the Calculated CircumCirlce of a set of 3 Verts (used with the triangle struct).
    public struct CircumCircle
    {
        public CircumCircle(float in_circumX, float in_circumY, float in_circumRad)
        {
            circumX = in_circumX;
            circumY = in_circumY;
            circumRad = in_circumRad;
        }

        float circumX;
        float circumY;
        float circumRad;

        //Checks if the Vertex passed is inside of the CircumCircle.
        public bool IsInsideOfCircumCircle(Vector3 V)
        {
            float dist = Mathf.Sqrt(Mathf.Pow(V.x - this.circumX, 2) + Mathf.Pow(V.y - this.circumY, 2));
            return dist <= this.circumRad;
        }
    }

    //The edge between 2 vertices;
    //the members of this stuct are the index of the vertices in teh vertex list.
    public struct Edge
    {
        public Edge(int in_A, int in_B)
        {
            indxA = in_A;
            indxB = in_B;
        }
        int indxA;
        public int IndxA { get { return indxA; } }

        int indxB;
        public int IndxB { get { return indxB; } }
    }

    //triangles - made of edges and vertices (same as edges verts are indicies).
    public struct Triangle
    {
        public Triangle(Edge[] in_Edge, int[] in_verts)
        {
            edges = in_Edge;
            vertices = in_verts;
            cCircle = TriangulatorUtility.CalculateCircumCircle(in_verts);
        }
        Edge[] edges;
        int[] vertices;
        public int[] Vertices { get { return vertices; } }
        CircumCircle cCircle;
    }

};
public class Triangulator {

    public static Vector3[] Triangulate (Vector3[] VertexList, int VertexCount)
    {
        TriangulatorUtility.VertexList = VertexList;

        Vector3[] TriangleList;
        TriangleList = new Vector3[VertexCount];

        /////Algorithm!!!
        
        






        /////
        return TriangleList;
    }

}
