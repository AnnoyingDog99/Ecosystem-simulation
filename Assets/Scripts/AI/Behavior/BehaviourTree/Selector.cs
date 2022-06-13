using System.Collections.Generic;

public class Selector : Node
{
    /** The child nodes for this selector */
    protected List<Node> m_nodes = new List<Node>();


    /** The constructor requires a lsit of child nodes to be 
     * passed in*/
    public Selector(List<Node> nodes)
    {
        m_nodes = nodes;
    }

    /* 
     * If any of the children reports a success, the selector will
     * immediately report a success upwards. 
     * If all children fail, it will report a failure instead.
    */
    public override NodeStates Evaluate()
    {
        foreach (Node node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case NodeStates.FAILURE:
                    continue;
                case NodeStates.SUCCESS:
                    return NodeStates.SUCCESS;
                case NodeStates.RUNNING:
                    return NodeStates.RUNNING;
                default:
                    continue;
            }
        }
        return NodeStates.FAILURE;
    }
}
