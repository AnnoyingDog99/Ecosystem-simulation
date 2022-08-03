public class OldAnimalAgeState : AnimalAgeState, IAnimalAgeState
{
    public OldAnimalAgeState(IAnimal animal) : base(animal)
    {
    }

    public void Grow()
    {
        this.context.SetStrategy(new OldAnimalGrowStrategy());
        this.context.ExecuteStrategy(this.animal);
    }
}