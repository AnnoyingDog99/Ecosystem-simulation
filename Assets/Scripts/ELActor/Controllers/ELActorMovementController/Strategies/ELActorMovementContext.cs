using UnityEngine;
using UnityEngine.AI;

public class ELActorMovementContext
{
    private IELActorMovementStrategy strategy = null;

    public void SetStrategy(IELActorMovementStrategy strategy)
    {
        this.strategy = strategy;
    }

    public bool ExecuteStrategy(IELActor actor, Vector3 position, float maxDistance, int areaMask)
    {
        if (this.strategy == null) return false;
        return this.strategy.execute(actor, position, maxDistance, areaMask);
    }
}