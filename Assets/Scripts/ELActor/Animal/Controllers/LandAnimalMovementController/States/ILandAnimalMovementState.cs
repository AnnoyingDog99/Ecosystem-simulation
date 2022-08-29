using UnityEngine;
using UnityEngine.AI;
public interface ILandAnimalMovementState
{
    public bool WalkTo(NavMeshPath path);
    public bool RunTo(NavMeshPath path);
}