public interface IAgeableAnimal : IAnimal, IAnimalAgeModel
{
    public AnimalAgeController GetAnimalAgeController();
}