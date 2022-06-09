using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBehaviourTree : HerbivoreBehaviourTree
{
    [SerializeField] Bunny bunny;

    protected override void Start()
    {
        base.Start();
        rootNode = new Selector(new List<Node>() {
            new Sequence(new List<Node>() {
                new CheckForPredatorsNode(bunny),
                new FleeFromPredatorsNode(bunny, this.maxPredatorDistance)
            }),
            new Sequence(new List<Node>() {
                new IsHungryNode(bunny),
                new ForageNode(bunny, maxPlantDistance)
            })
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
