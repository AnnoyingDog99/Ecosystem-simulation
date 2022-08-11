using System;
using System.Collections.Generic;
using UnityEngine;
public class IsHungryNode : Node
{
    private Animal animal;

    private float keepEatingTime = 30f;
    private float keepEatingTimer;
    private bool keepEating = false;

    public IsHungryNode(Animal animal, float minEatingTime)
    {
        this.animal = animal;
        this.keepEatingTimer = 0;
        this.keepEatingTime = minEatingTime;
    }

    public override NodeStates Evaluate()
    {
        HungerTracker.HungerStatus hungerStatus = this.animal.GetAnimalHungerController().GetHungerTracker().GetStatus().Get();
        float hungerPercentage = this.animal.GetAnimalHungerController().GetHungerTracker().GetCurrentPercentage();

        if (this.keepEating)
        {
            this.keepEatingTimer += Time.deltaTime;
        }
        if (hungerPercentage >= 100)
        {
            this.keepEating = false;
        }
        else if (hungerStatus <= HungerTracker.HungerStatus.SATISFIED && this.keepEatingTimer >= this.keepEatingTime)
        {
            this.keepEating = false;
        }
        else if (!this.keepEating && hungerStatus >= HungerTracker.HungerStatus.HUNGRY)
        {
            this.keepEating = true;
            this.keepEatingTimer = 0;
        }

        /**
            Evaluate whether the animal should keep looking for and eat food
        */
        return this.keepEating ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}