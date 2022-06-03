using UnityEngine;
using System.Collections.Generic;
using System;

public class FleeFromPredatorsNode : Node
{
    private Animal animal;

    public FleeFromPredatorsNode(Animal animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        /**
            Run opposite direction of predator
        */
        AnimalMemory memory = (animal.GetMemory() as AnimalMemory);

        List<ELActor> nearbyPredators = memory.GetPredatorsInMemory();

        float minDistance = -1f;
        float maxDistance = 0f;
        /**
            Add nearby predators into the equation
        */
        List<Tuple<Vector3, float>> positions = new List<Tuple<Vector3, float>>();
        foreach (ELActor predator in nearbyPredators)
        {
            float distance = Vector3.Distance(animal.GetPosition(), predator.GetPosition());
            if (minDistance == -1f || distance < minDistance) minDistance = distance;
            if (distance > maxDistance) maxDistance = distance;
            positions.Add(new Tuple<Vector3, float>(predator.GetPosition(), distance));
        }
        
        /**
            Add possible obstacles into the equation
        */
        foreach (Transform obstacle in animal.GetSight().GetVisibleObstacles())
        {
            if (obstacle.tag == "Surface") continue;
            memory.AddObstacleMemory(obstacle.gameObject.GetComponent<Collider>());
        }
        float obstaclePreventionWeight = 0.5f;
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

        Vector3 direction = animal.GetPosition() - averagePosition;

        Vector3 newPosition = animal.GetPosition() + direction * 1.5f;

        animal.RunTo(newPosition);

        return NodeStates.SUCCESS;
    }
}