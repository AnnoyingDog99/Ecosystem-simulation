using UnityEngine;

public class LandAnimalStaminaAddon : StatusTrackerAddon
{
    [SerializeField] private float walkingCost;
    [SerializeField] private float runningCost;

    private ILandAnimal landAnimal;

    public void Awake()
    {
        this.landAnimal = GetComponentInParent<ILandAnimal>();
    }

    public override float CalculateCost()
    {
        float cost = 0;
        if (this.landAnimal.GetLandAnimalMovementController().IsWalking())
        {
            cost += (this.walkingCost * Time.deltaTime);
        }
        else if (this.landAnimal.GetLandAnimalMovementController().IsRunning())
        {
            cost += (this.runningCost * Time.deltaTime);
        }
        return cost;
    }
}