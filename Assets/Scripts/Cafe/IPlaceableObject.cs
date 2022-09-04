using GridSystem;
using UnityEngine;

public interface IPlaceableObject
{
    bool CanPlace(GridXZ grid, ObjectPlacementSystemNode clickedNode);
    void MoveTo(Vector3 position);
}
