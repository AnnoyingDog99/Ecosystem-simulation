public abstract class Node
{
    public Node() { }

    public abstract NodeStates Evaluate();

}

/** Enum containing the possible states a node can return */
public enum NodeStates
{
    SUCCESS,
    FAILURE,
    RUNNING,
}
