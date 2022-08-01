using UnityEngine;
using UnityEngine.AI;

public interface IAnimalHungerStrategy
{
    public bool execute(IAnimal animal, INutritional target);
}