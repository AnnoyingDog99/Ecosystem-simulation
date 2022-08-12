using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBehaviourTree : HerbivoreBehaviourTree
{
    private Bunny bunny;

    protected override void Start()
    {
        base.Start();
        this.bunny = GetComponent<Bunny>();
        rootNode = new Selector(new List<Node>() {
            new Sequence(new List<Node>() {
                new CheckForPredatorsNode(bunny),
                new FleeFromPredatorsOnLandNode(bunny, this.maxPredatorDistance, this.obstaclePreventionWeight)
            }),
            new Sequence(new List<Node>() {
                new IsTiredNode(bunny),
                new RestNode(bunny)
            }),
            new Sequence(new List<Node>() {
                new IsHungryNode(bunny, this.minEatingTime),
                new ForageNode(bunny, maxPlantDistance)
            }),
            new Sequence(new List<Node>() {
                new IsInHeatNode(bunny),
                new CheckForPotentialPartnersNode(bunny),
                new MateWithPartnerOnLandNode(bunny, this.maxMatingDistance)
            }),
            new Sequence(new List<Node>() {
                new HerbivoreWanderOnLandNode(bunny)
            })
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
