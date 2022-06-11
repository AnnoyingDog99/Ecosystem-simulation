using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plant : ELActor
{
    // The scale percentage this plant needs to be considered alive
    [SerializeField] int minimumScalePercentage = 1;
    [SerializeField] private int growTime = 10;
    [SerializeField] private int growthRecoveryTime = 10;
    private Vector3 growthStep;
    private float growthRecoveryTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        base.SetScale(this.minScale);
        this.growthStep = (this.GetMaxScale() - this.GetMinScale()) / growTime;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.isDead) return;
        growthRecoveryTimer -= Time.deltaTime;
        if (this.HasRecovered() && !this.IsFullyGrown())
        {
            base.SetScale(base.GetScale() + (this.growthStep * Time.deltaTime));
        }
    }

    private bool HasRecovered()
    {
        return this.growthRecoveryTimer <= 0;
    }

    public override float GetEaten(float biteSize)
    {
        growthRecoveryTimer = growthRecoveryTime;
        float eatenFoodPoints = Mathf.Min(biteSize, GetCurrentFoodPoints());
        int percentageEaten = Mathf.RoundToInt((100 / this.foodPoints) * biteSize);
        this.Shrink(percentageEaten);
        if (GetGrowthPercent() <= minimumScalePercentage)
        {
            this.isDead = true;
        }
        return eatenFoodPoints;
    }
}
