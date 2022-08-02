using UnityEngine;
public class OldAnimalGrowStrategy : IAnimalGrowStrategy
{
    public void execute(IAgeableAnimal animal)
    {
        // Set the initial scale
        animal.GetActorScaleController().SetScale(animal.GetMaxScale());
    }
}