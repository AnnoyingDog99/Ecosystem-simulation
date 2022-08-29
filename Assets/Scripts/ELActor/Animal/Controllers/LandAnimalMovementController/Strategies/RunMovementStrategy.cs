using UnityEngine;
using UnityEngine.AI;
public class RunMovementStrategy : LandAnimalMovementStrategy
{
    public override bool execute(ILandAnimal landAnimal, NavMeshPath path)
    {
        if (landAnimal.GetLandAnimalMovementController().IsWalking())
        {
            landAnimal.GetLandAnimalAnimator().SetIsWalkingBool(false);
        }
        if (!landAnimal.GetLandAnimalMovementController().IsRunning())
        {
            landAnimal.GetLandAnimalAnimator().SetIsRunningBool(true);
        }

        if (!base.MoveTo(landAnimal, path, landAnimal.GetRunSpeed()))
        {
            // Failed to reach position
            landAnimal.GetLandAnimalMovementController().Idle();
            return false;
        }
        return true;
    }
}