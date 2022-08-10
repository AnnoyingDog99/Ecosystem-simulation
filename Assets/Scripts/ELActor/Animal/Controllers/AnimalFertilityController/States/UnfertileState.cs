
public class UnfertileState : AnimalFertilityState, IAnimalFertilityState
{
    public UnfertileState(IFertileAnimal animal) : base(animal)
    {
    }

    public bool Breed(IFertileAnimal partner)
    {
        this.context.SetStrategy(new UnfertileAnimalBreedingStrategy());
        return this.context.ExecuteStrategy(this.animal, partner);
    }
}