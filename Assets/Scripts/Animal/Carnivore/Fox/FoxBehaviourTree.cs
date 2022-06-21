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
                new CheckForPredatorsNode(fox),
                new FleeFromPredatorsNode(fox, this.maxPredatorDistance)
            }),
            new Sequence(new List<Node>() {
                new IsTiredNode(fox, this.minRestTime),
                new RestNode(fox)
            }),
            new Sequence(new List<Node>() {
                new IsHungryNode(fox),
                new CheckForPreyNode(fox),
                new ChasePreyNode(fox, this.maxPreyDistance)
            }),
            new Sequence(new List<Node>() {
                new IsInHeatNode(fox),
                new CheckForPotentialPartnersNode(fox),
                new MateWithPartnerNode(fox, this.maxMatingDistance)
            }),
            new Sequence(new List<Node>() {
                new WanderNode(fox)
            })
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
