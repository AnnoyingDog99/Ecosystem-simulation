using System;
using System.Collections.Generic;
using UnityEngine;
public class CheckForPreyNode : Node
{
    private Carnivore animal;

    public CheckForPreyNode(Carnivore animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        /**
            Evaluate whether a Prey can be seen
        */
        // Get all visible targets
        List<Transform> visibleTargets = animal.GetSight().GetVisibleTargets();

        // Extract all animals from targets
        List<Animal> visibleAnimals = visibleTargets
            .FindAll((target) => target.gameObject.GetComponent<Animal>() != null)
            .ConvertAll((target) => target.gameObject.GetComponent<Animal>());

        CarnivoreMemory memory = (animal.GetMemory() as CarnivoreMemory);

        // Check for each actor whether they are prey
        // Add them to the memory if they are prey
        foreach (Animal visibleAnimal in visibleAnimals)
        {
            if (!animal.GetPreyTags().Contains(visibleAnimal.tag)) continue;
            memory.AddPreyMemory(new Tuple<Animal, Vector3>(visibleAnimal, visibleAnimal.GetPosition()));
        }

        return memory.GetPreyInMemory().Count > 0 ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}
