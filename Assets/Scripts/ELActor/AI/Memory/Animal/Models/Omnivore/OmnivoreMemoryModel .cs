using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OmnivoreMemoryModel : MonoBehaviour, IOmnivoreMemoryModel
{
    [SerializeField] HerbivoreMemoryModel herbivoreMemoryModel;
    [SerializeField] CarnivoreMemoryModel carnivoreMemoryModel;

    public void UpdateMemories(ELActorMemory parent)
    {
        herbivoreMemoryModel.UpdateMemories(parent);
        carnivoreMemoryModel.UpdateMemories(parent);
    }

    public void AddPlantMemory(Plant plant)
    {
        this.herbivoreMemoryModel.AddPlantMemory(plant);
    }

    public List<Plant> GetPlantsInMemory()
    {
        return herbivoreMemoryModel.GetPlantsInMemory();
    }

    public void AddPreyMemory(Tuple<Animal, Vector3> animalAndPosition)
    {
        this.carnivoreMemoryModel.AddPreyMemory(animalAndPosition);
    }

    public List<Tuple<Animal, Vector3>> GetPreyInMemory()
    {
        return this.GetPreyInMemory();
    }
}
