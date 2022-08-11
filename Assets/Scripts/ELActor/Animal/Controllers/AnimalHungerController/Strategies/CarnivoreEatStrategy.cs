using UnityEngine;
using UnityEngine.AI;

public class CarnivoreEatStrategy : IAnimalEatStrategy
{
    public bool execute(IAnimal animal, INutritional target)
    {
        Debug.Log(0);
        if (!(animal is ICarnivore))
        {
            // Animal is not a Carnivore
            return false;
        }
        Debug.Log(1);
        ICarnivore carnivore = animal as ICarnivore;
        if (!carnivore.DoActorsCollide(target))
        {
            // Animal is not close enough to the target
            return false;
        }
        Debug.Log(2);
        if (!(target is IAnimal))
        {
            // The target is not an Animal
            return false;
        }
        Debug.Log(3);
        if (!carnivore.GetPreyTags().Contains(target.GetTag()))
        {
            // Carnivore does not eat the target Animal
            return false;
        }
        Debug.Log(4);
        Debug.Log(carnivore.GetAnimalHungerController().GetHungerTracker().GetCurrent());
        carnivore.GetAnimalHungerController().GetHungerTracker().AddAmount(target.GetEaten(animal.GetBiteSize()));
        Debug.Log(carnivore.GetAnimalHungerController().GetHungerTracker().GetCurrent());
        return true;
    }
}