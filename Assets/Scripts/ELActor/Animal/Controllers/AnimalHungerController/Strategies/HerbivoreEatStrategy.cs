using UnityEngine;
using UnityEngine.AI;

public class HerbivoreEatStrategy : IAnimalEatStrategy
{
    public bool execute(IAnimal animal, INutritional target)
    {
        if (!(animal is IHerbivore))
        {
            // Animal is not a Herbivore
            return false;
        }
        IHerbivore herbivore = animal as IHerbivore;
        if (!herbivore.DoActorsCollide(target))
        {
            // Animal is not close enough to the target
            return false;
        }
        if (!(target is IPlant))
        {
            // The target is not a Plant
            return false;
        }
        if (!herbivore.GetPlantTags().Contains(target.GetTag()))
        {
            // Herbivore does not eat the target Plant
            return false;
        }
        herbivore.GetAnimalHungerController().GetHungerTracker().AddAmount(target.GetEaten(animal.GetBiteSize()));
        return true;
    }
}