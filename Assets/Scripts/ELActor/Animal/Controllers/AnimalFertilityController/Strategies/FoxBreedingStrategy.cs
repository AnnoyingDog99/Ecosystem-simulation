public class FoxBreedingStrategy : IAnimalBreedingStrategy
{
    public bool execute(IFertileAnimal bunny, IFertileAnimal partner)
    {
        // if (animal.GetSex() != AnimalSex.F && partner.GetSex() != AnimalSex.F)
        // {
        //     return false;
        // }
        // if (animal.GetSex() != AnimalSex.F)
        // {
        //     return partner.GetAnimalFertilityController().Breed(animal);
        // }
        // if (!animal.DoActorsCollide(partner))
        // {
        //     return false;
        // }

        // Animal offspring = Director.Instance.SpawnActor(ActorFactory.TranslateClassToType(animal), animal.GetPosition(), animal.GetRotation()) as Animal;
        // return true;
        return true;
    }
}