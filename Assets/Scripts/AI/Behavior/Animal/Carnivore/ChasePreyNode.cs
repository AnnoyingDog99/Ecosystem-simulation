using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

public class ChasePreyNode : Node
{
    private Carnivore animal;
    private float maxPreyDistance;
    private float attackDelay = 2;
    private float attackDelayTimer;
    private float eatDelay = 2;
    private float eatDelayTimer;

    public ChasePreyNode(Carnivore animal, float maxPreyDistance)
    {
        this.animal = animal;
        this.maxPreyDistance = maxPreyDistance;

        this.eatDelayTimer = this.eatDelay;
        this.attackDelayTimer = 0;
    }

    public override NodeStates Evaluate()
    {
        this.attackDelayTimer -= Time.deltaTime;
        this.eatDelayTimer -= Time.deltaTime;
        /**
            Run to prey
        */
        CarnivoreMemory memory = (this.animal.GetMemory() as CarnivoreMemory);

        List<Tuple<Animal, Vector3>> nearbyPrey = memory.GetPreyInMemory();
        if (nearbyPrey.Count <= 0)
        {
            return NodeStates.FAILURE;
        }

        /**
            Get closest prey
        */
        float minDistance = -1f;
        NavMeshPath pathToClosesPrey = new NavMeshPath();
        Animal closestPrey = null;
        foreach (Tuple<Animal, Vector3> prey in nearbyPrey)
        {
            float distance = Vector3.Distance(this.animal.GetPosition(), prey.Item2);
            // Discard prey if it is too far away to consider chasing
            if (distance > this.maxPreyDistance) continue;
            if (minDistance == -1f || distance < minDistance || prey.Item1.isDead)
            {
                minDistance = minDistance == -1f || distance < minDistance ? distance : minDistance;
                if (closestPrey != null && (closestPrey.isDead && !prey.Item1.isDead))
                {
                    // Prioritize dead prey if the other prey is alive
                    continue;
                }
                if (closestPrey != null && (!closestPrey.isDead && !prey.Item1.isDead && distance > minDistance))
                {
                    // Prioritize closer prey if both prey are alive
                    continue;
                }
                if (this.animal.IsReachable(prey.Item2, out pathToClosesPrey))
                {
                    closestPrey = prey.Item1;
                }
            }
        }

        if (closestPrey == null)
        {
            // Prey is not reachable
            return NodeStates.FAILURE;
        }

        /**
            Attack/ Eat Prey
        */
        foreach (ELActor actor in this.animal.GetActorsBeingTouched())
        {
            if (closestPrey.GetID() != actor.GetID())
            {
                continue;
            }

            if (closestPrey.isDead)
            {
                // Eat Prey
                if ((this.eatDelayTimer) < 0)
                {
                    this.animal.GetHungerBar().AddFoodPoints(closestPrey.GetEaten(this.animal.GetBiteSize()));
                    this.eatDelayTimer = this.eatDelay;
                }
                return NodeStates.SUCCESS;
            }
            else
            {
                // Attack Prey
                if ((this.attackDelayTimer) < 0)
                {
                    this.animal.Attack(closestPrey);
                    this.attackDelayTimer = this.attackDelay;
                }
            }

            return NodeStates.RUNNING;
        }

        this.animal.RunTo(pathToClosesPrey);

        return NodeStates.RUNNING;
    }
}