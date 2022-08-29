public class YoungAnimalAgeState : AnimalAgeState, IAnimalAgeState
{
    public YoungAnimalAgeState(Animal animal) : base(animal)
    {
    }

    public void Grow()
    {
        this.context.SetStrategy(new YoungAnimalGrowStrategy());
        this.context.ExecuteStrategy(this.animal);
    }
}