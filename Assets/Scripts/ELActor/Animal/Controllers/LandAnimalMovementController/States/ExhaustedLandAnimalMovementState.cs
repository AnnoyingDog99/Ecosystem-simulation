using UnityEngine;
using UnityEngine.AI;

public class ExhaustedLandAnimalMovementState : LandAnimalMovementState, ILandAnimalMovementState
{
    public ExhaustedLandAnimalMovementState(ILandAnimal animal) : base(animal)
    {
    }

    public bool WalkTo(NavMeshPath path)
    {
        return false;
    }


    public bool RunTo(NavMeshPath path)
    {
        return false;
    }
}