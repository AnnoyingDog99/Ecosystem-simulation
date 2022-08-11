using System;
using System.Collections.Generic;
using UnityEngine;

public class FoxMemory : AnimalMemory, ICarnivoreMemory
{
    [SerializeField] CarnivoreMemoryModel carnivoreMemoryModel;

    public void UpdateMemories(ELActorMemory parent)
    {
        this.carnivoreMemoryModel.UpdateMemories(parent);
    }

    public void AddPreyMemory(Tuple<Animal, Vector3> animalAndPosition)
    {
        this.carnivoreMemoryModel.AddPreyMemory(animalAndPosition);
    }

    public List<Tuple<Animal, Vector3>> GetPreyInMemory()
    {
        return this.carnivoreMemoryModel.GetPreyInMemory();
    }
}
