public abstract class AnimalHungerState
{
    protected AnimalHungerContext context;
    protected IAnimal animal;

    public AnimalHungerState(IAnimal animal)
    {
        this.animal = animal;
        this.context = new AnimalHungerContext();
    }
}