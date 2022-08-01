using UnityEngine;
using UnityEngine.AI;

public interface IELActorMovementStrategy
{
    public bool execute(IELActor actor, Vector3 position, float maxDistance, int areaMask);
}