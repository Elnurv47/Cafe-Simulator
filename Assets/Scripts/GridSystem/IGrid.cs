using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public interface IGrid
    {
        int Width { get; }
        int Length { get; }
        Vector3 GetNodeOrigin(GridIndex point);
        Vector3 GetNodeCenter(GridIndex point);
        Vector3 GetNodeCenter(Vector3 worldPosition);
        Node GetNode(Vector3 cellWorldPosition);
        Node GetNode(GridIndex gridIndex);
        TextMesh[,] GetDebugArray();
        void SetActive(bool active);
        List<Node> GetNodesInsideArea(Vector3 center, Vector3 halfExtents);
        Vector3 GetGridCenterPosition();
        List<Node> GetNodes();
        List<Node> GetNeighbours(Node node);
    }
}