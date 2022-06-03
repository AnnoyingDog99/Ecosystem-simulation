using UnityEngine;
using System.Collections.Generic;
using System;

public class ChasePreyNode : Node
{
    private Carnivore animal;

    public ChasePreyNode(Carnivore animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        /**
            Run to prey
        */
        CarnivoreMemory memory = (animal.GetMemory() as CarnivoreMemory);

        List<Tuple<Animal, Vector3>> nearbyPrey = memory.GetPreyInMemory();

        float minDistance = -1f;
        Vector3 closestPreyPosition = Vector3.zero;

        if (nearbyPrey.Count <= 0)
        {
            return NodeStates.FAILURE;
        }

        /**
            Get closest prey
        */
        foreach (Tuple<Animal, Vector3> prey in nearbyPrey)
        {
            float distance = Vector3.Distance(animal.GetPosition(), prey.Item2);
            if (minDistance == -1f || distance < minDistance)
            {
                closestPreyPosition = prey.Item2;
            }
        }
        animal.RunTo(closestPreyPosition);

        return NodeStates.SUCCESS;
    }
}