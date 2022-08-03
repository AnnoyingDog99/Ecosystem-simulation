using UnityEngine;
using UnityEngine.AI;

public class DefaultAnimalHungerState : AnimalHungerState, IAnimalHungerState
{
    public DefaultAnimalHungerState(IAnimal animal) : base(animal)
    {
    }

    public bool Eat(INutritional target)
    {
        if (target is IOmnivore)
        {
            this.context.SetEatStrategy(new OmnivoreEatStrategy());
        }
        else if (target is IHerbivore)
        {
            this.context.SetEatStrategy(new HerbivoreEatStrategy());
        }
        else if (target is ICarnivore)
        {
            this.context.SetEatStrategy(new CarnivoreEatStrategy());
        }
        else
        {
            return false;
        }
        return context.ExecuteEatStrategy(this.animal, target);
    }
}