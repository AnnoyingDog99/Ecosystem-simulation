using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBehaviourTree : BehaviourTree
{
    [SerializeField] Fox fox;
    
    // The distance at which a prey is too far away to be worth chasing
    [SerializeField] float maxPreyDistance;

    protected override void Start()
    {
        base.Start();
        rootNode = new Selector(new List<Node>() {
            new Sequence(new List<Node>() {
                new CheckForPrey(fox),
                new ChasePreyNode(fox, maxPreyDistance)
            })
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
