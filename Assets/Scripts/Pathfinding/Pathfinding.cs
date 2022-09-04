using Utils;
using GridSystem;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private List<PathNode> _openList;
    private HashSet<PathNode> _closedSet;

    private Path _testPath;

    [SerializeField] private GridXZ _grid;

    [SerializeField] private Transform _seeker;
    [SerializeField] private Transform _target;

    private void Update()
    {
        //_testPath = FindPath(_seeker.position, _target.position);
    }

    public Path FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        _openList = new List<PathNode>();
        _closedSet = new HashSet<PathNode>();

        PathNode startNode = _grid.GetNode(startPosition) as PathNode;
        PathNode targetNode = _grid.GetNode(targetPosition) as PathNode;

        _openList.Add(startNode);

        while (_openList.Count > 0)
        {
            PathNode currentNode = _openList[0];

            for (int i = 1; i < _openList.Count; i++)
            {
                if (_openList[i].FCost < currentNode.FCost || _openList[i].FCost == currentNode.FCost && _openList[i].HCost < currentNode.HCost)
                {
                    currentNode = _openList[i];
                }
            }

            _openList.Remove(currentNode);
            _closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                _testPath = RetracePath(startNode, targetNode);
                return _testPath;
            }

            foreach (PathNode neighbour in _grid.GetNeighbours(currentNode))
            {
                if (!neighbour.Walkable || _closedSet.Contains(neighbour)) continue;

                int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);

                if (newMovementCostToNeighbour < neighbour.GCost || !_openList.Contains(neighbour))
                {
                    neighbour.GCost = newMovementCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour, targetNode);
                    neighbour.Parent = currentNode;

                    if (!_openList.Contains(neighbour))
                    {
                        _openList.Add(neighbour);
                    }
                }
            }
        }

        return Path.Empty;
    }

    private Path RetracePath(PathNode startNode, PathNode targetNode)
    {
        List<PathNode> pathNodes = new List<PathNode>();
        PathNode currentNode = targetNode;

        while (currentNode != startNode)
        {
            pathNodes.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        pathNodes.Reverse();

        return new Path(pathNodes);
    }

    private int GetDistance(PathNode nodeA, PathNode nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.GridIndex.X - nodeB.GridIndex.X);
        int distanceZ = Mathf.Abs(nodeA.GridIndex.Z - nodeB.GridIndex.Z);

        if (distanceX > distanceZ)
        {
            return 14 * distanceZ + 10 * (distanceX - distanceZ);
        }

        return 14 * distanceX + 10 * (distanceZ - distanceX);
    }

    private void OnDrawGizmos()
    {
        List<PathNode> nodes = _grid.GetNodes().Cast<PathNode>().ToList();

        if (nodes == null) return;

        foreach (PathNode node in nodes)
        {
            Gizmos.color = (node.Walkable) ? Color.white : Color.red;
            Gizmos.DrawCube(_grid.GetNodeCenter(node), new Vector3(_grid.CellSize - 0.1f, 0, _grid.CellSize - 0.1f));
        }

        if (_testPath == null) return;

        foreach (PathNode node in _testPath.Nodes)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawCube(node.transform.position, Vector3.one);
        }
    }
}
