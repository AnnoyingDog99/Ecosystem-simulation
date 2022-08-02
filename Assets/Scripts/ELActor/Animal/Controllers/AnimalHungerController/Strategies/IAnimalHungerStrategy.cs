public interface IAnimalHungerStrategy
{
    public bool execute(IAnimal animal, INutritional target);
}