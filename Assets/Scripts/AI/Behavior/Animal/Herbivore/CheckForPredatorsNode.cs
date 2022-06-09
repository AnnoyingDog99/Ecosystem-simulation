using System.Collections.Generic;
using UnityEngine;
public class CheckForPredatorsNode : Node
{
    private Animal animal;

    public CheckForPredatorsNode(Animal animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        /**
            Evaluate whether a Predator can be seen
        */
        // Get all visible targets
        List<Transform> visibleTargets = animal.GetSight().GetVisibleTargets();

        // Extract all animals from targets
        List<Animal> visibleAnimals = visibleTargets
            .FindAll((target) => target.gameObject.GetComponent<Animal>() != null)
            .ConvertAll((target) => target.gameObject.GetComponent<Animal>());

        AnimalMemory memory = (animal.GetMemory() as AnimalMemory);

        // Check for each actor whether they are a predator
        // Add them to the memory if they are a predator
        foreach (Animal visibleAnimal in visibleAnimals)
        {
            if (!animal.GetPredatorTags().Contains(visibleAnimal.tag)) continue;
            memory.AddPredatorMemory(visibleAnimal);
        }

        return memory.GetPredatorsInMemory().Count > 0 ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}
