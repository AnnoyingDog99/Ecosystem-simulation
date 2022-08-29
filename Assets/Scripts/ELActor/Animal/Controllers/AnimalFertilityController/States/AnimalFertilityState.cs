public abstract class AnimalFertilityState
{
    protected AnimalBreedingContext context;
    protected IFertileAnimal animal;

    public AnimalFertilityState(IFertileAnimal animal)
    {
        this.animal = animal;
        this.context = new AnimalBreedingContext();
    }
}