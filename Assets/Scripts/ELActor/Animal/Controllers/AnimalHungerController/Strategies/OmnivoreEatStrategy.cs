using UnityEngine;
using UnityEngine.AI;

public class OmnivoreEatStrategy : IAnimalEatStrategy
{
    public bool execute(IAnimal animal, INutritional target)
    {
        if (!(animal is IOmnivore))
        {
            // Animal is not an Omnivore
            return false;
        }
        IOmnivore omnivore = animal as IOmnivore;
        if (!omnivore.DoActorsCollide(target))
        {
            // Animal is not close enough to the target
            return false;
        }
        if (!(target is IAnimal) && !(target is IPlant))
        {
            // The target is not an Animal or Plant
            return false;
        }
        if (!omnivore.GetPreyTags().Contains(target.GetTag()) && !omnivore.GetPlantTags().Contains(target.GetTag()))
        {
            // Omnivore does not eat the target Animal or Plant
            return false;
        }
        omnivore.GetAnimalHungerController().GetHungerTracker().AddAmount(target.GetEaten(animal.GetBiteSize()));
        return true;
    }
}