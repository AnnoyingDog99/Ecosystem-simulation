public class AnimalHungerContext
{
    private IAnimalHungerStrategy strategy = null;

    public void SetStrategy(IAnimalHungerStrategy strategy)
    {
        this.strategy = strategy;
    }

    public bool ExecuteStrategy(IAnimal animal, INutritional target)
    {
        if (this.strategy == null) return false;
        return this.strategy.execute(animal, target);
    }
}