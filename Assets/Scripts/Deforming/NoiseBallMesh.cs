using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseBall
{
    public class NoiseBallMesh : ScriptableObject
    {
        #region Public Properties
        [SerializeField, Range(0, 5)]
        private int subdivisionLevel = 1;
        public int SubdivisionLevel
        {
            get { return subdivisionLevel; }
        }
        [SerializeField, HideInInspector]
        private Mesh mesh;
        public Mesh shareMesh 
        {
            get { return mesh; }
        }
        #endregion

        #region public Methods
        public void RebuildMesh()
        {
            if (mesh == null)
            {
                Debug.LogError("Mesh asset is missing.");
                return;
            }

            mesh.Clear();

            //var builder = 
        }
        #endregion
    }
}