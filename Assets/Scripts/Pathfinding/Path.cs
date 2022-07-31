using System.Collections.Generic;

public class Path
{
    private List<PathNode> _nodes;
    public List<PathNode> Nodes { get => _nodes; }

    public static Path Empty { get => new Path(new List<PathNode>()); }

    public Path(List<PathNode> nodes)
    {
        _nodes = nodes;
    }
}
