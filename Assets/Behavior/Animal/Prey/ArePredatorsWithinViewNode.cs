using System.Collections.Generic;

public class ArePredatorsWithinViewNode : Node
{
    private Animal animal;
    private List<string> predatorTags;

    public ArePredatorsWithinViewNode(Animal animal, List<string> predatorTags)
    {
        this.animal = animal;
        this.predatorTags = predatorTags;
    }

    public override NodeStates Evaluate()
    {
        /**
            Evaluate whether a Predator can be seen
        */
        return animal.GetSight().GetVisibleTargets().Exists(t => predatorTags.Contains(t.tag)) ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}
