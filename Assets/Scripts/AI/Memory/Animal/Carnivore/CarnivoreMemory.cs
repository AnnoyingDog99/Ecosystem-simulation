using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        // Check if memory doesn´t already exist
        Memory<Tuple<Animal, Vector3>> existingMemory = this.prey.Find((memory) => memory.GetMemoryContent().Item1.GetID() == animalAndPosition.Item1.GetID());
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
        return prey.ConvertAll((fragment) => fragment.GetMemoryContent());
    }
}
