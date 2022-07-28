public class Inverter : Node
{
    /* Child node to evaluate */
    private Node m_node;

    public Node node
    {
        get { return m_node; }
    }

    /* 
    * The constructor requires the child node that this inverter decorator 
    * wraps
    */
    public Inverter(Node node)
    {
        m_node = node;
    }

    /* 
    * Reports a success if the child fails and 
    * a failure if the child succeeds. Running will report 
    * as running 
    */
    public override NodeStates Evaluate()
    {
        switch (m_node.Evaluate())
        {
            case NodeStates.FAILURE:
                return NodeStates.SUCCESS;
            case NodeStates.SUCCESS:
                return NodeStates.FAILURE;
            case NodeStates.RUNNING:
                return NodeStates.RUNNING;
        }
        return NodeStates.SUCCESS;
    }
}