using UnityEngine;
using UnityEngine.AI;
public abstract class LandAnimalMovementStrategy : ILandAnimalMovementStrategy
{
    protected bool MoveTo(ILandAnimal landAnimal, NavMeshPath path, float speed)
    {
        landAnimal.GetAgent().speed = speed;
        return landAnimal.GetAgent().SetPath(path);
    }
    public virtual bool execute(ILandAnimal landAnimal, NavMeshPath path) { return false; }
}
