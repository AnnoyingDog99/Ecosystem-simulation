
using System;
using System.Collections.Generic;
using UnityEngine;
public class CheckForPreyNode : Node
{
    private ICarnivore carnivore;

    public CheckForPreyNode(ICarnivore carnivore)
    {
        this.carnivore = carnivore;
    }

    public override NodeStates Evaluate()
    {
        /**
            Evaluate whether a Prey can be seen
        */
        // Get all visible targets
        List<Transform> visibleTargets = carnivore.GetSight().GetVisibleTargets();

        // Extract all carnivores from targets
        List<Animal> visibleAnimals = visibleTargets
            .FindAll((target) => target.gameObject.GetComponent<Animal>() != null)
            .ConvertAll((target) => target.gameObject.GetComponent<Animal>());

        ICarnivoreMemory memory = carnivore.GetCarnivoreMemory();

        // Check for each actor whether they are prey
        // Add them to the memory if they are prey
        foreach (Animal visibleAnimal in visibleAnimals)
        {
            if (!carnivore.GetPreyTags().Contains(visibleAnimal.GetTag())) continue;
            memory.AddPreyMemory(new Tuple<Animal, Vector3>(visibleAnimal, visibleAnimal.GetPosition()));
        }

        return memory.GetPreyInMemory().Count > 0 ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}