using UnityEngine;
using UnityEngine.AI;

public class DefaultAnimalHungerState : AnimalHungerState, IAnimalHungerState
{
    public DefaultAnimalHungerState(IAnimal animal) : base(animal)
    {
    }

    public bool Eat(INutritional target)
    {
        if (this.animal is IOmnivore)
        {
            this.context.SetEatStrategy(new OmnivoreEatStrategy());
        }
        else if (this.animal is IHerbivore)
        {
            this.context.SetEatStrategy(new HerbivoreEatStrategy());
        }
        else if (this.animal is ICarnivore)
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