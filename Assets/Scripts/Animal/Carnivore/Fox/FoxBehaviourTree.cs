using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBehaviourTree : BehaviourTree
{
    [SerializeField] Fox fox;

    protected override void Start()
    {
        base.Start();
        rootNode = new Selector(new List<Node>() {
            new Sequence(new List<Node>() {
                new CheckForPrey(fox),
                new ChasePreyNode(fox)
            })
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
