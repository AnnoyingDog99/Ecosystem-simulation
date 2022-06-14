using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviourTree : BehaviourTree
{
    // The distance at which a predator is too far away to be worth fleeing from
    [SerializeField] protected float maxPredatorDistance;
    [SerializeField] protected float minRestTime;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
