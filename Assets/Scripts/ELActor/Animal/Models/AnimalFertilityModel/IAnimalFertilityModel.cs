using System.Collections.Generic;

public interface IAnimalFertilityModel
{
    public List<Animal> GetOffspring();

    public void AddOffspring(Animal offspring);

    public Animal GetMother();

    public void SetMother(Animal mother);

    public Animal GetFather();

    public void SetFather(Animal father);

    public List<Animal> GetPartners();

    public void AddPartner(Animal partner);

    public AnimalSex GetSex();
}