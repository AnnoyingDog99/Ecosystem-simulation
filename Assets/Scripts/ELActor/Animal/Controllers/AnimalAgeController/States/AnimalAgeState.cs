public abstract class AnimalAgeState
{
    protected AnimalAgeContext context;
    protected Animal animal;

    public AnimalAgeState(Animal animal)
    {
        this.animal = animal;
        this.context = new AnimalAgeContext();
    }
}