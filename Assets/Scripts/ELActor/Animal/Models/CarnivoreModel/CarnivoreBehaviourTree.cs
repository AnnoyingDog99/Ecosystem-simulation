using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivoreBehaviourTree : AnimalBehaviourTree
{
    // The distance at which a prey is too far away to be worth chasing
    [SerializeField] protected float maxPreyDistance;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
