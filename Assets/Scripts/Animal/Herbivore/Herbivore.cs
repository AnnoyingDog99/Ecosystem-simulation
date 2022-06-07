using System.Collections.Generic;
using UnityEngine;

public class Herbivore : Animal
{
    [SerializeField] protected List<string> plantTags = new List<string>();

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
        return this.plantTags;
    }
}
