using System.Collections.Generic;

public class Sequence : Node
{
    /** Children nodes that belong to this sequence */
    private List<Node> nodes = new List<Node>();

    /** Must provide an initial set of children nodes to work */
    public Sequence(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    /* 
     * If any child node returns a failure, the entire node fails. 
     * If all nodes return a success, the node reports a success. 
    */
    public override NodeStates Evaluate()
    {
        bool anyChildRunning = false;

        foreach (Node node in this.nodes)
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
        return anyChildRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
    }
}