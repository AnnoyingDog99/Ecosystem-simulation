public class MatureAnimalAgeState : AnimalAgeState, IAnimalAgeState
{
    public MatureAnimalAgeState(Animal animal) : base(animal)
    {
    }

    public void Grow()
    {
        this.context.SetStrategy(new MatureAnimalGrowStrategy());
        this.context.ExecuteStrategy(this.animal);
    }
}