using UnityEngine;
public class FoxBreedingStrategy : IAnimalBreedingStrategy
{
    public bool execute(IFertileAnimal animal, IFertileAnimal partner)
    {
        if (!(animal is Fox)) return false;
        if (!(partner is Fox)) return false;

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

        Fox foxMother = animal as Fox;
        Fox foxFather = partner as Fox;

        Fox offspring = Director.Instance.SpawnActor(ActorFactory.ActorType.FOX, foxMother.GetPosition(), foxMother.GetRotation()) as Fox;

        foxMother.AddOffspring(offspring);
        foxFather.AddOffspring(offspring);

        foxMother.AddPartner(foxFather);
        foxFather.AddPartner(foxMother);

        Identifier identifier = null;
        identifier = offspring.GetActorInitializationState().Subscribe((ELActorInitializationState initializationState) =>
        {
            if (initializationState < ELActorInitializationState.UPDATING) return;
            offspring.SetMother(foxMother);
            offspring.SetFather(foxFather);
            offspring.GetAnimalAgeController().GetAgeTracker().SetCurrent(0);
            offspring.GetActorInitializationState().Unsubscribe(identifier);
        });

        return true;
    }
}