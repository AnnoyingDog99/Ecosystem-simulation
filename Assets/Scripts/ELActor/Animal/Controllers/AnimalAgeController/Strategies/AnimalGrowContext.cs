public class AnimalAgeContext
{
    private IAnimalGrowStrategy strategy = null;

    public void SetStrategy(IAnimalGrowStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void ExecuteStrategy(IAgeableAnimal animal)
    {
        if (this.strategy == null) return;
        this.strategy.execute(animal);
    }
}