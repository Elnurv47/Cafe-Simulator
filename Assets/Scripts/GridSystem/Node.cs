using UnityEngine;

namespace GridSystem
{
    public abstract class Node : MonoBehaviour
    {
        private GridIndex _gridIndex;
        public GridIndex GridIndex { get => _gridIndex; set => _gridIndex = value; }

        public override string ToString()
        {
            return GridIndex.X + ", " + GridIndex.Z;
        }
    }
}