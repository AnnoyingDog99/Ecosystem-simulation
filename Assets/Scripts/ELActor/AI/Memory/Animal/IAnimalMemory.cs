using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAnimalMemory : IELActorMemory
{
    public void AddPredatorMemory(Animal predator);
    public List<Animal> GetPredatorsInMemory();
    public void AddOwnKindMemory(Animal ownKind);
    public List<Animal> GetOwnKindInMemory();
    public void AddObstacleMemory(Sight.ObstacleLocation obstacle);
    public List<Sight.ObstacleLocation> GetObstaclesInMemory();
}
