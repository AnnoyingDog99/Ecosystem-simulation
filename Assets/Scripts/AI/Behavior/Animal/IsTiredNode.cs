public class IsTiredNode : Node
{
    private Animal animal;

    private bool keepResting = false;

    public IsTiredNode(Animal animal, float minRestTime)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {

        if (this.animal.GetStaminaBar().IsTired())
        {
            // Keep resting if animal is still tired
            this.keepResting = true;
        }
        else if (this.animal.GetStaminaBar().GetStaminaPercentage() >= 100)
        {
            // Stop resting if animal is fully rested
            this.keepResting = false;
        }

        /**
            Evaluate whether the animal should keep resting
        */
        return keepResting ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}
