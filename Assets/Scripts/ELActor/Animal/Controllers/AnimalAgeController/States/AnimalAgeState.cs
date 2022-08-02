public abstract class AnimalAgeState
{
    protected AnimalAgeContext context;
    protected IAnimalAgeable animal;

    public AnimalAgeState(IAnimalAgeable animal)
    {
        this.animal = animal;
        this.context = new AnimalAgeContext();
    }
}