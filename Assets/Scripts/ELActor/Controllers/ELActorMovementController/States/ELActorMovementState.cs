using UnityEngine;
using UnityEngine.AI;

public abstract class ELActorMovementState
{
    protected ELActorMovementContext context;
    protected IELActor actor;

    public ELActorMovementState(IELActor actor)
    {
        this.actor = actor;
        this.context = new ELActorMovementContext();
    }
}