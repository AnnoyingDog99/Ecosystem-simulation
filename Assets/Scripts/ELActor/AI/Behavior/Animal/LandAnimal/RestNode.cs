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
        this.landAnimal.GetLandAnimalMovementController().Idle();
        return NodeStates.SUCCESS;
    }
}