using System;
using System.Collections.Generic;
using UnityEngine;
public class RestNode : Node
{
    private Animal animal;

    public RestNode(Animal animal)
    {
        this.animal = animal;
    }

    public override NodeStates Evaluate()
    {
        this.animal.Idle();
        return NodeStates.SUCCESS;
    }
}
