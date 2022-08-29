using UnityEngine;
using UnityEngine.AI;
public interface IELActorMovementState
{
    public bool WarpTo(Vector3 position, float maxDistance, int areaMask);
}