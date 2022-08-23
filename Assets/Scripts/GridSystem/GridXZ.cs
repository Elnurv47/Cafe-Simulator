using Utils;
using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace GridSystem
{
    public class GridXZ : MonoBehaviour, IGrid
    {
        private bool _activeSelf;
        public bool ActiveSelf { get => _activeSelf; }

        [SerializeField] private int _width;
        [SerializeField] private int _length;
        [SerializeField] private float _cellSize;

        public float CellSize { get => _cellSize; }

        [SerializeField] private Vector3 _origin;
        [SerializeField] private bool _isDebugEnabled;
        [SerializeField] private GameObject _nodePrefab;
        [SerializeField] private Transform _nodeContainer;

        private Node[,] _nodes;

        private TextMesh[,] _nodesDebugArray;

        public int Width { get => _width; }
        public int Length { get => _length; }

        public event Action OnGridActivated;
        public event Action OnGridDeactivated;

        public TextMesh[,] GetDebugArray() => _nodesDebugArray;

        public void Start()
        {
            _activeSelf = gameObject.activeSelf;
            _nodes = new Node[_width, _length];
            _nodesDebugArray = new TextMesh[_width, _length];

            #region Debugging
            if (_isDebugEnabled)
            {
                for (int x = 0; x < _nodes.GetLength(0); x++)
                {
                    for (int z = 0; z < _nodes.GetLength(1); z++)
                    {
                        GridIndex gridIndex = new GridIndex(x, 0, z);

                        GameObject spawnedNodeObject = Instantiate(_nodePrefab, GetNodeCenter(gridIndex), Quaternion.Euler(90, 0, 0), _nodeContainer);
                        spawnedNodeObject.transform.localScale = new Vector3(_cellSize, _cellSize, 0.001f);
                        Node spawnedNode = spawnedNodeObject.GetComponent<Node>();

                        SetNode(gridIndex, spawnedNode);

                        _nodesDebugArray[x, z] = Utility.CreateWorldText(
                            _nodes[x, z].ToString(),
                            fontSize: 24,
                            position: GetNodePosition(new GridIndex(x, 0, z)) + new Vector3(0.5f, 0, 0.5f) * _cellSize,
                            rotation: Quaternion.Euler(90, 0, 0),
                            localScale: Vector3.one * 0.2f,
                            color: Color.black
                        );

                        //Debug.DrawLine(GetNodePosition(new GridIndex(x, 0, z)), GetNodePosition(new GridIndex(x, 0, z + 1)), Color.white, 1000f);
                        //Debug.DrawLine(GetNodePosition(new GridIndex(x, 0, z)), GetNodePosition(new GridIndex(x + 1, 0, z)), Color.white, 1000f);
                    }
                }

                //Debug.DrawLine(GetNodePosition(new GridIndex(0, 0, _length)), GetNodePosition(new GridIndex(_width, 0, _length)), Color.white, 1000f);
                //Debug.DrawLine(GetNodePosition(new GridIndex(_width, 0, 0)), GetNodePosition(new GridIndex(_width, 0, _length)), Color.white, 1000f);
            }
            #endregion
        }

        public Vector3 GetNodePosition(GridIndex gridIndex)
        {
            if (!IsValidCoordinate(gridIndex))
            {
                Debug.LogWarning($"Specified gridIndex is not valid: {gridIndex}");
            }

            return gridIndex.ToVector3() * _cellSize + _origin;
        }

        private GridIndex ClampToGrid(GridIndex gridIndex)
        {
            gridIndex.X = gridIndex.X < 0 ? 0 : gridIndex.X;
            gridIndex.X = gridIndex.X >= _width ? _width - 1 : gridIndex.X;
            gridIndex.Z = gridIndex.Z < 0 ? 0 : gridIndex.Z;
            gridIndex.Z = gridIndex.Z >= _length ? _length - 1 : gridIndex.Z;

            return gridIndex;
        }

        public Vector3 GetNodeCenter(GridIndex gridIndex)
        {
            if (!IsValidCoordinate(gridIndex))
            {
                gridIndex = ClampToGrid(gridIndex);
            }

            Vector3 offset = new Vector3(1, 0, 1) * _cellSize * 0.5f;
            return gridIndex.ToVector3() * _cellSize + offset + _origin;
        }

        public Vector3 GetNodeCenter(Vector3 worldPosition)
        {
            GridIndex gridIndex = GetGridIndex(worldPosition);
            return GetNodeCenter(gridIndex);
        }

        public Vector3 GetNodeCenter(Node node)
        {
            return GetNodeCenter(node.GridIndex);
        }

        public void SetNode(Vector3 cellWorldPosition, Node node)
        {
            GridIndex point = GetGridIndex(cellWorldPosition);
            SetNode(point, node);
        }

        private GridIndex GetGridIndex(Vector3 cellWorldPosition)
        {
            int x = Mathf.FloorToInt((cellWorldPosition - _origin).x / _cellSize);
            int z = Mathf.FloorToInt((cellWorldPosition - _origin).z / _cellSize);

            GridIndex gridIndex = new GridIndex(x, 0, z);

            return IsValidCoordinate(gridIndex) ? gridIndex : ClampToGrid(gridIndex);
        }

        public Node GetNode(GridIndex gridIndex)
        {
            if (!IsValidCoordinate(gridIndex)) return null;

            return _nodes[gridIndex.X, gridIndex.Z];
        }

        public Node GetNode(Vector3 cellWorldPosition)
        {
            GridIndex gridIndex = GetGridIndex(cellWorldPosition);
            return IsValidCoordinate(gridIndex) ? _nodes[gridIndex.X, gridIndex.Z] : default;
        }

        private void SetNode(GridIndex gridIndex, Node node)
        {
            if (!IsValidCoordinate(gridIndex)) return;
            node.GridIndex = gridIndex;
            _nodes[gridIndex.X, gridIndex.Z] = node;
        }

        private bool IsValidCoordinate(GridIndex gridIndex)
        {
            return gridIndex.X >= 0 && gridIndex.Z >= 0 && gridIndex.X < _width && gridIndex.Z < _length;
        }

        public List<Node> GetNodes()
        {
            /*foreach (Node node in _nodes)
            {
                Replace current method with this one
            }*/


            List<Node> nodes = new List<Node>();

            if (_nodes == null) return nodes;

            for (int i = 0; i < _nodes.GetLength(0); i++)
            {
                for (int j = 0; j < _nodes.GetLength(1); j++)
                {
                    nodes.Add(_nodes[i, j]);
                }
            }

            return nodes;
        }

        public Vector3 GetGridCenterPosition()
        {
            return new Vector3(_width * _cellSize / 2, 0, _length * _cellSize / 2)- _origin;
        }

        public List<Node> GetNodesInsideArea(Vector3 center, Vector3 halfExtents)
        {
            Collider[] colliders = Physics.OverlapBox(center, halfExtents - Vector3.one * 0.1f, Quaternion.identity);
            return colliders.Where(collider => collider.GetComponent<Node>() != null).Select(collider => collider.GetComponent<Node>()).ToList();
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    if (x == node.GridIndex.X && z == node.GridIndex.Z) continue;

                    int checkX = node.GridIndex.X + x;
                    int checkZ = node.GridIndex.Z + z;
                    GridIndex checkGridIndex = new GridIndex(checkX, 0, checkZ);

                    if (IsValidCoordinate(checkGridIndex))
                    {
                        neighbours.Add(GetNode(checkGridIndex)); 
                    }
                }
            }

            return neighbours;
        }

        public void SetActive(bool active)
        {
            _activeSelf = active;
            _nodeContainer.gameObject.SetActive(active);

            if (active)
            {
                OnGridActivated?.Invoke();
            }
            else
            {
                OnGridDeactivated?.Invoke();
            }
        }
    }
}