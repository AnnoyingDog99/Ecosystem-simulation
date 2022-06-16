public class IsInHeatNode : Node
{
    private Animal animal;

    public IsInHeatNode(Animal animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        /**
            Evaluate whether the animal is looking for a partner
        */
        if (this.animal.GetOffspring().Count > 0 || this.animal.GetPartner() != null)
        {
            // Animal already has offspring and/or a partner
            return NodeStates.FAILURE;
        }

        if (this.animal.GetAge() < this.animal.GetAgeBar().GetAdultAge())
        {
            // Animal is not old enough
            return NodeStates.FAILURE;
        }

        return NodeStates.SUCCESS;
    }
}
