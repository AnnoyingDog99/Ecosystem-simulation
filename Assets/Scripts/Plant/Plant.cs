using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plant : ELActor
{
    [SerializeField] Vector3 startScale = new Vector3(0, 0, 0);

    // The scale percentage this plant needs to be considered alive
    [SerializeField] int minimumScalePercentage = 1;
    [SerializeField] Vector3 endScale = new Vector3(1, 1, 10);
    [SerializeField] private int growTime = 10;
    [SerializeField] private int growthRecoveryTime = 10;
    private Vector3 growthStep;
    private float growthRecoveryTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        base.SetScale(this.startScale);
        this.growthStep = (endScale - startScale) / growTime;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.isDead) return;
        if (this.IsFullyGrown()) return;
        growthRecoveryTimer -= Time.deltaTime;
        if (HasRecovered() && !this.IsFullyGrown())
        {
            base.SetScale(base.GetScale() + (this.growthStep * Time.deltaTime));
        }
    }

    public void Shrink(int percentage)
    {
        base.SetScale(base.GetScale() - ((this.endScale / 100) * percentage));
    }

    public int GetCurrentFoodPoints()
    {
        return Mathf.RoundToInt(((float)this.foodPoints / 100) * (float)this.GetGrowthPercent());
    }

    public int GetGrowthPercent()
    {
        float max_distance = Vector3.Distance(this.endScale, this.startScale);
        float current_distance = Vector3.Distance(this.endScale, base.GetScale());
        return Mathf.RoundToInt((100 / max_distance) * (max_distance - current_distance));
    }

    public bool IsFullyGrown()
    {
        return this.GetGrowthPercent() >= 100;
    }

    private bool HasRecovered()
    {
        return this.growthRecoveryTimer <= 0;
    }

    public override float Eat(float biteSize)
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

    public Vector3 GetStartScale()
    {
        return this.startScale;
    }

    public Vector3 GetEndScale()
    {
        return this.endScale;
    }
}
