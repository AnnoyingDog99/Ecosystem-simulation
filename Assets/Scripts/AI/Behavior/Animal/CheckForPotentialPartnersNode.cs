using UnityEngine;
using System.Collections.Generic;

public class CheckForPotentialPartnersNode : Node
{
    private Animal animal;

    public CheckForPotentialPartnersNode(Animal animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        /**
            Search for a potential partner
        */
        // Get all visible targets
        List<Transform> visibleTargets = animal.GetSight().GetVisibleTargets();

        // Extract all animals from targets
        List<Animal> visibleOwnKind = visibleTargets
            .FindAll((target) => target.gameObject.GetComponent<Animal>() != null && target.tag == this.animal.tag)
            .ConvertAll((target) => target.gameObject.GetComponent<Animal>());

        AnimalMemory memory = (animal.GetMemory() as AnimalMemory);
        foreach (Animal visibleAnimal in visibleOwnKind)
        {
            memory.AddOwnKindMemory(visibleAnimal);
        }

        if (memory.GetOwnKindInMemory().Find((ownKind) => ownKind.IsPotentialPartner(this.animal)) != null)
        {
            return NodeStates.SUCCESS;
        }

        return NodeStates.FAILURE;
    }
}
