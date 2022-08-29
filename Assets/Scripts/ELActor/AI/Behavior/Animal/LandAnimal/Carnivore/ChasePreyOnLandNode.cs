using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

public class ChasePreyNode : Node
{
    private ICarnivoreLandAnimal carnivoreLandAnimal;
    private float maxPreyDistance;
    private float attackDelay = 2;
    private float attackDelayTimer;
    private float eatDelay = 2;
    private float eatDelayTimer;

    public ChasePreyNode(ICarnivoreLandAnimal carnivoreLandAnimal, float maxPreyDistance)
    {
        this.carnivoreLandAnimal = carnivoreLandAnimal;
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
        ICarnivoreMemory memory = this.carnivoreLandAnimal.GetCarnivoreMemory();

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
            float distance = Vector3.Distance(this.carnivoreLandAnimal.GetPosition(), prey.Item2);
            // Discard prey if it is too far away to consider chasing
            if (distance > this.maxPreyDistance) continue;
            if (minDistance == -1f || distance < minDistance || prey.Item1.GetAnimalHealthController().IsDead())
            {
                minDistance = minDistance == -1f || distance < minDistance ? distance : minDistance;
                if (closestPrey != null && (closestPrey.GetAnimalHealthController().IsDead() && !prey.Item1.GetAnimalHealthController().IsDead()))
                {
                    // Prioritize dead prey if the other prey is alive
                    continue;
                }
                if (closestPrey != null && (!closestPrey.GetAnimalHealthController().IsDead() && !prey.Item1.GetAnimalHealthController().IsDead() && distance > minDistance))
                {
                    // Prioritize closer prey if both prey are alive
                    continue;
                }
                if (this.carnivoreLandAnimal.GetLandAnimalMovementController().CalculatePath(prey.Item2, out pathToClosesPrey))
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
        foreach (ELActor actor in this.carnivoreLandAnimal.GetCollidingActors())
        {
            if (closestPrey.GetID() != actor.GetID())
            {
                continue;
            }

            if (closestPrey.GetAnimalHealthController().IsDead())
            {
                // Eat Prey
                if ((this.eatDelayTimer) < 0)
                {
                    this.carnivoreLandAnimal.GetAnimalHungerController().Eat(closestPrey);
                    this.eatDelayTimer = this.eatDelay;
                }
                return NodeStates.SUCCESS;
            }
            else
            {
                // Attack Prey
                if ((this.attackDelayTimer) < 0)
                {
                    closestPrey.GetAnimalHealthController().GetDamaged(this.carnivoreLandAnimal.GetAttackDamage());
                    this.attackDelayTimer = this.attackDelay;
                }
            }

            return NodeStates.RUNNING;
        }

        this.carnivoreLandAnimal.GetLandAnimalMovementController().RunTo(pathToClosesPrey);

        return NodeStates.RUNNING;
    }
}