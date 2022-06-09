using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBehaviourTree : CarnivoreBehaviourTree
{
    [SerializeField] Fox fox;

    protected override void Start()
    {
        base.Start();
        rootNode = new Selector(new List<Node>() {
            new Sequence(new List<Node>() {
                new CheckForPreyNode(fox),
                new ChasePreyNode(fox, this.maxPreyDistance)
            })
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
