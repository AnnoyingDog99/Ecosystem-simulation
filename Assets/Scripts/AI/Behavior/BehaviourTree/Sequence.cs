using System.Collections.Generic;

public class Sequence : Node
{
    /** Children nodes that belong to this sequence */
    private List<Node> m_nodes = new List<Node>();

    /** Must provide an initial set of children nodes to work */
    public Sequence(List<Node> nodes)
    {
        m_nodes = nodes;
    }

    /* If any child node returns a failure, the entire node fails. Whence all  
     * nodes return a success, the node reports a success. */
    public override NodeStates Evaluate()
    {
        bool anyChildRunning = false;

        foreach (Node node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case NodeStates.FAILURE:
                    return NodeStates.FAILURE;
                case NodeStates.SUCCESS:
                    continue;
                case NodeStates.RUNNING:
                    anyChildRunning = true;
                    continue;
                default:
                    return NodeStates.SUCCESS;
            }
        }
        return anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;;
    }
}