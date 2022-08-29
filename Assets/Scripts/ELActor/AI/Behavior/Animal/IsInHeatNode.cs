using UnityEngine;
public class IsInHeatNode : Node
{
    private IFertileAnimal animal;

    public IsInHeatNode(IFertileAnimal animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        /**
            Evaluate whether the animal is looking for a partner
        */
        if (this.animal.GetOffspring().Count > 0 || this.animal.GetPartners().Count > 0)
        {
            // Animal already has offspring and/or a partner
            return NodeStates.FAILURE;
        }

        if (this.animal.GetAnimalAgeController().GetAgeTracker().GetStatus().Get() < AgeTracker.AgeStatus.MATURE)
        {
            // Animal is not old enough
            return NodeStates.FAILURE;
        }

        return NodeStates.SUCCESS;
    }
}