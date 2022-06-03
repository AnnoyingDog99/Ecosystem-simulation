using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimalMemory : ELActorMemory
{
    [SerializeField] private float predatorMemorySpan;
    [SerializeField] private float ownKindMemorySpan;
    [SerializeField] private float obstacleMemorySpan;

    private List<Memory<ELActor>> predators = new List<Memory<ELActor>>();
    protected List<Memory<ELActor>> ownKind = new List<Memory<ELActor>>();
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

    public void AddPredatorMemory(Animal predator) {
        AddELActorMemory(predator, this.predatorMemorySpan, this.predators);
    }

    public List<ELActor> GetPredatorsInMemory() {
        return predators.ConvertAll((fragment) => fragment.GetMemoryContent());
    }

    public void AddObstacleMemory(Collider obstacle) {
        // Check if memory doesn´t already exist
        Memory<Collider> existingMemory = obstacles.Find((memory) => memory.GetMemoryContent().gameObject.GetInstanceID() == obstacle.gameObject.GetInstanceID());
        if (existingMemory != null) {
            // Refresh existing memory instead of adding new one
            existingMemory.Refresh();
            return;
        }
        
        obstacles.Add(new Memory<Collider>(obstacle, this.obstacleMemorySpan));
    }

    public List<Collider> GetObstaclesInMemory() {
        return obstacles.ConvertAll((fragment) => fragment.GetMemoryContent());
    }
}
