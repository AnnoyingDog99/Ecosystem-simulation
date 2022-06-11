using System;
using System.Collections.Generic;
using UnityEngine;
public class IsHungryNode : Node
{
    private Animal animal;

    public IsHungryNode(Animal animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        /**
            Evaluate whether the animal is hungry
        */
        return NodeStates.SUCCESS;
        return this.animal.GetHungerBar().IsHungry() ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}
