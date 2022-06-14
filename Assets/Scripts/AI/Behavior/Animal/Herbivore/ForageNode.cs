using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ForageNode : Node
{
    private Herbivore animal;
    private float maxPlantDistance;
    private float eatDelay = 2;
    private float eatDelayTimer;

    public ForageNode(Herbivore animal, float maxPlantDistance)
    {
        this.animal = animal;
        this.maxPlantDistance = maxPlantDistance;
        this.eatDelayTimer = this.eatDelay;
    }

    public override NodeStates Evaluate()
    {
        /**
            Check if there is visible food nearby
        */

        // Get all visible targets
        List<Transform> visibleTargets = animal.GetSight().GetVisibleTargets();

        // Extract all plants from targets
        List<Plant> visiblePlants = visibleTargets
            .FindAll((target) => target.gameObject.GetComponent<Plant>() != null)
            .ConvertAll((target) => target.gameObject.GetComponent<Plant>());

        HerbivoreMemory memory = (animal.GetMemory() as HerbivoreMemory);

        // Check for each actor whether they are a plant this animal eats
        // Add them to the memory if they are a plant this animal eats
        foreach (Plant visiblePlant in visiblePlants)
        {
            if (!animal.GetPlantTags().Contains(visiblePlant.tag)) continue;
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
            float distance = Vector3.Distance(this.animal.GetPosition(), plant.GetPosition());
            // Discard plant if it is too far away to consider foraging
            if (distance > this.maxPlantDistance) continue;
            if (minDistance == -1f || distance < minDistance)
            {
                minDistance = distance;
                if (this.animal.IsReachable(plant.GetPosition(), out pathToClosestPlant))
                {
                    closestPlant = plant;
                }
            }
        }

        if (closestPlant == null)
        {
            return NodeStates.FAILURE;
        }

        foreach (ELActor actor in this.animal.GetActorsBeingTouched())
        {
            if (closestPlant.GetID() != actor.GetID())
            {
                continue;
            }

            this.animal.Idle();

            // Eat Plant
            if ((this.eatDelayTimer -= Time.deltaTime) < 0)
            {
                this.animal.GetHungerBar().AddFoodPoints(closestPlant.GetEaten(this.animal.GetBiteSize()));
                this.eatDelayTimer = this.eatDelay;
            }

            return NodeStates.SUCCESS;
        }

        this.animal.WalkTo(pathToClosestPlant);

        return NodeStates.RUNNING;
    }
}