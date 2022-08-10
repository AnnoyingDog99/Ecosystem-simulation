using UnityEngine;
using UnityEngine.AI;

public class AnimalBreedingContext
{
    private IAnimalBreedingStrategy strategy = null;

    public void SetStrategy(IAnimalBreedingStrategy strategy)
    {
        this.strategy = strategy;
    }

    public bool ExecuteStrategy(IFertileAnimal animal, IFertileAnimal partner)
    {
        if (this.strategy == null) return false;
        return this.strategy.execute(animal, partner);
    }
}