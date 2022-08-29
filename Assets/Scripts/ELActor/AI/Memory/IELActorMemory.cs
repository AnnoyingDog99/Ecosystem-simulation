using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IELActorMemory
{
    public void UpdateMemories<T>(params List<Memory<T>>[] memoriesCollection);

    public void UpdateMemories<T>(List<Memory<T>> memories);

    public void AddELActorMemory(ELActor actor, float memorySpan, List<Memory<ELActor>> memoriesCollection);
}
