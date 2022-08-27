using UnityEngine;

namespace GridSystem
{
    public abstract class Node : MonoBehaviour
    {
        private GridIndex _gridIndex;
        public GridIndex GridIndex { get => _gridIndex; private set => _gridIndex = value; }

        public void Initialize(GridIndex gridIndex)
        {
            GridIndex = gridIndex;
        }

        public override string ToString()
        {
            return GridIndex.X + ", " + GridIndex.Z;
        }
    }
}