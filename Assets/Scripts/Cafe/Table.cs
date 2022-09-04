using GridSystem;
using UnityEngine;

public class Table : MonoBehaviour, IPlaceableObject
{
    public bool CanPlace(GridXZ objectPlacementGrid, ObjectPlacementSystemNode nodeToPlace)
    {
        return nodeToPlace.IsEmpty;
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = position;
    }
}
