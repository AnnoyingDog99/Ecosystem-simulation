using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IAnimal : IELActor, IAnimalHungerModel, IAnimalAgeModel, INutritional, IDamageable
{
    public AnimalAnimator GetAnimalAnimator();

    public Sight GetSight();

    public AnimalMemory GetAnimalMemory();

    public List<string> GetPredatorTags();

    public List<Animal> GetOffspring();

    public void AddOffspring(Animal offspring);

    public Animal GetMother();

    public void SetMother(Animal mother);

    public Animal GetFather();

    public void SetFather(Animal father);

    public List<Animal> GetPartners();

    public void AddPartner(Animal partner);

    public AnimalSex GetSex();

    public AnimalHungerController GetAnimalHungerController();

    public AnimalAgeController GetAnimalAgeController();

    public ELActorHealthController GetAnimalHealthController();
}

public enum AnimalSex
{
    M,
    F
}