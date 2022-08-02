public abstract class AnimalAgeState
{
    protected AnimalAgeContext context;
    protected IAgeableAnimal animal;

    public AnimalAgeState(IAgeableAnimal animal)
    {
        this.animal = animal;
        this.context = new AnimalAgeContext();
    }
}