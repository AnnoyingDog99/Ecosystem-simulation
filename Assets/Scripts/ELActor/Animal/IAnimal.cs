using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IAnimal : IELActor, 
IAnimalHungerModel, 
IAnimalAgeModel, 
IAnimalFertilityModel, 
INutritional,
IDamageable
{
    public AnimalAnimator GetAnimalAnimator();

    public Sight GetSight();

    public AnimalMemory GetAnimalMemory();

    public List<string> GetPredatorTags();

    public AnimalMovementController GetAnimalMovementController();

    public AnimalHungerController GetAnimalHungerController();

    public AnimalAgeController GetAnimalAgeController();

    public ELActorHealthController GetAnimalHealthController();

    public AnimalFertilityController GetAnimalFertilityController();
}

public enum AnimalSex
{
    M,
    F
}