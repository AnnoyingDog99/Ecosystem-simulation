public class CheckForPredatorsNode : Node
{
    private Animal animal;

    public CheckForPredatorsNode(Animal animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        /**
            Run opposite direction of predator
        */
        return NodeStates.SUCCESS;
    }
}