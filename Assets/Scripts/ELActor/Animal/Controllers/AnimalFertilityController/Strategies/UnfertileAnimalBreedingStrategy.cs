public class UnfertileAnimalBreedingStrategy : IAnimalBreedingStrategy
{
    public bool execute(IFertileAnimal animal, IFertileAnimal partner)
    {
        return false;
    }
}