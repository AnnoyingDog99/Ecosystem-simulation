using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MateWithPartnerOnLandNode : Node
{
    private IFertileLandAnimal animal;
    private float maxPartnerDistance;

    private float matingTime = 2.5f;
    private float matingTimer;

    public MateWithPartnerOnLandNode(IFertileLandAnimal animal, float maxPartnerDistance)
    {
        this.animal = animal;
        this.maxPartnerDistance = maxPartnerDistance;
        this.matingTimer = this.matingTime;
    }

    public override NodeStates Evaluate()
    {
        AnimalMemory memory = this.animal.GetAnimalMemory();

        List<IFertileAnimal> potentialPartners = new List<IFertileAnimal>();
        foreach (Animal potentialPartner in memory.GetOwnKindInMemory())
        {
            if (!(potentialPartner is IFertileAnimal)) continue;
            if (potentialPartner.GetAnimalFertilityController().IsPotentialPartner(this.animal))
            {
                potentialPartners.Add(potentialPartner as IFertileAnimal);
            }
        }

        if (potentialPartners.Count < 1)
        {
            this.matingTimer = this.matingTime;
            return NodeStates.FAILURE;
        }

        /**
            Get closest partner
        */
        float minDistance = -1f;
        IFertileAnimal closestPotentialPartner = null;
        NavMeshPath pathToClosestPotentialPartner = new NavMeshPath();
        foreach (IFertileAnimal potentialPartner in potentialPartners)
        {
            float distance = Vector3.Distance(this.animal.GetPosition(), potentialPartner.GetPosition());
            // Discard plant if it is too far away to consider foraging
            if (distance > this.maxPartnerDistance) continue;
            if (minDistance == -1f || distance < minDistance)
            {
                minDistance = distance;
                if (this.animal.GetLandAnimalMovementController().CalculatePath(potentialPartner.GetPosition(), out pathToClosestPotentialPartner))
                {
                    closestPotentialPartner = potentialPartner;
                }
            }
        }

        if (closestPotentialPartner == null)
        {
            this.matingTimer = this.matingTime;
            return NodeStates.FAILURE;
        }

        foreach (ELActor actor in this.animal.GetCollidingActors())
        {
            if (closestPotentialPartner.GetID() != actor.GetID())
            {
                continue;
            }

            this.animal.GetLandAnimalMovementController().Idle();

            // Attempt Mating
            if ((this.matingTimer -= Time.deltaTime) < 0)
            {
                if (this.animal.GetAnimalFertilityController().Breed(closestPotentialPartner))
                {
                    return NodeStates.SUCCESS;
                }
                return NodeStates.FAILURE;
            }
            return NodeStates.RUNNING;
        }
        this.matingTimer = this.matingTime;

        this.animal.GetLandAnimalMovementController().WalkTo(pathToClosestPotentialPartner);

        return NodeStates.RUNNING;
    }
}