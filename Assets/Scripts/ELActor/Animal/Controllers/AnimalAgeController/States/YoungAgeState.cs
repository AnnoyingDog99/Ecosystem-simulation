public class YoungAnimalAgeState : AnimalAgeState, IAnimalAgeState
{
    public YoungAnimalAgeState(IAnimal animal) : base(animal)
    {
    }

    public void Grow()
    {
        this.context.SetStrategy(new YoungAnimalGrowStrategy());
        this.context.ExecuteStrategy(this.animal);
    }
}