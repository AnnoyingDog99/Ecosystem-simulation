public interface IAnimalAgeable : IAnimal, IAnimalAgeModel
{
    public AnimalAgeController GetAnimalAgeController();
}