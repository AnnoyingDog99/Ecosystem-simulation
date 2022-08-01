using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animal : ELActor, IAnimal, INutritional
{
    [SerializeField] protected BehaviourTree behaviourTree;
    [SerializeField] private AnimalMemory memory;
    [SerializeField] private Sight sight;

    [SerializeField] private AnimalHungerModel hungerModel;

    private AnimalHungerController _hungerController;

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
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        this.behaviourTree.Evaluate();
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
        return 1;
    }

    public float GetBiteSize()
    {
        return this.hungerModel.GetBiteSize();
    }

    public AnimalHungerController GetAnimalHungerController()
    {
        return this._hungerController;
    }
}