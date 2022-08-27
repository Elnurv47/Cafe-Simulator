using GridSystem;
using UnityEngine;

public class ObjectPlacementSystemNode : Node
{
    private GameObject _placedObject;
    public GameObject PlacedObject { get => _placedObject; set => _placedObject = value; }

    [SerializeField] private Transform _pivot;
    public Transform Pivot { get => _pivot; }

    public bool IsEmpty { get => _placedObject == null; }
}
