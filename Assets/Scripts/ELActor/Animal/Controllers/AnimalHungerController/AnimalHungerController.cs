using UnityEngine;
using UnityEngine.AI;

public class AnimalHungerController : Controller
{
    [SerializeField] private HungerTracker hungerTracker;

    private IAnimal animal;
    private IAnimalHungerState hungerState;

    private void Start()
    {
        this.animal = GetComponentInParent<IAnimal>();
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