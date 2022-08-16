using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HerbivoreWanderOnLandNode : WanderOnLandNode
{
    private IHerbivoreLandAnimal herbivoreLandAnimal;

    public HerbivoreWanderOnLandNode(IHerbivoreLandAnimal herbivoreLandAnimal) : base(herbivoreLandAnimal)
    {
        this.herbivoreLandAnimal = herbivoreLandAnimal;
    }

    public override NodeStates Evaluate()
    {
        NodeStates baseNodeState = base.Evaluate();
        if (baseNodeState != NodeStates.SUCCESS) return baseNodeState;

        /*
            Get possible POIs
        */
        IHerbivoreMemory memory = this.herbivoreLandAnimal.GetHerbivoreMemory();
        List<Transform> visibleTargets = herbivoreLandAnimal.GetSight().GetVisibleTargets();
        List<ELActor> visibleActors = visibleTargets.FindAll((target) => target.gameObject.GetComponent<ELActor>() != null).ConvertAll((target) => target.gameObject.GetComponent<ELActor>());

        List<Animal> visibleOwnKind = visibleActors.FindAll((actor) => actor.tag == this.herbivoreLandAnimal.GetTag()).ConvertAll((actor) => actor as Animal);
        List<Plant> visiblePlants = visibleTargets
            .FindAll((target) => target.gameObject.GetComponent<Plant>() != null && this.herbivoreLandAnimal.GetPlantTags().Contains(target.tag))
            .ConvertAll((target) => target.gameObject.GetComponent<Plant>());
        foreach (Animal ownKind in visibleOwnKind)
        {
            memory.AddOwnKindMemory(ownKind);
        }
        foreach (Plant plant in visiblePlants)
        {
            memory.AddPlantMemory(plant);
        }

        /*
            Prioritize POIs
        */
        NavMeshPath pathToPOI = new NavMeshPath();

        List<Animal> ownKindMemory = herbivoreLandAnimal.GetHerbivoreMemory().GetOwnKindInMemory();

        // Prioritize partners
        List<Vector3> partnerPOIs = new List<Vector3>();
        foreach (Animal partner in this.herbivoreLandAnimal.GetPartners())
        {
            foreach (Animal ownKind in ownKindMemory)
            {
                if (partner.GetID() == ownKind.GetID())
                {
                    partnerPOIs.Add(ownKind.GetPosition());
                }
            }
        }
        if (partnerPOIs.Count > 0)
        {
            this.POI = GetClosestPOI(partnerPOIs, out pathToPOI);
            if (this.POI.HasValue)
            {
                this.herbivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToPOI);
                return NodeStates.SUCCESS;
            }
        }

        // Prioritize parents if not yet mature
        if (this.herbivoreLandAnimal.GetAnimalAgeController().GetAgeTracker().GetCurrent() < this.herbivoreLandAnimal.GetAnimalAgeController().GetAgeTracker().GetMatureAge())
        {
            List<Vector3> parentPOIs = new List<Vector3>();
            if (Director.Instance.ActorExists(this.herbivoreLandAnimal.GetMother()))
            {
                foreach (Animal ownKind in ownKindMemory)
                {
                    if (ownKind.GetID() != this.herbivoreLandAnimal.GetMother().GetID())
                    {
                        continue;
                    }
                    partnerPOIs.Add(ownKind.GetPosition());
                    break;
                }
            }
            if (Director.Instance.ActorExists(this.herbivoreLandAnimal.GetFather()))
            {
                foreach (Animal ownKind in ownKindMemory)
                {
                    if (ownKind.GetID() != this.herbivoreLandAnimal.GetFather().GetID())
                    {
                        continue;
                    }
                    partnerPOIs.Add(ownKind.GetPosition());
                    break;
                }
            }
            this.POI = GetClosestPOI(parentPOIs, out pathToPOI);
        }

        if (this.POI.HasValue)
        {
            this.herbivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToPOI);
            return NodeStates.SUCCESS;
        }

        // Prioritize own kind (TODO: Seperate to group animal behaviour)
        List<Vector3> ownKindPOIs = ownKindMemory.ConvertAll((herbivoreLandAnimal) => (herbivoreLandAnimal.GetPosition() + herbivoreLandAnimal.GetMaxScale()));
        this.POI = GetClosestPOI(ownKindPOIs, out pathToPOI);

        if (this.POI.HasValue)
        {
            this.herbivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToPOI);
            return NodeStates.SUCCESS;
        }

        // Prioritize Food
        List<Vector3> foodPOIs = new List<Vector3>();
        foodPOIs.AddRange(memory.GetPlantsInMemory().ConvertAll((actor) => actor.GetPosition()));
        this.POI = GetClosestPOI(foodPOIs, out pathToPOI);

        if (this.POI.HasValue)
        {
            this.herbivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToPOI);
            return NodeStates.SUCCESS;
        }

        // Lastly go to random position
        this.POI = this.GetRandomPosition(5f, out pathToPOI);
        if (!this.POI.HasValue)
        {
            this.POI = null;
            herbivoreLandAnimal.GetLandAnimalMovementController().Idle();
            return NodeStates.FAILURE;
        }

        herbivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToPOI);

        return NodeStates.SUCCESS;
    }
}