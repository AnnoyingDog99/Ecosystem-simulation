using UnityEngine;
using UnityEngine.AI;

public interface ILandAnimalMovementStrategy
{
    public bool execute(ILandAnimal landAnimal, NavMeshPath path);
}