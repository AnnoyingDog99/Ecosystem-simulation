using UnityEngine;
using UnityEngine.AI;

public interface IELActorMovementStrategy
{
    public bool execute(ELActor actor, Vector3 position, float maxDistance, int areaMask);
}