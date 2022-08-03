using UnityEngine;
using System.Collections.Generic;
public interface IField
{
    public void Spread();

    public Plant GetReferencePlant();

    public List<Plant> GetPlants();

    public int GetMaxAmountOfPlants();

    public Vector2 GetSize();

    public Vector3 GetPosition();

    public float GetSpreadTime();
}