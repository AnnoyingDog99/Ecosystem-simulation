using UnityEngine;
using System.Collections.Generic;
using System;

public class FleeFromPredatorsNode : Node
{
    private Animal animal;
    private float maxPredatorDistance;

    public FleeFromPredatorsNode(Animal animal, float maxPredatorDistance)
    {
        this.animal = animal;
        this.maxPredatorDistance = maxPredatorDistance;
    }

    public override NodeStates Evaluate()
    {
        /**
            Run opposite direction of predator
        */
        AnimalMemory memory = (animal.GetMemory() as AnimalMemory);

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
            float distance = Vector3.Distance(animal.GetPosition(), predator.GetPosition());
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
        foreach (Transform obstacle in animal.GetSight().GetVisibleObstacles())
        {
            if (obstacle.tag == "Surface") continue;
            memory.AddObstacleMemory(obstacle.gameObject.GetComponent<Collider>());
        }
        float obstaclePreventionWeight = 0.15f;
        List<Collider> obstacles = memory.GetObstaclesInMemory();
        foreach (Collider obstacle in obstacles)
        {
            float distance = Vector3.Distance(animal.GetPosition(), obstacle.transform.position);
            if (minDistance == -1 || distance < minDistance) minDistance = distance;
            if (distance > maxDistance) maxDistance = distance;
            positions.Add(new Tuple<Vector3, float>(obstacle.transform.position * obstaclePreventionWeight, distance));
        }

        Vector3 averagePosition = Vector3.zero;
        foreach (Tuple<Vector3, float> position in positions)
        {
            float normalizedDistance = ((1 + (maxDistance - position.Item2)) / (1 + (maxDistance - minDistance)));
            averagePosition += (position.Item1 * normalizedDistance);
        }
        averagePosition = averagePosition / nearbyPredators.Count;

        Vector3 direction = (animal.GetPosition() - averagePosition).normalized;

        Vector3 newPosition = animal.GetPosition() + (direction * 0.15f);

        if (!animal.RunTo(newPosition))
        {
            animal.Idle();
            return NodeStates.FAILURE;
        }

        return NodeStates.SUCCESS;
    }
}