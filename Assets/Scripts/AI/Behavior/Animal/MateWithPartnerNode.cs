using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MateWithPartnerNode : Node
{
    private Animal animal;
    private float maxPartnerDistance;

    private float matingTime = 2.5f;
    private float matingTimer;

    public MateWithPartnerNode(Animal animal, float maxPartnerDistance)
    {
        this.animal = animal;
        this.maxPartnerDistance = maxPartnerDistance;
        this.matingTimer = this.matingTime;
    }

    public override NodeStates Evaluate()
    {
        AnimalMemory memory = this.animal.GetMemory() as AnimalMemory;

        List<Animal> potentialPartners = new List<Animal>();
        foreach (Animal potentialPartner in memory.GetOwnKindInMemory())
        {
            if (potentialPartner.IsPotentialPartner(this.animal))
            {
                potentialPartners.Add(potentialPartner);
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
        Animal closestPotentialPartner = null;
        NavMeshPath pathToClosestPotentialPartner = new NavMeshPath();
        foreach (Animal potentialPartner in potentialPartners)
        {
            float distance = Vector3.Distance(this.animal.GetPosition(), potentialPartner.GetPosition());
            // Discard plant if it is too far away to consider foraging
            if (distance > this.maxPartnerDistance) continue;
            if (minDistance == -1f || distance < minDistance)
            {
                minDistance = distance;
                if (this.animal.IsReachable(potentialPartner.GetPosition(), out pathToClosestPotentialPartner))
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

        foreach (ELActor actor in this.animal.GetActorsBeingTouched())
        {
            if (closestPotentialPartner.GetID() != actor.GetID())
            {
                continue;
            }

            this.animal.Idle();

            // Attempt Mating
            if ((this.matingTimer -= Time.deltaTime) < 0)
            {
                if (!this.animal.SendMateRequest(closestPotentialPartner))
                {
                    return NodeStates.FAILURE;
                }
                return NodeStates.SUCCESS;
            }
            return NodeStates.RUNNING;
        }
        this.matingTimer = this.matingTime;

        this.animal.WalkTo(pathToClosestPotentialPartner);

        return NodeStates.RUNNING;
    }
}
