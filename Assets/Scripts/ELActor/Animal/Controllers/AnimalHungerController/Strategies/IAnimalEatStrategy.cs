public interface IAnimalEatStrategy
{
    public bool execute(IAnimal animal, INutritional target);
}