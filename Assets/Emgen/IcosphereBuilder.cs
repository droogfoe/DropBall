using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Emgen
{
    public class IcosphereBuilder
    {
        #region Internal Classes
        // Midpoint table, used to avoid vertex duplication
        class MidpointTable
        {
            VertexCache vertexCache;
            Dictionary<int, int> table;

            // Generates a key from a pair of indices.
            static int IndexPairToKey(int i1, int i2)
            {
                // ?
                if (i1 < i2)
                {
                    return i1 | (i2 << 16);
                }
                else
                {
                    return (i1 << 16) | i2;
                }
            }

            // Constructor
            public MidpointTable(VertexCache vc)
            {
                vertexCache = vc;
                table = new Dictionary<int, int>();
            }

            // Get the midpoint of the pair of indices
            //public int GetMidpoint(int i1, int i2)
            //{
            //    var key = IndexPairToKey(i1, i2);

            //    // return from the table
            //    if (table.ContainsKey(key))
            //        return table[key];
            //    // add a new entry to the table
            //    var mid = (vertexCache.vertices[i1] + vertexCache.vertices[i2]) * 0.5f;
            //}
        }
        #endregion
    }
}
