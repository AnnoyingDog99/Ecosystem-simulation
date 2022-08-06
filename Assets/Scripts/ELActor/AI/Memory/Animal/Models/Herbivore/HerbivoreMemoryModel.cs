using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HerbivoreMemoryModel : MonoBehaviour, IHerbivoreMemoryModel
{
    [SerializeField] private float plantMemorySpan;
    protected List<Memory<Plant>> plants = new List<Memory<Plant>>();

    public void UpdateMemories(ELActorMemory parent)
    {
        parent.UpdateMemories(this.plants);
    }

    public void AddPlantMemory(Plant plant)
    {
        // Check if memory doesn't already exist, filter out plants that were destroyed
        Memory<Plant> existingMemory = this.plants.FindAll((memory) => Director.Instance.ActorExists(memory.GetMemoryContent())).Find((memory) => memory.GetMemoryContent().GetID() == plant.GetID());
        if (existingMemory != null)
        {
            // Refresh existing memory instead of adding new one
            existingMemory.Refresh();
            return;
        }

        plants.Add(new Memory<Plant>(plant, this.plantMemorySpan));
    }

    public List<Plant> GetPlantsInMemory()
    {
        // Return plants, filter out plants that were destroyed
        return plants.ConvertAll((fragment) => fragment.GetMemoryContent()).FindAll((plant) => Director.Instance.ActorExists(plant));
    }
}
