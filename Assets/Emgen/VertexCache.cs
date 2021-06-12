using UnityEngine;
using System.Collections.Generic;

namespace Emgen
{
    public class VertexCache
    {
        #region Internal Triangle Representations
        public struct RawTriangle
        {
            public Vector3 v1, v2, v3;
            public RawTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.v3 = v3;
            }
        }
        public struct IndexedTriangle
        {
            public int i1, i2, i3;
            public IndexedTriangle(int i1, int i2, int i3)
            {
                this.i1 = i1;
                this.i2 = i2;
                this.i3 = i3;
            }
        }
        #endregion

        #region Public Data Fields
        public List<Vector3> vertices;
        public List<IndexedTriangle> triangles;
        #endregion

        #region Constructors
        public VertexCache(VertexCache source)
        {
            vertices  = new List<Vector3>(source.vertices);
            triangles = new List<IndexedTriangle>(source.triangles);
        }
        #endregion

        #region Vertex Accessors
        public int AddVertex(Vector3 v)
        {
            var i = vertices.Count;
            vertices.Add(v);
            return i;
        }
        public int LookUpVertex(Vector3 v)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if ((vertices[i] - v).sqrMagnitude < 0.001f)
                {
                    return i;
                }
            }
            return -1;
        }
        public int LookUpOraddVertex(Vector3 v)
        {
            var index = LookUpVertex(v);
            
            if (index >= 0)
                return index;

            vertices.Add(v);
            return vertices.Count - 1;
        }
        #endregion

        #region Triangle Accessors
        public IEnumerable<RawTriangle> GetRawTrianglesEnumerator()
        {
            foreach (var t in triangles)
            {
                yield return new RawTriangle(
                    vertices[t.i1], vertices[t.i2], vertices[t.i3]);
            }
        }

        public void AddTriangle(int i1, int i2, int i3)
        {
            triangles.Add(new IndexedTriangle(i1, i2, i3));
        }
        public void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            triangles.Add(new IndexedTriangle(
                LookUpOraddVertex(v1),
                LookUpOraddVertex(v2),
                LookUpOraddVertex(v3)
                ));
        }
        #endregion

        #region Line Mesh Builder
        public Vector3[] MakeVertexArrayForLineMesh()
        {
            return vertices.ToArray();
        }
        public int[] MakeIndexArrayForLineMesh()
        {
            var indices = new int[6 * triangles.Count];
            var i = 0;
            foreach (var t in triangles)
            {
                indices[i++] = t.i1;
                indices[i++] = t.i2;
                indices[i++] = t.i2;
                indices[i++] = t.i3;
                indices[i++] = t.i3;
                indices[i++] = t.i1;
            }
            return indices;
        }
        public Mesh BuildLineMesh()
        {
            var mesh = new Mesh();
            mesh.vertices = MakeVertexArrayForLineMesh();
            mesh.SetIndices(MakeIndexArrayForLineMesh(), MeshTopology.Lines, 0);
            return mesh;
        }
        #endregion
    }
}