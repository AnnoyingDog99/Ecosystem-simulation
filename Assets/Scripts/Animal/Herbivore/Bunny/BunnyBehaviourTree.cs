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
                new FleeFromPredatorsNode(bunny, this.maxPredatorDistance, this.obstaclePreventionWeight)
            }),
            new Sequence(new List<Node>() {
                new IsHungryNode(bunny),
                new ForageNode(bunny, maxPlantDistance)
            }),
            new Sequence(new List<Node>() {
                new IsTiredNode(bunny, this.minRestTime),
                new RestNode(bunny)
            }),
            new Sequence(new List<Node>() {
                new IsInHeatNode(bunny),
                new CheckForPotentialPartnersNode(bunny),
                new MateWithPartnerNode(bunny, this.maxMatingDistance)
            }),
            new Sequence(new List<Node>() {
                new WanderNode(bunny)
            })
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
