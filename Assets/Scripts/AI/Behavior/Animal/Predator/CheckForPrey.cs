using System;
using System.Collections.Generic;
using UnityEngine;
public class CheckForPrey : Node
{
    private Carnivore animal;

    public CheckForPrey(Carnivore animal)
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

        // Extract all actors from targets
        List<ELActor> visibleActors = visibleTargets.ConvertAll((target) => target.gameObject.GetComponent<ELActor>());

        CarnivoreMemory memory = (animal.GetMemory() as CarnivoreMemory);

        // Check for each actor whether they are prey
        // Add them to the memory if they are prey
        foreach (Animal visibleActor in visibleActors)
        {
            if (!animal.GetPreyTags().Contains(visibleActor.tag)) continue;
            memory.AddPreyMemory(new Tuple<Animal, Vector3>(visibleActor, visibleActor.GetPosition()));
        }

        return memory.GetPreyInMemory().Count > 0 ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}
