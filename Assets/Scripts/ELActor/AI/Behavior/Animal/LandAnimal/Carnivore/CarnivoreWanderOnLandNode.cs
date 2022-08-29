using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CarnivoreWanderOnLandNode : WanderOnLandNode
{
    private ICarnivoreLandAnimal carnivoreLandAnimal;

    Range randomizeTimeRange = new Range(5, 10);
    float randomizeTimer = 0f;
    Range randomizeCooldownTimeRange = new Range(5, 10);
    float randomizeCooldownTimer = 0f;


    Range idleTimeRange = new Range(2, 10);
    float idleTimer = 0f;
    Range idleCooldownTimeRange = new Range(5, 10);
    float idleCooldownTimer = 0f;

    public CarnivoreWanderOnLandNode(ICarnivoreLandAnimal carnivoreLandAnimal) : base(carnivoreLandAnimal)
    {
        this.carnivoreLandAnimal = carnivoreLandAnimal;
    }

    public override NodeStates Evaluate()
    {
        NodeStates baseNodeState = base.Evaluate();
        if (baseNodeState != NodeStates.SUCCESS) return baseNodeState;

        /*
            Get possible POIs
        */
        ICarnivoreMemory memory = this.carnivoreLandAnimal.GetCarnivoreMemory();
        List<Transform> visibleTargets = carnivoreLandAnimal.GetSight().GetVisibleTargets();
        List<ELActor> visibleActors = visibleTargets.FindAll((target) => target.gameObject.GetComponent<ELActor>() != null).ConvertAll((target) => target.gameObject.GetComponent<ELActor>());

        List<Animal> visibleOwnKind = visibleActors.FindAll((actor) => actor.tag == this.carnivoreLandAnimal.GetTag()).ConvertAll((actor) => actor as Animal);
        List<Animal> visiblePrey = visibleTargets
            .FindAll((target) => target.gameObject.GetComponent<Animal>() != null && this.carnivoreLandAnimal.GetPreyTags().Contains(target.tag))
            .ConvertAll((target) => target.gameObject.GetComponent<Animal>());
        foreach (Animal ownKind in visibleOwnKind)
        {
            memory.AddOwnKindMemory(ownKind);
        }
        foreach (Animal prey in visiblePrey)
        {
            memory.AddPreyMemory(new Tuple<Animal, Vector3>(prey, prey.GetPosition()));
        }

        /*
            Prioritize POIs
        */
        NavMeshPath pathToPOI = new NavMeshPath();

        List<Animal> ownKindMemory = carnivoreLandAnimal.GetCarnivoreMemory().GetOwnKindInMemory();

        // Prioritize partners
        List<Vector3> partnerPOIs = new List<Vector3>();
        foreach (Animal partner in this.carnivoreLandAnimal.GetPartners())
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
                this.carnivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToPOI);
                return NodeStates.SUCCESS;
            }
        }

        // Prioritize parents if not yet mature
        if (this.carnivoreLandAnimal.GetAnimalAgeController().GetAgeTracker().GetCurrent() < this.carnivoreLandAnimal.GetAnimalAgeController().GetAgeTracker().GetMatureAge())
        {
            if (Director.Instance.ActorExists(this.carnivoreLandAnimal.GetMother()) || Director.Instance.ActorExists(this.carnivoreLandAnimal.GetFather()))
            {
                List<Vector3> parentPOIs = ownKindMemory.FindAll((ownKind) => ownKind.GetID() == this.carnivoreLandAnimal.GetMother().GetID()
                || ownKind.GetID() == this.carnivoreLandAnimal.GetFather().GetID()).ConvertAll((parent) => carnivoreLandAnimal.GetPosition());
                this.POI = GetClosestPOI(parentPOIs, out pathToPOI);
            }
        }

        if (this.POI.HasValue)
        {
            this.carnivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToPOI);
            return NodeStates.SUCCESS;
        }

        // Prioritize own kind (TODO: Seperate to group animal behaviour)
        List<Vector3> ownKindPOIs = ownKindMemory.ConvertAll((carnivoreLandAnimal) => (carnivoreLandAnimal.GetPosition() + carnivoreLandAnimal.GetMaxScale()));
        this.POI = GetClosestPOI(ownKindPOIs, out pathToPOI);

        if (this.POI.HasValue)
        {
            this.carnivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToPOI);
            return NodeStates.SUCCESS;
        }

        // Prioritize Food
        List<Vector3> foodPOIs = new List<Vector3>();
        foodPOIs.AddRange(memory.GetPreyInMemory().ConvertAll((actorAndPosition) => actorAndPosition.Item2));
        this.POI = GetClosestPOI(foodPOIs, out pathToPOI);

        if (this.POI.HasValue)
        {
            this.carnivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToPOI);
            return NodeStates.SUCCESS;
        }

        // Lastly go to random position
        this.POI = this.GetRandomPosition(5f, out pathToPOI);
        if (!this.POI.HasValue)
        {
            this.POI = null;
            carnivoreLandAnimal.GetLandAnimalMovementController().Idle();
            return NodeStates.FAILURE;
        }

        carnivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToPOI);

        return NodeStates.SUCCESS;
    }
}