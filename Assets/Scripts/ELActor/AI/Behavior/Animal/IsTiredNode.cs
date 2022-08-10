using UnityEngine;
public class IsTiredNode : Node
{
    private Animal animal;

    private bool keepResting = false;

    public IsTiredNode(Animal animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        float currentStaminaPercentage = this.animal.GetAnimalMovementController().GetStaminaTracker().GetCurrentPercentage();
        if (currentStaminaPercentage < this.animal.GetAnimalMovementController().GetStaminaTracker().GetTiredPercentage())
        {
            // Keep resting if animal is not energized
            this.keepResting = true;
        }
        else if (currentStaminaPercentage >= 100)
        {
            // Stop resting if animal has fully rested
            this.keepResting = false;
        }

        /**
            Evaluate whether the animal should rest
        */
        return keepResting ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}