using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    public MeshCollider meshCollider;

    void OnDrawGizmos()
    {
        if (meshCollider == null)
            meshCollider = GetComponent<MeshCollider>(); // Automatically find MeshCollider if not assigned

        if (meshCollider != null && meshCollider.sharedMesh != null)
        {
            Gizmos.color = Color.red; // Set the color for the outline

            // Draw the outline of the MeshCollider's mesh
            DrawMeshOutline(meshCollider.sharedMesh, meshCollider.transform);
        }
    }

    // Function to draw the outline of a mesh
    private void DrawMeshOutline(Mesh mesh, Transform transform)
    {
        // Get the mesh vertices and edges
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        // Go through each triangle and draw lines for its edges
        for (int i = 0; i < triangles.Length; i += 3)
        {
            // Get the 3 vertices of the current triangle
            Vector3 v0 = transform.TransformPoint(vertices[triangles[i]]);
            Vector3 v1 = transform.TransformPoint(vertices[triangles[i + 1]]);
            Vector3 v2 = transform.TransformPoint(vertices[triangles[i + 2]]);

            // Draw the edges of the triangle
            Gizmos.DrawLine(v0, v1);
            Gizmos.DrawLine(v1, v2);
            Gizmos.DrawLine(v2, v0);
        }
    }
}
