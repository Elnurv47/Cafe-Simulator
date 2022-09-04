using GridSystem;
using UnityEngine;

public class ObjectPlacementSystemNode : Node
{
    /*private GameObject _placedObject;
    public GameObject PlacedObject { get => _placedObject; set => _placedObject = value; }*/

    public IPlaceableObject PlacedObject { get; set; }

    [SerializeField] private Transform _pivot;
    public Transform Pivot { get => _pivot; }

    public bool IsEmpty { get => PlacedObject == null; }
}
