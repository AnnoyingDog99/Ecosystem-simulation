using System.Collections.Generic;
using UnityEngine;

public class Omnivore : Animal
{
    [SerializeField] protected Herbivore herbivoreParameters;
    [SerializeField] protected Carnivore carnivoreParameters;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public List<string> GetPlantTags()
    {
        return this.herbivoreParameters.GetPlantTags();
    }

    public List<string> GetPreyTags()
    {
        return this.carnivoreParameters.GetPreyTags();
    }
}
