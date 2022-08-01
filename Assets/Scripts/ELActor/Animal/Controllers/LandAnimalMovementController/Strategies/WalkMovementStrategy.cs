using UnityEngine;
using UnityEngine.AI;
public class WalkMovementStrategy : LandAnimalMovementStrategy
{
    public override bool execute(ILandAnimal landAnimal, NavMeshPath path)
    {
        if (!landAnimal.GetLandAnimalMovementController().IsWalking())
        {
            landAnimal.GetLandAnimalAnimator().SetIsWalkingBool(true);
        }
        if (landAnimal.GetLandAnimalMovementController().IsRunning())
        {
            landAnimal.GetLandAnimalAnimator().SetIsRunningBool(false);
        }
        if (!base.MoveTo(landAnimal, path, landAnimal.GetWalkSpeed()))
        {
            // Failed to reach position
            landAnimal.GetLandAnimalMovementController().Idle();
            return false;
        }
        return true;
    }
}