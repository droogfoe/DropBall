using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralFeedBack : MonoBehaviour
{
    public Transform refer;
    public AnimationCurve curve;
    public bool RecalculateNormals = false;
    public Vector3 velocity;
    public float pannerTimer = 1;

    private Vector3 velocityPanner;
    private float panner;

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
        StartCoroutine(LerpBackVelocity());

        oriVertices = mesh.vertices;
        if (vertexDatas == null)
        {
            vertexDatas = new VertexNormal[mesh.vertices.Length];

            for (int i = 0; i < vertexDatas.Length; i++)
            {
                vertexDatas[i].point = mesh.vertices[i];
                vertexDatas[i].normal = mesh.normals[i];
            }
        }
    }
    private void Update()
    {
        Execute();
    }
    public void Execute()
    {
        Vector3[] deformVertices = new Vector3[vertexDatas.Length];

        Vector3 dir = velocity.normalized;
        float speed = velocity.magnitude;

        for (int i = 0; i < deformVertices.Length; i++)
        {
            Vector3 worldSpaceNormal = transform.TransformDirection(vertexDatas[i].normal);

            float dotValue = Vector3.Dot(worldSpaceNormal, dir);
            Vector3 deformValue = vertexDatas[i].point + Vector3.Lerp(transform.InverseTransformDirection (-worldSpaceNormal), transform.InverseTransformDirection(dir) * Mathf.Sign(dotValue), curve.Evaluate(Mathf.Abs(dotValue))) * 0.2f;
            deformVertices[i] = Vector3.Lerp(vertexDatas[i].point, deformValue, speed/8);
        }

        mesh.vertices = deformVertices;

        if (RecalculateNormals)
            mesh.RecalculateNormals();

        mesh.RecalculateBounds();
    }
    private IEnumerator LerpBackVelocity()
    {
        while (true)
        {
            pannerTimer += Time.deltaTime;
            velocity = Vector3.Lerp(velocity, Vector3.zero, pannerTimer);
            yield return null;
        }
    }
    //private void OnDrawGizmos()
    //{
    //    if (!EditorApplication.isPlaying)
    //        return;

    //    Gizmos.matrix = transform.localToWorldMatrix;
    //    Handles.matrix = transform.localToWorldMatrix;
    //    for (int i = 0; i < vertexDatas.Length; i++)
    //    {
    //        Vector3 dir = (refer.position - transform.position).normalized;
    //        float dotValue = Vector3.Dot(transform.TransformDirection(vertexDatas[i].normal), dir);
    //        Handles.Label(vertexDatas[i].point, System.Math.Round(dotValue, 1).ToString());
    //        //Handles.Label(vertexDatas[i].point, Mathf.Sign(dotValue).ToString());
    //        //Gizmos.DrawWireCube(vertexDatas[i].point, Vector3.one * 0.01f);
    //        //DrawArrow.ForGizmo(vertexDatas[i].point, vertexDatas[i].normal);
    //    }
    //}
}
