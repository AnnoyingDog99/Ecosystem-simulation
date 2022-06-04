using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBehaviourTree : BehaviourTree
{
    [SerializeField] Bunny bunny;

    // The distance at which a predator is too far away to be worth fleeing from
    [SerializeField] float maxPredatorDistance;

    protected override void Start()
    {
        base.Start();
        rootNode = new Selector(new List<Node>() {
            new Sequence(new List<Node>() {
                new CheckForPredators(bunny),
                new FleeFromPredatorsNode(bunny, maxPredatorDistance)
            })
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
