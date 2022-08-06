using System;
using System.Collections.Generic;
using UnityEngine;
public class RestNode : Node
{
    private ILandAnimal landAnimal;

    public RestNode(ILandAnimal landAnimal)
    {
        this.landAnimal = landAnimal;
    }

    public override NodeStates Evaluate()
    {
        Debug.Log("Resting");
        this.landAnimal.GetLandAnimalMovementController().Idle();
        return NodeStates.SUCCESS;
    }
}