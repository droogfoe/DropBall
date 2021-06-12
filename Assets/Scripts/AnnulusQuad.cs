using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnulusQuad : MonoBehaviour
{
    public enum Ringtype
    {
        FullLine,
        DottedLine,
        Pentagon,
        Square,
        Triangle,
        Disk
    }

    public int depth = 4;
    public float out_radius = 1;
    private float inner_radius;
    private float width = 2.5f;
    public Ringtype ringtype = Ringtype.DottedLine;
    private MeshFilter meshFilter;
    private Mesh mesh;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        Rebuild();
    }
    private void Rebuild()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
        }
        switch (ringtype)
        {
            case Ringtype.FullLine:
                break;
            case Ringtype.DottedLine:
                depth = 4;
                out_radius = -3;
                width = 0.2f;
                RebuildRingDataDotted();
                break;
            case Ringtype.Pentagon:
                break;
            case Ringtype.Square:
                break;
            case Ringtype.Triangle:
                break;
            case Ringtype.Disk:
                break;
            default:
                break;
        }

        meshFilter.sharedMesh = mesh;
    }

    private float Deg2Red;
    private Vector3[] vertices;
    private int[] indices;
    private Vector2[] uvs;

    private void RebuildRingDataDotted()
    {
        mesh.Clear();
        Deg2Red = Mathf.Deg2Rad;
        inner_radius = out_radius - width;
        int count = 360 / depth;

        int totalTriangleCount = count * 2;
        int totalVertexCount   = totalTriangleCount * 3;

        if (vertices == null || vertices.Length < totalVertexCount)
        {
            vertices = new Vector3[totalVertexCount];
        }
        if (indices == null || indices.Length < totalVertexCount)
        {
            indices = new int[totalVertexCount];
        }
        if (uvs == null || uvs.Length < totalVertexCount)
        {
            uvs = new Vector2[totalVertexCount];
        }

        int vertexIndex = 0;
        for (int i = 0; i < count; i++)
        {
            Vector3 v0 = GetPos(out_radius, i * depth);
            Vector3 v1 = GetPos(inner_radius, i * depth);
            Vector3 v2 = GetPos(out_radius, (i + 1) * depth);
            Vector3 v3 = GetPos(inner_radius, (i + 1) * depth);

            vertices[vertexIndex++] = v0;
            vertices[vertexIndex++] = v1;
            vertices[vertexIndex++] = v2;
            vertices[vertexIndex++] = v2;
            vertices[vertexIndex++] = v1;
            vertices[vertexIndex++] = v3;
        }
        for (int i = 0; i < totalVertexCount; i++)
        {
            indices[i] = i;
            uvs[i] = Vector2.zero;
        }
        // ?
        if (vertices.Length > totalVertexCount)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = Vector3.zero;
                indices[i] = 0;
            }
        }
        mesh.vertices = vertices;
        mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
        mesh.uv = uvs;
        mesh.MarkDynamic();
    }
    private Vector3 GetPos(float _radius, float _angle)
    {
        float x = _radius * Mathf.Sin(_angle * Deg2Red);
        float y = _radius * Mathf.Cos(_angle * Deg2Red);
        return new Vector3(x, y, 0);
    }
}
