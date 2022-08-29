using UnityEngine;
using UnityEngine.AI;

public class TiredLandAnimalMovementState : LandAnimalMovementState, ILandAnimalMovementState
{
    public TiredLandAnimalMovementState(ILandAnimal animal) : base(animal)
    {
    }

    public bool WalkTo(NavMeshPath path)
    {
        this.context.SetStrategy(new WalkMovementStrategy());
        return this.context.ExecuteStrategy(this.animal, path);
    }
    
    public bool RunTo(NavMeshPath path)
    {
        return this.WalkTo(path);
    }
}