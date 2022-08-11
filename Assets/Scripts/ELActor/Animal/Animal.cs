using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animal : ELActor, IAnimal
{
    [SerializeField] protected BehaviourTree behaviourTree;
    [SerializeField] private AnimalMemory memory;
    [SerializeField] private Sight sight;
    [SerializeField][TagSelector] public List<string> predatorTags = new List<string>();
    [SerializeField] private AnimalHungerModel hungerModel;
    [SerializeField] private NutritionalModel nutritionalModel;
    [SerializeField] private AnimalAgeModel ageModel;
    [SerializeField] private AttackModel attackModel;
    [SerializeField] private AnimalFertilityModel fertilityModel;
    private AnimalMovementController _movementController;
    private AnimalHungerController _hungerController;
    private AnimalAgeController _ageController;
    private ELActorHealthController _healthController;
    private AnimalFertilityController _fertilityController;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this._movementController = GetComponent<AnimalMovementController>();
        this._hungerController = GetComponent<AnimalHungerController>();
        this._ageController = GetComponent<AnimalAgeController>();
        this._healthController = GetComponent<ELActorHealthController>();
        this._fertilityController = GetComponent<AnimalFertilityController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.GetAnimalHealthController().IsDead()) return;
        this.behaviourTree.Evaluate();
    }

    public virtual void OnDeath()
    {
        this.GetAnimalMovementController().Idle();
        this.GetAnimalHungerController().GetHungerTracker().Pause();
        this.GetAnimalAgeController().GetAgeTracker().Pause();
        this.GetAnimalHealthController().GetHealthTracker().Pause();
        this.GetActorScaleController().SetScale(this.GetScale());

        Director.Instance.QueueActorDestruction(this, 25f);
    }

    public AnimalAnimator GetAnimalAnimator()
    {
        return this.GetActorAnimator() as AnimalAnimator;
    }

    public Sight GetSight()
    {
        return this.sight;
    }

    public AnimalMemory GetAnimalMemory()
    {
        return this.memory;
    }

    public List<string> GetPredatorTags()
    {
        return this.predatorTags;
    }


    public List<Animal> GetOffspring()
    {
        return this.fertilityModel.GetOffspring();
    }

    public void AddOffspring(Animal offspring)
    {
        this.fertilityModel.AddOffspring(offspring);
    }

    public Animal GetMother()
    {
        return this.fertilityModel.GetMother();
    }

    public void SetMother(Animal mother)
    {
        this.fertilityModel.SetMother(mother);
    }

    public Animal GetFather()
    {
        return this.fertilityModel.GetFather();
    }

    public void SetFather(Animal father)
    {
        this.fertilityModel.SetFather(father);
    }

    public List<Animal> GetPartners()
    {
        return this.fertilityModel.GetPartners();
    }

    public void AddPartner(Animal partner)
    {
        this.fertilityModel.AddPartner(partner);
    }

    public AnimalSex GetSex()
    {
        return this.fertilityModel.GetSex();
    }

    public void SetSex(AnimalSex sex)
    {
        this.fertilityModel.SetSex(sex);
    }

    public virtual float GetEaten(float amount)
    {
        return this.nutritionalModel.GetEaten(amount);
    }
    public float GetMaxFoodPoints()
    {
        return this.nutritionalModel.GetMaxFoodPoints();
    }
    public float GetCurrentFoodPoints()
    {
        return this.nutritionalModel.GetCurrentFoodPoints();
    }

    public void SetCurrentFoodPoints(float foodPoints)
    {
        this.nutritionalModel.SetCurrentFoodPoints(foodPoints);
    }

    public float GetBiteSize()
    {
        return this.hungerModel.GetBiteSize();
    }

    public AnimalMovementController GetAnimalMovementController()
    {
        return this._movementController;
    }

    public AnimalHungerController GetAnimalHungerController()
    {
        return this._hungerController;
    }

    public AnimalAgeController GetAnimalAgeController()
    {
        return this._ageController;
    }

    public ELActorHealthController GetAnimalHealthController()
    {
        return this._healthController;
    }


    public float GetAttackDamage()
    {
        return this.attackModel.GetAttackDamage();
    }

    public void GetDamaged(float damage)
    {
        this.GetAnimalHealthController().GetDamaged(damage);
    }

    public AnimalFertilityController GetAnimalFertilityController()
    {
        return this._fertilityController;
    }
}