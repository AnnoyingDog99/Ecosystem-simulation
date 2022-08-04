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
    private AnimalHungerController _hungerController;
    private AnimalAgeController _ageController;
    private ELActorHealthController _healthController;
    private List<Animal> _offspring = new List<Animal>();
    private Animal _mother;
    private Animal _father;
    private List<Animal> _partners = new List<Animal>();
    private AnimalSex _sex;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this._hungerController = GetComponent<AnimalHungerController>();
        this._ageController = GetComponent<AnimalAgeController>();
        this._healthController = GetComponent<ELActorHealthController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        this.behaviourTree.Evaluate();
    }

    public virtual void OnDeath()
    {
        this.GetAnimalHungerController().GetHungerTracker().Pause();
        this.GetAnimalAgeController().GetAgeTracker().Pause();
        this.GetAnimalHealthController().GetHealthTracker().Pause();
        this.GetActorScaleController().SetScale(this.GetScale());
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
        return this._offspring;
    }

    public void AddOffspring(Animal offspring)
    {
        this._offspring.Add(offspring);
    }

    public Animal GetMother()
    {
        if (this._mother != null && !Director.Instance.ActorExists(this._mother))
        {
            this.SetMother(null);
        }
        return this._mother;
    }

    public void SetMother(Animal mother)
    {
        this._mother = mother;
    }

    public Animal GetFather()
    {
        if (this._father != null && !Director.Instance.ActorExists(this._father))
        {
            this.SetFather(null);
        }
        return this._father;
    }

    public void SetFather(Animal father)
    {
        this._father = father;
    }

    public List<Animal> GetPartners()
    {
        this._partners = this._partners.FindAll((_partner) => Director.Instance.ActorExists(_partner));
        return this._partners;
    }

    public void AddPartner(Animal partner)
    {
        this._partners.Add(partner);
    }

    public AnimalSex GetSex()
    {
        return this._sex;
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
}