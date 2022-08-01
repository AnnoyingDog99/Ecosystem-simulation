using UnityEngine;
using UnityEngine.AI;

public class DefaultAnimalHungerState : AnimalHungerState, IAnimalHungerState
{
    public DefaultAnimalHungerState(IAnimal animal) : base(animal)
    {
    }

    public bool Eat(INutritional target)
    {
        this.context.SetStrategy(new DefaultAnimalHungerStrategy());
        return context.ExecuteStrategy(this.animal, target);
    }
}