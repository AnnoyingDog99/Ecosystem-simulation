using UnityEngine;
public class AnimalAgeController : Controller
{
    [SerializeField] private AgeTracker ageTracker;
    private IAnimalAgeable animal;
    private IAnimalAgeState ageState;

    private void Start()
    {
        this.animal = GetComponentInParent<IAnimalAgeable>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FirstUpdate()
    {
        base.FirstUpdate();
        this.ageTracker.GetStatus().Subscribe((AgeTracker.AgeStatus status) =>
        {
            switch (status)
            {
                case AgeTracker.AgeStatus.YOUNG:
                    this.ageState = new YoungAnimalAgeState(this.animal);
                    break;
                case AgeTracker.AgeStatus.MATURE:
                    this.ageState = new MatureAnimalAgeState(this.animal);
                    break;
                case AgeTracker.AgeStatus.OLD:
                    this.ageState = new OldAnimalAgeState(this.animal);
                    break;
            }
            this.ageState.Grow();
        });
    }

    public AgeTracker GetAgeTracker()
    {
        return this.ageTracker;
    }
}