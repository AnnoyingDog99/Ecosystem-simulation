public class FleeFromPredatorsNode : Node
{
    private Animal animal;

    public FleeFromPredatorsNode(Animal animal)
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