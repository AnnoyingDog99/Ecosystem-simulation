using UnityEngine;
using UnityEngine.AI;
public interface IAnimalHungerState
{
    public bool Eat(INutritional target);
}