using UnityEngine;

public class AnimalFertilityController : Controller
{
    [SerializeField] private AgeTracker ageTracker;

    private IFertileAnimal fertileAnimal;
    private IAnimalFertilityState fertilityState;

    protected override void Start()
    {
        this.fertileAnimal = GetComponentInParent<IFertileAnimal>();

        this.ageTracker.GetStatus().Subscribe((AgeTracker.AgeStatus status) =>
        {
            switch (status)
            {
                case AgeTracker.AgeStatus.YOUNG:
                    this.fertilityState = new UnfertileState(this.fertileAnimal);
                    break;
                case AgeTracker.AgeStatus.MATURE:
                    this.fertilityState = new FertileState(this.fertileAnimal);
                    break;
                case AgeTracker.AgeStatus.OLD:
                    this.fertilityState = new UnfertileState(this.fertileAnimal);
                    break;
            }
        });
    }

    public bool Breed(IFertileAnimal partner)
    {
        return this.fertilityState.Breed(partner);
    }

    public AgeTracker GetAgeTracker()
    {
        return this.ageTracker;
    }

    public bool IsPotentialPartner(IFertileAnimal partner)
    {
        if (partner == null) return false;
        
        if (this.fertileAnimal.GetSex() == partner.GetSex())
        {
            return false;
        }

        float animalAge = this.GetAgeTracker().GetCurrent();
        if (animalAge < this.GetAgeTracker().GetMatureAge()
        || animalAge > this.GetAgeTracker().GetOldAge())
        {
            return false;
        }

        float partnerAge = partner.GetAnimalFertilityController().GetAgeTracker().GetCurrent();
        if (partnerAge < partner.GetAnimalFertilityController().GetAgeTracker().GetMatureAge()
        || partnerAge > partner.GetAnimalFertilityController().GetAgeTracker().GetOldAge())
        {
            return false;
        }

        if (this.fertileAnimal.GetPartners().Count > 0)
        {
            return false;
        }

        return true;
    }
}