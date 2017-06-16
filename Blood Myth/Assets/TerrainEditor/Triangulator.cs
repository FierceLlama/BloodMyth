using System.Collections;
using System.Collections.Generic;
using TriangleNet.Geometry;
using UnityEngine;

//First pass at Code structure. Probably going to rewrite it all not sure how to structure it so just 
//typing code for now.

public class Triangulator
{
    TriangleNet.Mesh meshRepresentation;
    TriangleNet.Geometry.InputGeometry geometry;

    public float distance = 5;
    public float verticalDistance = 2;
    public float boxDistance = 1f;
    public float circleDistance = 0.7f;

    public float boxWidth = 1f;
    public float zOffset = 0f;

    Mesh mesh;
    
    public Mesh TriangulateMesh (Vector3[] Vertices)
    {
        if (Vertices.Length < 3)
            return null;
        
        geometry = new InputGeometry();

        foreach (Vector3 Vert in Vertices)
            geometry.AddPoint(Vert.x, Vert.y);

        List<Point> points = new List<Point>();
        for (float offsetX = -distance; offsetX < distance; offsetX += boxDistance)
        {
            for (float offsetY = -verticalDistance; offsetY < verticalDistance; offsetY += boxDistance)
            {
                Vector2 offset = new Vector2(offsetX, offsetY) + Vector2.one * boxDistance * 0.5f;

                float radians = Random.Range(0, 2 * Mathf.PI);
                float length = Random.Range(0, circleDistance);

                Vector2 pos = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * length;
                pos += offset;
            }
        }

        meshRepresentation = new TriangleNet.Mesh();
        meshRepresentation.Triangulate(geometry);

        //generate mesh based on triangulation

        Dictionary<int, float> zOffsets = new Dictionary<int, float>();

        foreach (KeyValuePair<int, TriangleNet.Data.Vertex> pair in meshRepresentation.vertices)
        {
            zOffsets.Add(pair.Key, Random.Range(-zOffset, zOffset));
        }

        int triangleIndex = 0;
        List<Vector3> vertices = new List<Vector3>(meshRepresentation.triangles.Count * 3);
        List<int> triangleIndices = new List<int>(meshRepresentation.triangles.Count * 3);

        foreach (KeyValuePair<int, TriangleNet.Data.Triangle> pair in meshRepresentation.triangles)
        {
            TriangleNet.Data.Triangle triangle = pair.Value;

            TriangleNet.Data.Vertex vertex0 = triangle.GetVertex(0);
            TriangleNet.Data.Vertex vertex1 = triangle.GetVertex(1);
            TriangleNet.Data.Vertex vertex2 = triangle.GetVertex(2);

            Vector3 p0 = new Vector3(vertex0.x, vertex0.y, zOffsets[vertex0.id]);
            Vector3 p1 = new Vector3(vertex1.x, vertex1.y, zOffsets[vertex1.id]);
            Vector3 p2 = new Vector3(vertex2.x, vertex2.y, zOffsets[vertex2.id]);

            vertices.Add(p0);
            vertices.Add(p1);
            vertices.Add(p2);

            triangleIndices.Add(triangleIndex + 2);
            triangleIndices.Add(triangleIndex + 1);
            triangleIndices.Add(triangleIndex);

            triangleIndex += 3;
        }

        mesh = new Mesh();
        mesh.name = "Triangulated Terrain";
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangleIndices.ToArray();
    
        return mesh;
    }

}
