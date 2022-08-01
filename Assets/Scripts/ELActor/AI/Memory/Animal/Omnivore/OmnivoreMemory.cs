using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OmnivoreMemory : AnimalMemory
{
    [SerializeField] HerbivoreMemory herbivoreMemory;
    [SerializeField] CarnivoreMemory carnivoreMemory;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
