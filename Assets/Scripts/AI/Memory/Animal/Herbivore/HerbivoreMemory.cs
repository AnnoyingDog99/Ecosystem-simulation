using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HerbivoreMemory : AnimalMemory
{
    [SerializeField] private float plantMemorySpan;
    protected List<Memory<ELActor>> plants = new List<Memory<ELActor>>();
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        UpdateMemories(plants);
    }
}
