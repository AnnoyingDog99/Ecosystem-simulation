using UnityEngine;
using System.Collections.Generic;
using System;

public class FleeFromPredatorsOnLandNode : Node
{
    private ILandAnimal landAnimal;
    private float maxPredatorDistance;
    float obstaclePreventionWeight;

    public FleeFromPredatorsOnLandNode(ILandAnimal landAnimal, float maxPredatorDistance, float obstaclePreventionWeight)
    {
        this.landAnimal = landAnimal;
        this.maxPredatorDistance = maxPredatorDistance;
        this.obstaclePreventionWeight = obstaclePreventionWeight;
    }

    public override NodeStates Evaluate()
    {
        /**
            Run opposite direction of predator
        */
        AnimalMemory memory = (landAnimal.GetAnimalMemory() as AnimalMemory);

        List<Animal> nearbyPredators = memory.GetPredatorsInMemory();
        if (nearbyPredators.Count <= 0)
        {
            return NodeStates.SUCCESS;
        }

        float minDistance = -1f;
        float maxDistance = -1f;

        /**
            Add nearby predators into the equation
        */
        List<Tuple<Vector3, float>> positions = new List<Tuple<Vector3, float>>();
        foreach (ELActor predator in nearbyPredators)
        {
            float distance = Vector3.Distance(landAnimal.GetPosition(), predator.GetPosition());
            if (minDistance == -1f || distance < minDistance) minDistance = distance;
            if (maxDistance == -1f || distance > maxDistance) maxDistance = distance;
            positions.Add(new Tuple<Vector3, float>(predator.GetPosition(), distance));
        }

        // Predator is too far away to consider a threat
        if (minDistance > maxPredatorDistance)
        {
            return NodeStates.FAILURE;
        }

        /**
            Add possible obstacles into the equation
        */
        foreach (Sight.ObstacleLocation obstacle in landAnimal.GetSight().GetVisibleObstacles())
        {
            memory.AddObstacleMemory(obstacle);
        }
        List<Sight.ObstacleLocation> obstacles = memory.GetObstaclesInMemory();
        foreach (Sight.ObstacleLocation obstacle in obstacles)
        {
            float distance = Vector3.Distance(landAnimal.GetPosition(), obstacle.position);
            if (minDistance == -1 || distance < minDistance) minDistance = distance;
            if (distance > maxDistance) maxDistance = distance;
            positions.Add(new Tuple<Vector3, float>(obstacle.position * obstaclePreventionWeight, distance));
        }

        Vector3 averagePosition = Vector3.zero;
        foreach (Tuple<Vector3, float> position in positions)
        {
            float normalizedDistance = ((1 + (maxDistance - position.Item2)) / (1 + (maxDistance - minDistance)));
            averagePosition += (position.Item1 * normalizedDistance);
        }
        averagePosition = averagePosition / nearbyPredators.Count;

        Vector3 direction = (landAnimal.GetPosition() - averagePosition).normalized;

        Vector3 newPosition = landAnimal.GetPosition() + (direction * 0.25f);

        if (!landAnimal.GetLandAnimalMovementController().RunTo(newPosition))
        {
            landAnimal.GetLandAnimalMovementController().Idle();
            return NodeStates.FAILURE;
        }

        return NodeStates.SUCCESS;
    }
}