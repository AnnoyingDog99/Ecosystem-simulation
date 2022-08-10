using UnityEngine;
using System.Collections.Generic;

public class AnimalFertilityModel : MonoBehaviour, IAnimalFertilityModel
{
    [SerializeField] private AnimalSex sex;
    private List<Animal> _offspring = new List<Animal>();
    private Animal _mother;
    private Animal _father;
    private List<Animal> _partners = new List<Animal>();

    public List<Animal> GetOffspring()
    {
        return this._offspring;
    }

    public void AddOffspring(Animal offspring)
    {
        if (this._offspring.Contains(offspring)) return;
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
        if (this._partners.Contains(partner)) return;
        this._partners.Add(partner);
    }

    public AnimalSex GetSex()
    {
        return this.sex;
    }

    public void SetSex(AnimalSex sex)
    {
        this.sex = sex;
    }
}