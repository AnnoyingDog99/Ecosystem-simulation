using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plant : ELActor, IPlant
{
    [SerializeField] private float growTime;
    [SerializeField] private float growthRecoveryTime;
    [SerializeField] private NutritionalModel nutritionalModel;
    private ELActorHealthController _plantHealthController;

    protected override void Start()
    {
        base.Start();
        this._plantHealthController = GetComponent<ELActorHealthController>();
    }

    protected override void Update()
    {
        base.Update();
        if (this.GetPlantHealthController().IsDead()) return;
    }

    protected override void FirstUpdate()
    {
        base.FirstUpdate();
    }

    public void OnDeath()
    {
        Debug.Log("Dead");
    }

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

    public void SetCurrentFoodPoints(float foodPoints)
    {
        this.nutritionalModel.SetCurrentFoodPoints(foodPoints);
    }

    public void GetDamaged(float damage)
    {
        this.GetPlantHealthController().GetDamaged(damage);
    }

    public ELActorHealthController GetPlantHealthController()
    {
        return this._plantHealthController;
    }

}