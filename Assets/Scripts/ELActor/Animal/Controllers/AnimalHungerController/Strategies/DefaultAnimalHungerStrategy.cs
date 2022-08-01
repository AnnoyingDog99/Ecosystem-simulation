using UnityEngine;
using UnityEngine.AI;
public class DefaultAnimalHungerStrategy : IAnimalHungerStrategy
{
    public bool execute(IAnimal animal, INutritional target)
    {
        if (!animal.DoActorsCollide(target)) return false;
        animal.GetAnimalHungerController().GetHungerTracker().AddAmount(target.GetEaten(animal.GetBiteSize()));
        return true;
    }
}