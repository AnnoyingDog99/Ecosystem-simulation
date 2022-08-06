using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimalMemory : ELActorMemory, IAnimalMemory
{
    [SerializeField] private float predatorMemorySpan;
    [SerializeField] private float ownKindMemorySpan;
    [SerializeField] private float obstacleMemorySpan;

    private List<Memory<Animal>> predators = new List<Memory<Animal>>();
    protected List<Memory<Animal>> ownKind = new List<Memory<Animal>>();
    protected List<Memory<Sight.ObstacleLocation>> obstacles = new List<Memory<Sight.ObstacleLocation>>();

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
        // Check if memory doesn't already exist
        Memory<Animal> existingMemory = this.predators.Find((memory) => memory.GetMemoryContent().GetID() == predator.GetID());
        if (existingMemory != null)
        {
            // Refresh existing memory instead of adding new one
            existingMemory.Refresh();
            return;
        }

        this.predators.Add(new Memory<Animal>(predator, predatorMemorySpan));
        // Director.Instance.ActorExists(memory.GetMemoryContent())).Find((memory) =>
    }

    public List<Animal> GetPredatorsInMemory()
    {
        this.predators = this.predators.FindAll((fragment) => Director.Instance.ActorExists(fragment.GetMemoryContent()));
        return this.predators.ConvertAll((fragment) => fragment.GetMemoryContent());
    }

    public void AddOwnKindMemory(Animal ownKind)
    {
        // Check if memory doesn't already exist
        Memory<Animal> existingMemory = this.ownKind.Find((memory) => memory.GetMemoryContent().GetID() == ownKind.GetID());
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
        this.ownKind = this.ownKind.FindAll((fragment) => Director.Instance.ActorExists(fragment.GetMemoryContent()));
        return this.ownKind.ConvertAll((fragment) => fragment.GetMemoryContent());
    }

    public void AddObstacleMemory(Sight.ObstacleLocation obstacle)
    {
        // Check if memory doesn´t already exist
        Memory<Sight.ObstacleLocation> existingMemory = this.obstacles.Find((memory) => memory.GetMemoryContent().transform.gameObject.GetInstanceID() == obstacle.transform.gameObject.GetInstanceID());
        if (existingMemory != null)
        {
            // Refresh existing memory instead of adding new one
            existingMemory.Refresh();
            return;
        }

        this.obstacles.Add(new Memory<Sight.ObstacleLocation>(obstacle, this.obstacleMemorySpan));
    }

    public List<Sight.ObstacleLocation> GetObstaclesInMemory()
    {
        return this.obstacles.ConvertAll((fragment) => fragment.GetMemoryContent());
    }
}
