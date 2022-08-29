using UnityEngine;
using System.Collections.Generic;

public class CheckForPotentialPartnersNode : Node
{
    private IFertileAnimal animal;

    public CheckForPotentialPartnersNode(IFertileAnimal animal)
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
            .FindAll((target) => target.gameObject.GetComponent<Animal>() != null && target.gameObject.GetComponent<Animal>().GetTag() == this.animal.GetTag())
            .ConvertAll((target) => target.gameObject.GetComponent<Animal>());

        AnimalMemory memory = animal.GetAnimalMemory();
        foreach (Animal visibleAnimal in visibleOwnKind)
        {
            memory.AddOwnKindMemory(visibleAnimal);
        }

        if (memory.GetOwnKindInMemory().Find((ownKind) =>
            ownKind is IFertileAnimal && this.animal.GetAnimalFertilityController().IsPotentialPartner(ownKind as IFertileAnimal)) != null)
        {
            return NodeStates.SUCCESS;
        }

        return NodeStates.FAILURE;
    }
}