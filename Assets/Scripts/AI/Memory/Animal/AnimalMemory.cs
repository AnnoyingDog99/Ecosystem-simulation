using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimalMemory : ELActorMemory
{
    [SerializeField] private float predatorMemorySpan;
    [SerializeField] private float ownKindMemorySpan;
    [SerializeField] private float obstacleMemorySpan;

    private List<Memory<Animal>> predators = new List<Memory<Animal>>();
    protected List<Memory<Animal>> ownKind = new List<Memory<Animal>>();
    protected List<Memory<Collider>> obstacles = new List<Memory<Collider>>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        UpdateMemories(predators, ownKind);
        UpdateMemories(obstacles);
    }

    public void AddPredatorMemory(Animal predator)
    {
        // Check if memory doesn't already exist, filter out predators that were destroyed
        Memory<Animal> existingMemory = this.predators.FindAll((memory) => memory.GetMemoryContent() != null).Find((memory) => memory.GetMemoryContent().GetID() == predator.GetID());
        if (existingMemory != null)
        {
            // Refresh existing memory instead of adding new one
            existingMemory.Refresh();
            return;
        }

        this.predators.Add(new Memory<Animal>(predator, predatorMemorySpan));
    }

    public List<Animal> GetPredatorsInMemory()
    {
        // Return predators, filter out predators that were destroyed
        return this.predators.ConvertAll((fragment) => fragment.GetMemoryContent()).FindAll((predator) => predator != null);
    }

    public void AddOwnKindMemory(Animal ownKind)
    {
        // Check if memory doesn't already exist, filter out own kind that were destroyed
        Memory<Animal> existingMemory = this.ownKind.FindAll((memory) => memory.GetMemoryContent() != null).Find((memory) => memory.GetMemoryContent().GetID() == ownKind.GetID());
        if (existingMemory != null)
        {
            // Refresh existing memory instead of adding new one
            existingMemory.Refresh();
            return;
        }

        this.ownKind.Add(new Memory<Animal>(ownKind, predatorMemorySpan));
    }

    public List<Animal> GetOwnKindInMemory()
    {
        // Return predators, filter out own kind that were destroyed
        return this.ownKind.ConvertAll((fragment) => fragment.GetMemoryContent()).FindAll((ownKind) => ownKind != null);
    }

    public void AddObstacleMemory(Collider obstacle)
    {
        // Check if memory doesn´t already exist
        Memory<Collider> existingMemory = this.obstacles.Find((memory) => memory.GetMemoryContent().gameObject.GetInstanceID() == obstacle.gameObject.GetInstanceID());
        if (existingMemory != null)
        {
            // Refresh existing memory instead of adding new one
            existingMemory.Refresh();
            return;
        }

        this.obstacles.Add(new Memory<Collider>(obstacle, this.obstacleMemorySpan));
    }

    public List<Collider> GetObstaclesInMemory()
    {
        return this.obstacles.ConvertAll((fragment) => fragment.GetMemoryContent());
    }
}
