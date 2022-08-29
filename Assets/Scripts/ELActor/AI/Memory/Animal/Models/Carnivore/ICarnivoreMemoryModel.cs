using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICarnivoreMemoryModel : IMemoryModel
{
    public void AddPreyMemory(Tuple<Animal, Vector3> animalAndPosition);
    public List<Tuple<Animal, Vector3>> GetPreyInMemory();
}
