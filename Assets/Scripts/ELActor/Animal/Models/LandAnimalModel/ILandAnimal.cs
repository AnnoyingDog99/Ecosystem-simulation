public interface ILandAnimal : IAnimal, ILandAnimalModel
{
    public LandAnimalMovementController GetLandAnimalMovementController();
}