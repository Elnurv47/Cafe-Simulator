using GridSystem;
using UnityEngine;

public class ObjectPlacementSystemNode : Node
{
    [SerializeField] private Transform _pivot;

    public Transform Pivot { get => _pivot; }
}
