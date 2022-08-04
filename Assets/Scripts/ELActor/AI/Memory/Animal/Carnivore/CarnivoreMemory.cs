using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// TODO: Convert to interface?
public class CarnivoreMemory : AnimalMemory
{
    [SerializeField] private float preyMemorySpan;

    // Memory containing prey and the last known position of that prey
    protected List<Memory<Tuple<Animal, Vector3>>> prey = new List<Memory<Tuple<Animal, Vector3>>>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        UpdateMemories(prey);
    }

    public void AddPreyMemory(Tuple<Animal, Vector3> animalAndPosition)
    {
        // Check if memory doesn´t already exist, filter out prey that were destroyed
        Memory<Tuple<Animal, Vector3>> existingMemory = this.prey.FindAll((memory) => Director.Instance.ActorExists(memory.GetMemoryContent().Item1)).Find((memory) => memory.GetMemoryContent().Item1.GetID() == animalAndPosition.Item1.GetID());
        if (existingMemory != null)
        {
            // Refresh existing memory instead of adding new one
            // Rest the last seen position aswell
            existingMemory.SetMemoryContent(animalAndPosition);
            existingMemory.Refresh();
            return;
        }

        this.prey.Add(new Memory<Tuple<Animal, Vector3>>(animalAndPosition, preyMemorySpan));
    }

    public List<Tuple<Animal, Vector3>> GetPreyInMemory()
    {
        // Return prey, filter out prey that were destroyed
        return prey.ConvertAll((fragment) => fragment.GetMemoryContent()).FindAll((prey) => Director.Instance.ActorExists(prey.Item1));
    }
}
