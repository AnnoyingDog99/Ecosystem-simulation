using UnityEngine;
using UnityEngine.AI;

public class CarnivoreEatStrategy : IAnimalEatStrategy
{
    public bool execute(IAnimal animal, INutritional target)
    {
        if (!(animal is ICarnivore))
        {
            // Animal is not a Carnivore
            return false;
        }
        ICarnivore carnivore = animal as ICarnivore;
        if (!carnivore.DoActorsCollide(target))
        {
            // Animal is not close enough to the target
            return false;
        }
        if (!(target is IAnimal))
        {
            // The target is not an Animal
            return false;
        }
        if (!carnivore.GetPreyTags().Contains(target.GetTag()))
        {
            // Carnivore does not eat the target Animal
            return false;
        }
        carnivore.GetAnimalHungerController().GetHungerTracker().AddAmount(target.GetEaten(animal.GetBiteSize()));
        return true;
    }
}