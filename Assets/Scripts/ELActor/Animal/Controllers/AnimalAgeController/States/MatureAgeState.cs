public class MatureAnimalAgeState : AnimalAgeState, IAnimalAgeState
{
    public MatureAnimalAgeState(IAnimalAgeable animal) : base(animal)
    {
    }

    public void Grow()
    {
        this.context.SetStrategy(new MatureAnimalGrowStrategy());
        this.context.ExecuteStrategy(this.animal);
    }
}