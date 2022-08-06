using UnityEngine;
using UnityEngine.AI;

public class ELActorMovementController : Controller
{
    [SerializeField] private StaminaTracker staminaTracker;

    private IELActor actor;
    private IELActorMovementState actorMovementState;

    protected override void Start()
    {
        this.actor = GetComponentInParent<IELActor>();
        this.actorMovementState = new DefaultELActorMovementState(this.actor);
    }

    public bool WarpTo(Vector3 position, float maxDistance = 50, int areaMask = 1)
    {
        return this.actorMovementState.WarpTo(position, maxDistance, areaMask);
    }

    public StaminaTracker GetStaminaTracker()
    {
        return this.staminaTracker;
    }
}