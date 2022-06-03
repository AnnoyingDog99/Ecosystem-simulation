using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBehaviourTree : BehaviourTree
{
    [SerializeField] Bunny bunny;

    protected override void Start()
    {
        base.Start();
        rootNode = new Selector(new List<Node>() {
            new Sequence(new List<Node>() {
                new CheckForPredators(bunny),
                new FleeFromPredatorsNode(bunny)
            })
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
