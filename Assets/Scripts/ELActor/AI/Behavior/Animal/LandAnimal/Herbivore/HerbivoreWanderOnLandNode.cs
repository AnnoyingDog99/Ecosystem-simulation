using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HerbivoreWanderOnLandNode : Node
{
    private IHerbivoreLandAnimal herbivoreLandAnimal;
    Nullable<Vector3> POI = null;

    Range randomizeTimeRange = new Range(5, 10);
    float randomizeTimer = 0f;
    Range randomizeCooldownTimeRange = new Range(5, 10);
    float randomizeCooldownTimer = 0f;


    Range idleTimeRange = new Range(2, 10);
    float idleTimer = 0f;
    Range idleCooldownTimeRange = new Range(5, 10);
    float idleCooldownTimer = 0f;

    public HerbivoreWanderOnLandNode(IHerbivoreLandAnimal herbivoreLandAnimal)
    {
        this.herbivoreLandAnimal = herbivoreLandAnimal;
    }

    public override NodeStates Evaluate()
    {
        if (this.Idling())
        {
            this.POI = null;
            return NodeStates.RUNNING;
        }
        if (this.Randomizing())
        {
            this.POI = null;
            return NodeStates.RUNNING;
        }

        if (this.POI != null)
        {
            if (this.herbivoreLandAnimal.GetLandAnimalMovementController().HasReachedDestination())
            {
                this.POI = null;
            }
            else
            {
                return NodeStates.RUNNING;
            }
        }

        /*
            Get possible POIs
        */
        IHerbivoreMemory memory = this.herbivoreLandAnimal.GetHerbivoreMemory();
        List<Transform> visibleTargets = herbivoreLandAnimal.GetSight().GetVisibleTargets();
        List<ELActor> visibleActors = visibleTargets.FindAll((target) => target.gameObject.GetComponent<ELActor>() != null).ConvertAll((target) => target.gameObject.GetComponent<ELActor>());

        List<Animal> visibleOwnKind = visibleActors.FindAll((actor) => actor.tag == this.herbivoreLandAnimal.GetTag()).ConvertAll((actor) => actor as Animal);
        List<Plant> visiblePlants = new List<Plant>();
        foreach (Animal ownKind in visibleOwnKind)
        {
            memory.AddOwnKindMemory(ownKind);
        }
        if (this.herbivoreLandAnimal is IHerbivore || this.herbivoreLandAnimal is IOmnivore)
        {
            visiblePlants.AddRange(visiblePlants.FindAll((actor) => this.herbivoreLandAnimal.GetPlantTags().Contains(actor.tag)));
            foreach (Plant plant in visiblePlants)
            {
                memory.AddPlantMemory(plant);
            }
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
            if (Director.Instance.ActorExists(this.herbivoreLandAnimal.GetMother()) || Director.Instance.ActorExists(this.herbivoreLandAnimal.GetFather()))
            {
                List<Vector3> parentPOIs = ownKindMemory.FindAll((ownKind) => ownKind.GetID() == this.herbivoreLandAnimal.GetMother().GetID()
                || ownKind.GetID() == this.herbivoreLandAnimal.GetFather().GetID()).ConvertAll((parent) => herbivoreLandAnimal.GetPosition());
                this.POI = GetClosestPOI(parentPOIs, out pathToPOI);
            }
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

    private Nullable<Vector3> GetClosestPOI(List<Vector3> positions, out NavMeshPath path)
    {
        Nullable<Vector3> closestPOI = null;
        float minDistance = -1f;
        path = new NavMeshPath();
        foreach (Vector3 position in positions)
        {
            if (!this.herbivoreLandAnimal.GetLandAnimalMovementController().CalculatePath(position, out path))
            {
                continue;
            }
            float distance = Vector3.Distance(this.herbivoreLandAnimal.GetPosition(), this.POI.GetValueOrDefault());
            if (this.POI == null || minDistance != -1f || distance < minDistance)
            {
                minDistance = distance;
                closestPOI = position;
            }
        }
        return closestPOI;
    }

    private Nullable<Vector3> GetRandomPosition(float distance, out NavMeshPath path)
    {
        Vector3 newPosition = this.herbivoreLandAnimal.GetPosition() + (new Vector3(UnityEngine.Random.Range(-distance, distance), UnityEngine.Random.Range(-distance, distance), UnityEngine.Random.Range(-distance, distance)));
        return this.herbivoreLandAnimal.GetLandAnimalMovementController().CalculatePath(newPosition, out path) ? newPosition : null;
    }

    private bool Idling()
    {
        if ((this.idleCooldownTimer -= Time.deltaTime) <= 0)
        {
            if ((this.idleTimer -= Time.deltaTime) > 0)
            {
                this.herbivoreLandAnimal.GetLandAnimalMovementController().Idle();
                return true;
            }
            else
            {
                this.idleCooldownTimer = UnityEngine.Random.Range(
                    this.idleCooldownTimeRange.Start.Value,
                    this.idleCooldownTimeRange.End.Value
                );
            }
        }
        else
        {
            this.idleTimer = UnityEngine.Random.Range(this.idleTimeRange.Start.Value, this.idleTimeRange.End.Value);
        }
        return false;
    }

    private bool Randomizing()
    {
        if ((this.randomizeCooldownTimer -= Time.deltaTime) > 0)
        {
            return false;
        }
        if ((this.randomizeTimer -= Time.deltaTime) > 0)
        {
            if (this.herbivoreLandAnimal.GetLandAnimalMovementController().HasReachedDestination())
            {
                NavMeshPath path;
                if (this.GetRandomPosition(5f, out path).HasValue)
                {
                    this.herbivoreLandAnimal.GetLandAnimalMovementController().WalkTo(path);
                    return true;
                }
            }
            return true;
        }
        this.randomizeTimer = UnityEngine.Random.Range(
            this.randomizeTimeRange.Start.Value,
            this.randomizeTimeRange.End.Value
        );
        this.randomizeCooldownTimer = UnityEngine.Random.Range(
            this.randomizeCooldownTimeRange.Start.Value,
            this.randomizeCooldownTimeRange.End.Value
        );
        return false;
    }
}