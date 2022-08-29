using UnityEngine;
public class BunnyBreedingStrategy : IAnimalBreedingStrategy
{
    public bool execute(IFertileAnimal animal, IFertileAnimal partner)
    {
        if (!(animal is Bunny)) return false;
        if (!(partner is Bunny)) return false;

        if (animal.GetSex() != AnimalSex.F && partner.GetSex() != AnimalSex.F)
        {
            return false;
        }
        if (animal.GetSex() != AnimalSex.F)
        {
            return partner.GetAnimalFertilityController().Breed(animal);
        }
        if (!animal.DoActorsCollide(partner))
        {
            return false;
        }

        Bunny bunnyMother = animal as Bunny;
        Bunny bunnyFather = partner as Bunny;

        Bunny offspring = Director.Instance.SpawnActor(ActorFactory.ActorType.BUNNY, bunnyMother.GetPosition(), bunnyMother.GetRotation()) as Bunny;

        bunnyMother.AddOffspring(offspring);
        bunnyFather.AddOffspring(offspring);

        bunnyMother.AddPartner(bunnyFather);
        bunnyFather.AddPartner(bunnyMother);

        Identifier identifier = null;
        identifier = offspring.GetActorInitializationState().Subscribe((ELActorInitializationState initializationState) =>
        {
            if (initializationState < ELActorInitializationState.UPDATING) return;
            offspring.SetMother(bunnyMother);
            offspring.SetFather(bunnyFather);
            offspring.GetAnimalAgeController().GetAgeTracker().SetCurrent(0);
            offspring.GetActorInitializationState().Unsubscribe(identifier);
        });

        return true;
    }
}