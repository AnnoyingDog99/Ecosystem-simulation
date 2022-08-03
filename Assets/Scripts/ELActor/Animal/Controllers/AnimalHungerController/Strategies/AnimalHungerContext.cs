public class AnimalHungerContext
{
    private IAnimalEatStrategy eatStrategy = null;

    public void SetEatStrategy(IAnimalEatStrategy strategy)
    {
        this.eatStrategy = strategy;
    }

    public bool ExecuteEatStrategy(IAnimal animal, INutritional target)
    {
        if (this.eatStrategy == null) return false;
        return this.eatStrategy.execute(animal, target);
    }
}