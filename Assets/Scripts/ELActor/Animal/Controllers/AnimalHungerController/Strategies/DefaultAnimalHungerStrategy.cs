using UnityEngine;
using UnityEngine.AI;
public class DefaultAnimalHungerStrategy : IAnimalHungerStrategy
{
    public bool execute(IAnimal animal, INutritional target)
    {
        if (!animal.DoActorsCollide(target))
        {
            return false;
        }
        if (!(animal is IHerbivore && target is IPlant))
        {
            return false;
        }
        if (!(animal is ICarnivore && target is IAnimal))
        {
            return false;
        }
        animal.GetAnimalHungerController().GetHungerTracker().AddAmount(target.GetEaten(animal.GetBiteSize()));
        return true;
    }
}