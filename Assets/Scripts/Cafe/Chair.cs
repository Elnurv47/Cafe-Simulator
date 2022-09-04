using GridSystem;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, IPlaceableObject
{
    [SerializeField] private Transform _sittingPositionTransform;

    public Customer SeatedCustomer { get; set; }
    public bool IsAvailable { get => SeatedCustomer == null; }
    public Vector3 Position { get => transform.position; }
    public Vector3 SittingPosition { get => _sittingPositionTransform.position; }
    public Table ConnectedTable { get; private set; }

    public bool CanPlace(GridXZ objectPlacementGrid, ObjectPlacementSystemNode nodeToPlace)
    {
        bool canPlace = false;

        List<Node> neighbours = objectPlacementGrid.GetNeighbours(nodeToPlace);

        foreach (ObjectPlacementSystemNode neighbour in neighbours)
        {
            if (neighbour.PlacedObject is Table)
            {
                ConnectedTable = neighbour.PlacedObject as Table;
                canPlace = true;
            }
        }

        return canPlace && nodeToPlace.IsEmpty;
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = position;
    }
}
