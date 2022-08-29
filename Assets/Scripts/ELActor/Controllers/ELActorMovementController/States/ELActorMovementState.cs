using UnityEngine;
using UnityEngine.AI;

public abstract class ELActorMovementState
{
    protected ELActorMovementContext context;
    protected ELActor actor;

    public ELActorMovementState(ELActor actor)
    {
        this.actor = actor;
        this.context = new ELActorMovementContext();
    }
}