public class OldAnimalAgeState : AnimalAgeState, IAnimalAgeState
{
    public OldAnimalAgeState(IAgeableAnimal animal) : base(animal)
    {
    }

    public void Grow()
    {
        this.context.SetStrategy(new OldAnimalGrowStrategy());
        this.context.ExecuteStrategy(this.animal);
    }
}