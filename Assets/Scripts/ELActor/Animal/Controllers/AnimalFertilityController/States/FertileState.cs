
public class FertileState : AnimalFertilityState, IAnimalFertilityState
{
    public FertileState(IFertileAnimal animal) : base(animal)
    {
    }

    public bool Breed(IFertileAnimal partner)
    {
        switch (this.animal)
        {
            case Bunny:
                this.context.SetStrategy(new BunnyBreedingStrategy());
                break;
            case Fox:
                this.context.SetStrategy(new FoxBreedingStrategy());
                break;
            default:
                this.context.SetStrategy(new UnfertileAnimalBreedingStrategy());
                break;
        }
        return this.context.ExecuteStrategy(this.animal, partner);
    }
}