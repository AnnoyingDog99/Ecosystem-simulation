using UnityEngine;
using UnityEngine.AI;
public class WarpMovementStrategy : IELActorMovementStrategy
{
    public bool execute(IELActor actor, Vector3 position, float maxDistance, int areaMask)
    {
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(position, out closestHit, maxDistance, areaMask))
        {
            return actor.GetAgent().Warp(closestHit.position);
        }
        return false;
    }
}