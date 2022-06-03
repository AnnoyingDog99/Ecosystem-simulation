using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELActorMemory : MonoBehaviour
{
    protected class Memory<T>
    {
        private T content;
        private float startTime;
        private float memorySpan;

        public Memory(T content, float memorySpan)
        {
            this.content =  content;
            this.memorySpan = memorySpan;
            this.Refresh();
        }

        public void SetMemoryContent(T content)
        {
            this.content = content;
        }

        public T GetMemoryContent()
        {
            return this.content;
        }

        public void Refresh()
        {
            this.startTime = Time.time;
        }

        public bool HasElapsed()
        {
            return Time.time > this.startTime + this.memorySpan;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected void UpdateMemories<T>(params List<Memory<T>>[] memoriesCollection)
    {
        foreach (List<Memory<T>> memories in memoriesCollection)
        {
            UpdateMemories(memories);
        }
    }
    protected void UpdateMemories<T>(List<Memory<T>> memories)
    {
        // Remove elapsed memories
        for (int i = memories.Count - 1; i >= 0; i--)
        {
            if (!memories[i].HasElapsed()) continue;
            memories.RemoveAt(i);
        }
    }

    protected void AddELActorMemory(ELActor actor, float memorySpan, List<Memory<ELActor>> memoriesCollection)
    {
        // Check if memory doesn´t already exist
        Memory<ELActor> existingMemory = memoriesCollection.Find((memory) => memory.GetMemoryContent().GetID() == actor.GetID());
        if (existingMemory != null) {
            // Refresh existing memory instead of adding new one
            existingMemory.Refresh();
            return;
        }
        
        memoriesCollection.Add(new Memory<ELActor>(actor, memorySpan));
    }
}
