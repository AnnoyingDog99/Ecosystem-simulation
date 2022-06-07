using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : ELActor
{
    [SerializeField] Vector3 startScale = new Vector3(0, 0, 0);
    [SerializeField] Vector3 endScale = new Vector3(1, 1, 10);
    [SerializeField] int growTime = 10;
    [SerializeField] int totalFoodPoints = 10;
    public bool isDead { get; private set; } = false;

    private Vector3 growthStep;
    private int growthRecoveryTime = 10;
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
        growthRecoveryTimer -= Time.deltaTime;
        if (HasRecovered() && !this.IsFullyGrown())
        {
            base.SetScale(base.GetScale() + (this.growthStep * Time.deltaTime));
        }
        if (this.IsFullyGrown())
        {
            this.Eat(1);
            this.Eat(1);
            this.Eat(1);
            this.Eat(1);
            this.Eat(1);
        }
    }

    public void Shrink(int percentage)
    {
        base.SetScale(base.GetScale() - ((this.endScale / 100) * percentage));
    }

    public int GetCurrentFoodPoints()
    {
        return (this.totalFoodPoints / 100) * this.GetGrowthPercent(); ;
    }

    public int GetGrowthPercent()
    {
        float max_distance = Vector3.Distance(this.endScale, this.startScale);
        float current_distance = Vector3.Distance(this.endScale, base.GetScale());
        return (int)Mathf.RoundToInt((100 / max_distance) * (max_distance - current_distance));
    }

    public bool IsFullyGrown()
    {
        return this.GetGrowthPercent() >= 100;
    }

    private bool HasRecovered()
    {
        return this.growthRecoveryTimer <= 0;
    }

    public virtual float Eat(float biteSize)
    {
        growthRecoveryTimer = growthRecoveryTime;
        float eatenFoodPoints = this.totalFoodPoints * (biteSize / 10);
        int percentageEaten = (int)Mathf.RoundToInt((100 / this.totalFoodPoints) * eatenFoodPoints);
        this.Shrink(percentageEaten);
        if (GetGrowthPercent() <= 0)
        {
            this.isDead = true;
        }
        return eatenFoodPoints;
    }
}
