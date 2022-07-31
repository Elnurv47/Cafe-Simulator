using GridSystem;
using UnityEngine;

public class PathNode : Node
{
    public bool Walkable { get; set; }
    public PathNode Parent { get; set; }
    public Vector3 Position { get => transform.position; }
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get => GCost + HCost; }

    private void Start()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(1.8f, 0, 1.8f) / 2);
        Walkable = colliders.Length == 0;
    }

    public void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
