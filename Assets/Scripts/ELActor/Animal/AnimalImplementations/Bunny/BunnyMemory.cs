using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMemory : AnimalMemory, IHerbivoreMemory
{
    [SerializeField] HerbivoreMemoryModel herbivoreMemoryModel;

    public void UpdateMemories(ELActorMemory parent)
    {
        this.herbivoreMemoryModel.UpdateMemories(parent);
    }

    public void AddPlantMemory(Plant plant)
    {
        this.herbivoreMemoryModel.AddPlantMemory(plant);
    }

    public List<Plant> GetPlantsInMemory()
    {
        return this.herbivoreMemoryModel.GetPlantsInMemory();
    }
}
