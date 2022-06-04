using UnityEngine;
using System.Collections.Generic;
using System;

public class ChasePreyNode : Node
{
    private Carnivore animal;
    private float maxPreyDistance;

    public ChasePreyNode(Carnivore animal, float maxPreyDistance)
    {
        this.animal = animal;
        this.maxPreyDistance = maxPreyDistance;
    }

    public override NodeStates Evaluate()
    {
        /**
            Run to prey
        */
        CarnivoreMemory memory = (animal.GetMemory() as CarnivoreMemory);

        List<Tuple<Animal, Vector3>> nearbyPrey = memory.GetPreyInMemory();
        if (nearbyPrey.Count <= 0)
        {
            return NodeStates.FAILURE;
        }

        float minDistance = -1f;
        Vector3 closestPreyPosition = Vector3.zero;

        /**
            Get closest prey
        */
        foreach (Tuple<Animal, Vector3> prey in nearbyPrey)
        {
            float distance = Vector3.Distance(animal.GetPosition(), prey.Item2);
            if (minDistance == -1f || distance < minDistance)
            {
                minDistance = distance;
                closestPreyPosition = prey.Item2;
            }
        }

        // Prey is too far away to consider chasing
        if (minDistance > maxPreyDistance)
        {
            return NodeStates.FAILURE;
        }

        animal.RunTo(closestPreyPosition);

        return NodeStates.SUCCESS;
    }
}