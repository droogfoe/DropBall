using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class DeformTest : MonoBehaviour
{
    [Range(0, 1)]
    public float panner;
    public AnimationCurve curve;
    public Transform referTrans;
    public bool RecalculateNormals = false;

    public struct VertexNormal 
    {
        public Vector3 point;
        public Vector3 normal;
    }

    private VertexNormal[] vertexDatas;
    private Vector3[] oriVertices;
    private Mesh mesh;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        oriVertices = mesh.vertices;
        if (vertexDatas == null)
        {
            vertexDatas = new VertexNormal[mesh.vertices.Length];

            for (int i = 0; i < vertexDatas.Length; i++)
            {
                vertexDatas[i].point  = mesh.vertices[i];
                vertexDatas[i].normal = mesh.normals[i];
            }
        }  
    }
    private void Update()
    {
        Vector3[] deformVertices = new Vector3[vertexDatas.Length];
        
        Vector3 dir = (referTrans.position - transform.position).normalized;
        DrawArrow.ForDebug(transform.position, dir, Color.red);

        for (int i = 0; i < deformVertices.Length; i++)
        {
            float dotValue = Vector3.Dot(vertexDatas[i].normal, dir);
            Vector3 deformValue = vertexDatas[i].point + Vector3.Lerp(-vertexDatas[i].normal, dir * Mathf.Sign(dotValue), Mathf.Abs(dotValue)) * curve.Evaluate(Mathf.Abs(dotValue)) * 0.2f;
            deformVertices[i] = Vector3.Lerp(vertexDatas[i].point, deformValue, panner);
        }

        mesh.vertices = deformVertices;

        if (RecalculateNormals)
            mesh.RecalculateNormals();

        mesh.RecalculateBounds();
    }
    private void OnDrawGizmos()
    {
        if (!EditorApplication.isPlaying)
            return;

        Gizmos.matrix  = transform.localToWorldMatrix;
        Handles.matrix = transform.localToWorldMatrix;
        for (int i = 0; i < vertexDatas.Length; i++)
        {
            Vector3 dir = (referTrans.position - transform.position).normalized;
            float dotValue = Vector3.Dot(vertexDatas[i].normal, dir);
            //Handles.Label(vertexDatas[i].point, System.Math.Round(dotValue, 1).ToString());
            if(Mathf.Abs(dotValue) < 0.125f)
                Handles.Label(vertexDatas[i].point, Mathf.Sign(dotValue).ToString());
            //Gizmos.DrawWireCube(vertexDatas[i].point, Vector3.one * 0.01f);
            //DrawArrow.ForGizmo(vertexDatas[i].point, vertexDatas[i].normal);
        }
    }
}
