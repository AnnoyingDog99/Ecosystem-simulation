using UnityEngine;
using UnityEngine.AI;

public class LandAnimalMovementContext
{
    private ILandAnimalMovementStrategy strategy = null;

    public void SetStrategy(ILandAnimalMovementStrategy strategy)
    {
        this.strategy = strategy;
    }

    public bool ExecuteStrategy(ILandAnimal landAnimal, NavMeshPath path)
    {
        if (this.strategy == null) return false;
        return this.strategy.execute(landAnimal, path);
    }
}