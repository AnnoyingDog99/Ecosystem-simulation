using System;
using System.Collections.Generic;
using UnityEngine;
public class IsHungryNode : Node
{
    private Animal animal;

    private float keepEatingTime = 10f;
    private float keepEatingTimer;

    public IsHungryNode(Animal animal)
    {
        this.animal = animal;
        this.keepEatingTimer = 0;
    }

    public override NodeStates Evaluate()
    {
        bool keepEating = false;

        if (this.animal.GetHungerBar().IsHungry())
        {
            // Keep eating if animal is still hungry
            keepEating = true;
            this.keepEatingTimer = this.keepEatingTime;
        }
        else if (this.animal.GetHungerBar().GetHungerPercentage() >= 100)
        {
            // Animal is full, stop eating
            this.keepEatingTimer = 0;
            keepEating = false;
        }
        else
        {
            // Keep eating until timer runs out
            keepEatingTimer -= Time.deltaTime;
            keepEating = this.keepEatingTimer > 0;
        }

        /**
            Evaluate whether the animal should keep looking for and eating food
        */
        return keepEating ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}
