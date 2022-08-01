using UnityEngine;
using UnityEngine.AI;

public class EnergizedLandAnimalMovementState : LandAnimalMovementState, ILandAnimalMovementState
{
    public EnergizedLandAnimalMovementState(ILandAnimal animal) : base(animal)
    {
    }

    public bool WalkTo(NavMeshPath path)
    {
        this.context.SetStrategy(new WalkMovementStrategy());
        return this.context.ExecuteStrategy(this.animal, path);
    }

    public bool RunTo(NavMeshPath path)
    {
        this.context.SetStrategy(new RunMovementStrategy());
        return this.context.ExecuteStrategy(this.animal, path);
    }
}