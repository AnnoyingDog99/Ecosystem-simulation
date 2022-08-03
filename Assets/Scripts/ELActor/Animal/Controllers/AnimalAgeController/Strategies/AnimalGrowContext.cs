public class AnimalAgeContext
{
    private IAnimalGrowStrategy strategy = null;

    public void SetStrategy(IAnimalGrowStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void ExecuteStrategy(IAnimal animal)
    {
        if (this.strategy == null) return;
        this.strategy.execute(animal);
    }
}