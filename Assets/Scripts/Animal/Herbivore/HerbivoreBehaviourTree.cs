using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbivoreBehaviourTree : AnimalBehaviourTree
{
    // The distance at which a plant is too far away to be worth gathering
    [SerializeField] protected float maxPlantDistance;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
