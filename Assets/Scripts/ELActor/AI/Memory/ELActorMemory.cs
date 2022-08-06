using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ELActorMemory : MonoBehaviour, IELActorMemory
{
    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    public void UpdateMemories<T>(params List<Memory<T>>[] memoriesCollection)
    {
        foreach (List<Memory<T>> memories in memoriesCollection)
        {
            UpdateMemories(memories);
        }
    }
    public void UpdateMemories<T>(List<Memory<T>> memories)
    {
        // Remove elapsed memories
        for (int i = memories.Count - 1; i >= 0; i--)
        {
            if (!memories[i].HasElapsed()) continue;
            memories.RemoveAt(i);
        }
    }

    public void AddELActorMemory(ELActor actor, float memorySpan, List<Memory<ELActor>> memoriesCollection)
    {
        // Check if memory doesn´t already exist
        Memory<ELActor> existingMemory = memoriesCollection.Find((memory) => memory.GetMemoryContent().GetID() == actor.GetID());
        if (existingMemory != null)
        {
            // Refresh existing memory instead of adding new one
            existingMemory.Refresh();
            return;
        }

        memoriesCollection.Add(new Memory<ELActor>(actor, memorySpan));
    }
}
