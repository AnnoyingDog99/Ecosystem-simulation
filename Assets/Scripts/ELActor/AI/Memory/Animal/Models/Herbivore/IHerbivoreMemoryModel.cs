using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHerbivoreMemoryModel : IMemoryModel
{
    public void AddPlantMemory(Plant plant);
    public List<Plant> GetPlantsInMemory();
}
