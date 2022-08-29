using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WanderOnLandNode : Node
{
    private ILandAnimal landAnimal;
    protected Nullable<Vector3> POI = null;

    Range randomizeTimeRange = new Range(5, 10);
    float randomizeTimer = 0f;
    Range randomizeCooldownTimeRange = new Range(5, 10);
    float randomizeCooldownTimer = 0f;


    Range idleTimeRange = new Range(2, 10);
    float idleTimer = 0f;
    Range idleCooldownTimeRange = new Range(5, 10);
    float idleCooldownTimer = 0f;

    public WanderOnLandNode(ILandAnimal landAnimal)
    {
        this.landAnimal = landAnimal;
    }

    public override NodeStates Evaluate()
    {
        if (this.Idling())
        {
            this.POI = null;
            return NodeStates.RUNNING;
        }
        if (this.Randomizing())
        {
            this.POI = null;
            return NodeStates.RUNNING;
        }

        if (this.POI != null)
        {
            if (this.landAnimal.GetLandAnimalMovementController().HasReachedDestination())
            {
                this.POI = null;
            }
            else
            {
                return NodeStates.RUNNING;
            }
        }

        return NodeStates.SUCCESS;
    }

    protected Nullable<Vector3> GetClosestPOI(List<Vector3> positions, out NavMeshPath path)
    {
        Nullable<Vector3> closestPOI = null;
        float minDistance = -1f;
        path = new NavMeshPath();
        foreach (Vector3 position in positions)
        {
            if (!this.landAnimal.GetLandAnimalMovementController().CalculatePath(position, out path))
            {
                continue;
            }
            float distance = Vector3.Distance(this.landAnimal.GetPosition(), this.POI.GetValueOrDefault());
            if (this.POI == null || minDistance != -1f || distance < minDistance)
            {
                minDistance = distance;
                closestPOI = position;
            }
        }
        return closestPOI;
    }

    protected Nullable<Vector3> GetRandomPosition(float distance, out NavMeshPath path)
    {
        Vector3 newPosition = this.landAnimal.GetPosition() + (new Vector3(UnityEngine.Random.Range(-distance, distance), UnityEngine.Random.Range(-distance, distance), UnityEngine.Random.Range(-distance, distance)));
        return this.landAnimal.GetLandAnimalMovementController().CalculatePath(newPosition, out path) ? newPosition : null;
    }

    private bool Idling()
    {
        if ((this.idleCooldownTimer -= Time.deltaTime) <= 0)
        {
            if ((this.idleTimer -= Time.deltaTime) > 0)
            {
                this.landAnimal.GetLandAnimalMovementController().Idle();
                return true;
            }
            else
            {
                this.idleCooldownTimer = UnityEngine.Random.Range(
                    this.idleCooldownTimeRange.Start.Value,
                    this.idleCooldownTimeRange.End.Value
                );
            }
        }
        else
        {
            this.idleTimer = UnityEngine.Random.Range(this.idleTimeRange.Start.Value, this.idleTimeRange.End.Value);
        }
        return false;
    }

    private bool Randomizing()
    {
        if ((this.randomizeCooldownTimer -= Time.deltaTime) > 0)
        {
            return false;
        }
        if ((this.randomizeTimer -= Time.deltaTime) > 0)
        {
            if (this.landAnimal.GetLandAnimalMovementController().HasReachedDestination())
            {
                NavMeshPath path;
                if (this.GetRandomPosition(5f, out path).HasValue)
                {
                    this.landAnimal.GetLandAnimalMovementController().WalkTo(path);
                    return true;
                }
            }
            return true;
        }
        this.randomizeTimer = UnityEngine.Random.Range(
            this.randomizeTimeRange.Start.Value,
            this.randomizeTimeRange.End.Value
        );
        this.randomizeCooldownTimer = UnityEngine.Random.Range(
            this.randomizeCooldownTimeRange.Start.Value,
            this.randomizeCooldownTimeRange.End.Value
        );
        return false;
    }
}