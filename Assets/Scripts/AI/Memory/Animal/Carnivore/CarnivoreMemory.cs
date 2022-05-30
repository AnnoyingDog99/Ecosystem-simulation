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
}
