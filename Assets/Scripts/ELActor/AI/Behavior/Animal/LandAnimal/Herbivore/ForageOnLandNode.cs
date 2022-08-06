using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ForageNode : Node
{
    private IHerbivoreLandAnimal herbivoreLandAnimal;
    private float maxPlantDistance;
    private float eatDelay = 2;
    private float eatDelayTimer;

    public ForageNode(IHerbivoreLandAnimal herbivoreLandAnimal, float maxPlantDistance)
    {
        this.herbivoreLandAnimal = herbivoreLandAnimal;
        this.maxPlantDistance = maxPlantDistance;
        this.eatDelayTimer = this.eatDelay;
    }

    public override NodeStates Evaluate()
    {
        /**
            Check if there is visible food nearby
        */

        // Get all visible targets
        List<Transform> visibleTargets = this.herbivoreLandAnimal.GetSight().GetVisibleTargets();

        // Extract all plants from targets
        List<Plant> visiblePlants = visibleTargets
            .FindAll((target) => target.gameObject.GetComponent<Plant>() != null)
            .ConvertAll((target) => target.gameObject.GetComponent<Plant>());

        IHerbivoreMemory memory = this.herbivoreLandAnimal.GetHerbivoreMemory();

        // Check for each actor whether they are a plant this herbivoreLandAnimal eats
        // Add them to the memory if they are a plant this herbivoreLandAnimal eats
        foreach (Plant visiblePlant in visiblePlants)
        {
            if (!herbivoreLandAnimal.GetPlantTags().Contains(visiblePlant.tag)) continue;
            memory.AddPlantMemory(visiblePlant);
        }

        List<Plant> nearbyPlants = memory.GetPlantsInMemory();
        if (nearbyPlants.Count <= 0)
        {
            return NodeStates.FAILURE;
        }

        /**
            Get closest plant
        */
        float minDistance = -1f;
        Plant closestPlant = null;
        NavMeshPath pathToClosestPlant = new NavMeshPath();
        foreach (Plant plant in nearbyPlants)
        {
            float distance = Vector3.Distance(this.herbivoreLandAnimal.GetPosition(), plant.GetPosition());
            // Discard plant if it is too far away to consider foraging
            if (distance > this.maxPlantDistance) continue;
            if (minDistance == -1f || distance < minDistance)
            {
                minDistance = distance;
                if (this.herbivoreLandAnimal.GetLandAnimalMovementController().CalculatePath(plant.GetPosition(), out pathToClosestPlant))
                {
                    closestPlant = plant;
                }
            }
        }

        if (closestPlant == null)
        {
            return NodeStates.FAILURE;
        }

        foreach (ELActor actor in this.herbivoreLandAnimal.GetCollidingActors())
        {
            if (closestPlant.GetID() != actor.GetID())
            {
                continue;
            }

            this.herbivoreLandAnimal.GetLandAnimalMovementController().Idle();

            // Eat Plant
            if ((this.eatDelayTimer -= Time.deltaTime) < 0)
            {
                this.herbivoreLandAnimal.GetAnimalHungerController().Eat(closestPlant);
                this.eatDelayTimer = this.eatDelay;
            }

            return NodeStates.SUCCESS;
        }

        this.herbivoreLandAnimal.GetLandAnimalMovementController().WalkTo(pathToClosestPlant);

        return NodeStates.RUNNING;
    }
}