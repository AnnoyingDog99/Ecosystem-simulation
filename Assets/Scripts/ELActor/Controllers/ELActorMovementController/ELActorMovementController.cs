using UnityEngine;
using UnityEngine.AI;

public class ELActorMovementController : MonoBehaviour
{
    private IELActor actor;
    private IELActorMovementState movementState;

    private void Start()
    {
        this.actor = GetComponentInParent<IELActor>();
        this.movementState = new DefaultELActorMovementState(this.actor);
    }

    public bool WarpTo(Vector3 position, float maxDistance = 50, int areaMask = 1)
    {
        return this.movementState.WarpTo(position, maxDistance, areaMask);
    }
}