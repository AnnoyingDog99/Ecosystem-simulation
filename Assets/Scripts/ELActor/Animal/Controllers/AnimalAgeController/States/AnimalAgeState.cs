public abstract class AnimalAgeState
{
    protected AnimalAgeContext context;
    protected IAnimal animal;

    public AnimalAgeState(IAnimal animal)
    {
        this.animal = animal;
        this.context = new AnimalAgeContext();
    }
}