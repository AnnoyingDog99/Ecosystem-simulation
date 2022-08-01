using UnityEngine;
using UnityEngine.AI;

public class DefaultELActorMovementState : ELActorMovementState, IELActorMovementState
{
    public DefaultELActorMovementState(IELActor actor) : base(actor)
    {
    }

    public bool WarpTo(Vector3 position, float maxDistance, int areaMask)
    {
        this.context.SetStrategy(new WarpMovementStrategy());
        return context.ExecuteStrategy(this.actor, position, maxDistance, areaMask);
    }
}