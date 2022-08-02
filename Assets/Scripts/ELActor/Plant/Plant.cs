using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plant : ELActor, IPlant
{
    [SerializeField] private NutritionalModel nutritionalModel;

    public virtual float GetEaten(float biteSize) 
    {
        return this.nutritionalModel.GetEaten(biteSize);
    }
    public float GetMaxFoodPoints() 
    {
        return this.nutritionalModel.GetMaxFoodPoints();
    }
    public float GetCurrentFoodPoints() 
    {
        return this.nutritionalModel.GetCurrentFoodPoints();
    }
}