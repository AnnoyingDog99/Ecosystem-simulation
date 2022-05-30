using System.Collections.Generic;
using UnityEngine;
public class CheckForPredators : Node
{
    private Animal animal;

    public CheckForPredators(Animal animal)
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

        // Extract all actors from targets
        List<ELActor> visibleActors = visibleTargets.ConvertAll((target) => target.gameObject.GetComponent<ELActor>());

        AnimalMemory memory = (animal.GetMemory() as AnimalMemory);

        // Check for each actor whether they are a predator
        // Add them to the memory if they are a predator
        foreach (Animal visibleActor in visibleActors) 
        {
            if (!animal.GetPredatorTags().Contains(visibleActor.tag)) continue;
            memory.AddPredatorMemory(visibleActor);
        }

        return memory.GetPredatorsInMemory().Count > 0 ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}
