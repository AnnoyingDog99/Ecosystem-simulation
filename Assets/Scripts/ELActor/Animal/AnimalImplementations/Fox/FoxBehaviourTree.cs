using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBehaviourTree : CarnivoreBehaviourTree
{
    private Fox fox;

    protected override void Start()
    {
        base.Start();
        this.fox = GetComponent<Fox>();
        rootNode = new Selector(new List<Node>() {
            new Sequence(new List<Node>() {
                new CheckForPredatorsNode(fox),
                new FleeFromPredatorsOnLandNode(fox, this.maxPredatorDistance, this.obstaclePreventionWeight)
            }),
            new Sequence(new List<Node>() {
                new IsTiredNode(fox),
                new RestNode(fox)
            }),
            new Sequence(new List<Node>() {
                new IsHungryNode(fox, this.minEatingTime),
                new CheckForPreyNode(fox),
                new ChasePreyNode(fox, this.maxPreyDistance)
            }),
            new Sequence(new List<Node>() {
                new IsInHeatNode(fox),
                new CheckForPotentialPartnersNode(fox),
                new MateWithPartnerOnLandNode(fox, this.maxMatingDistance)
            }),
            new Sequence(new List<Node>() {
                new CarnivoreWanderOnLandNode(fox)
            })
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
