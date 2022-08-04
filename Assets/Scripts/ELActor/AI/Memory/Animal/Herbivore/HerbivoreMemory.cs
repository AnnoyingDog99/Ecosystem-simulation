using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// TODO: Convert to interface?
public class HerbivoreMemory : AnimalMemory
{
    // [SerializeField] private float plantMemorySpan;
    // protected List<Memory<Plant>> plants = new List<Memory<Plant>>();

    // // Start is called before the first frame update
    // protected override void Start()
    // {
    //     base.Start();
    // }

    // // Update is called once per frame
    // protected override void Update()
    // {
    //     base.Update();
    //     UpdateMemories(plants);
    // }

    // public void AddPlantMemory(Plant plant)
    // {
    //     // Check if memory doesn't already exist, filter out plants that were destroyed
    //     Memory<Plant> existingMemory = this.plants.FindAll((memory) => Director.Instance.ActorExists(memory.GetMemoryContent())).Find((memory) => memory.GetMemoryContent().GetID() == plant.GetID());
    //     if (existingMemory != null)
    //     {
    //         // Refresh existing memory instead of adding new one
    //         existingMemory.Refresh();
    //         return;
    //     }

    //     plants.Add(new Memory<Plant>(plant, plantMemorySpan));
    // }

    // public List<Plant> GetPlantsInMemory()
    // {
    //     // Return plants, filter out plants that were destroyed
    //     return plants.ConvertAll((fragment) => fragment.GetMemoryContent()).FindAll((plant) => Director.Instance.ActorExists(plant));
    // }
}
