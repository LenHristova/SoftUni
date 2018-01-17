class Node
{
    public int Value { get; }

    public Node Parent { get; }

    public Node(int value, Node parent)
    {
        Value = value;
        Parent = parent;
    }
}