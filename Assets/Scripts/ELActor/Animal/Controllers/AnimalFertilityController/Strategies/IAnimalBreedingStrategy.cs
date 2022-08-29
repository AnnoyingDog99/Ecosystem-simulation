using UnityEngine;
using UnityEngine.AI;

public interface IAnimalBreedingStrategy
{
    public bool execute(IFertileAnimal animal, IFertileAnimal partner);
}