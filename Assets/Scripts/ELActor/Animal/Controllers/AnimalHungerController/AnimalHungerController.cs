using UnityEngine;

public class AnimalHungerController : Controller
{
    [SerializeField] private HungerTracker hungerTracker;

    private IAnimal animal;
    private IAnimalHungerState hungerState;

    protected override void Start()
    {
        this.animal = GetComponentInParent<IAnimal>();
        this.hungerState = new DefaultAnimalHungerState(this.animal);
    }

    public bool Eat(INutritional target)
    {
        return this.hungerState.Eat(target);
    }

    public HungerTracker GetHungerTracker()
    {
        return this.hungerTracker;
    }
}