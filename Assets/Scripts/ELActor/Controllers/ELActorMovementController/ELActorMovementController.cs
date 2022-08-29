using UnityEngine;
using UnityEngine.AI;

public class ELActorMovementController : Controller
{
    private ELActor actor;
    private IELActorMovementState actorMovementState;

    protected override void Start()
    {
        this.actor = GetComponentInParent<ELActor>();
        this.actorMovementState = new DefaultELActorMovementState(this.actor);
    }

    public virtual void Idle()
    {
        this.actor.GetActorAnimator().SetIsIdleBool(true);
    }

    public virtual bool IsIdle()
    {
        return this.actor.GetActorAnimator().GetIsIdleBool();
    }

    public bool WarpTo(Vector3 position, float maxDistance = 50, int areaMask = 1)
    {
        return this.actorMovementState.WarpTo(position, maxDistance, areaMask);
    }
}