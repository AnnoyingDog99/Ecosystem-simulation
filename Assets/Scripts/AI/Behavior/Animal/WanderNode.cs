using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WanderNode : Node
{
    private Animal animal;
    Vector3 POI = Vector3.zero;

    // Go into random directions for a given time to prevent animals staying at one spot for too long
    float randomizeTimer;
    float randomizeTime = 5f;
    float randomizeCooldownTimer;
    float randomizeCooldownTime = 10f;
    float randomizeNewPositionTimer;
    float randomizeNewPositionTime = 2f;

    public WanderNode(Animal animal)
    {
        this.animal = animal;
        this.randomizeTimer = 0f;
        this.randomizeCooldownTimer = this.randomizeCooldownTime;
    }

    public override NodeStates Evaluate()
    {
        // Go into random direction for some time
        if ((this.randomizeCooldownTimer -= Time.deltaTime) < 0 && (this.randomizeTimer -= Time.deltaTime) > 0)
        {
            if ((this.randomizeNewPositionTimer -= Time.deltaTime) < 0 || this.animal.ReachedDestination())
            {
                this.randomizeNewPositionTimer = this.randomizeNewPositionTime;
                if (this.GoToRandomPosition())
                {
                    return NodeStates.SUCCESS;
                }
            }
        }
        if (this.randomizeCooldownTimer > 0)
        {
            this.randomizeTimer = this.randomizeTime;
        }
        if (this.randomizeTimer < 0)
        {
            this.randomizeCooldownTimer = this.randomizeCooldownTime;
        }

        if (!animal.ReachedDestination())
        {
            return NodeStates.RUNNING;
        }

        /*
            Get possible POIs
        */
        AnimalMemory memory = this.animal.GetMemory() as AnimalMemory;
        List<Transform> visibleTargets = animal.GetSight().GetVisibleTargets();
        List<ELActor> visibleActors = visibleTargets.FindAll((target) => target.gameObject.GetComponent<ELActor>() != null).ConvertAll((target) => target.gameObject.GetComponent<ELActor>());
        List<Animal> visibleOwnKind = visibleActors.FindAll((actor) => actor.tag == this.animal.tag).ConvertAll((actor) => actor as Animal);
        List<ELActor> visibleFood = new List<ELActor>();
        foreach (Animal ownKind in visibleOwnKind)
        {
            memory.AddOwnKindMemory(ownKind);
        }
        if (this.animal is Herbivore || this.animal is Omnivore)
        {
            visibleFood.AddRange(visibleActors.FindAll((actor) => (this.animal as Herbivore).GetPlantTags().Contains(actor.tag)));
            foreach (ELActor plant in visibleFood)
            {
                (memory as HerbivoreMemory).AddPlantMemory(plant as Plant);
            }
        }
        if (this.animal is Carnivore || this.animal is Omnivore)
        {
            visibleFood.AddRange(visibleActors.FindAll((actor) => (this.animal as Carnivore).GetPreyTags().Contains(actor.tag)));
            foreach (ELActor prey in visibleFood)
            {
                (memory as CarnivoreMemory).AddPreyMemory(new Tuple<Animal, Vector3>(prey as Animal, prey.GetPosition()));
            }
        }

        /*
            Prioritize POIs
        */
        NavMeshPath pathToPOI = new NavMeshPath();

        // Prioritize own kind (TODO: Seperate to group animal behaviour)
        List<Vector3> ownKindPOIs = animal.GetMemory().GetOwnKindInMemory().ConvertAll((animal) => (animal.GetPosition() + (animal.GetScale() * 2f)));
        this.POI = GetClosestPOI(ownKindPOIs, out pathToPOI);
        if (this.POI != Vector3.zero)
        {
            this.animal.WalkTo(pathToPOI);
            return NodeStates.SUCCESS;
        }

        // Prioritize Food
        List<Vector3> foodPOIs = new List<Vector3>();
        if (this.animal is Herbivore || this.animal is Omnivore)
        {
            foodPOIs.AddRange((memory as HerbivoreMemory).GetPlantsInMemory().ConvertAll((actor) => actor.GetPosition()));
        }
        if (this.animal is Carnivore || this.animal is Omnivore)
        {
            foodPOIs.AddRange((memory as CarnivoreMemory).GetPreyInMemory().ConvertAll((actorAndPosition) => actorAndPosition.Item2));
        }
        this.POI = GetClosestPOI(foodPOIs, out pathToPOI);
        if (this.POI != Vector3.zero)
        {
            this.animal.WalkTo(pathToPOI);
            return NodeStates.SUCCESS;
        }

        // Lastly go to random position
        if (!this.GoToRandomPosition())
        {
            animal.Idle();
            return NodeStates.FAILURE;
        }

        return NodeStates.SUCCESS;
    }

    private Vector3 GetClosestPOI(List<Vector3> positions, out NavMeshPath path)
    {
        Vector3 closestPOI = Vector3.zero;
        float minDistance = -1f;
        path = new NavMeshPath();
        foreach (Vector3 position in positions)
        {
            if (!this.animal.IsReachable(position, out path))
            {
                continue;
            }
            float distance = Vector3.Distance(this.animal.GetPosition(), POI);
            if (this.POI == Vector3.zero || minDistance != -1f || distance < minDistance)
            {
                minDistance = distance;
                closestPOI = position;
            }
        }
        return closestPOI;
    }

    private bool GoToRandomPosition()
    {
        float distance = 15f;
        Vector3 newPosition = animal.GetPosition() + (new Vector3(UnityEngine.Random.Range(-distance, distance), UnityEngine.Random.Range(-distance, distance), UnityEngine.Random.Range(-distance, distance)));
        return animal.WalkTo(newPosition);
    }
}
