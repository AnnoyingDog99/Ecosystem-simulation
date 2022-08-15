using UnityEngine;
public class Grass : Plant, IGrass
{
    [SerializeField] private float growTime;
    [SerializeField] private float growthRecoveryTime;

    protected override void Start()
    {
        base.Start();

        // Set the max health equal to the amount of food points
        this.GetPlantHealthController().GetHealthTracker().SetMax(this.GetMaxFoodPoints());

        this.SetScale(this.GetScaleBasedOnHealth());

    }

    protected override void Update()
    {
        base.Update();
        if (this.GetPlantHealthController().IsDead()) return;

        float maxDistance = Vector3.Distance(this.GetMinScale(), GetMaxScale());
        float currentDistance = Vector3.Distance(this.GetScale(), GetMaxScale());

        // Keep the size of the bounding box the same regardless of the actual scale of the object
        this.GetBoundingBox().size = this.GetMaxScale() * (GetMaxScale().x / GetScale().x);

        // Set the amount food points equal to the amount of health
        this.SetCurrentFoodPoints(this.GetPlantHealthController().GetHealthTracker().GetCurrent());

        // Scale grass according to health
        // Use ScaleOverTime for a smoother transition
        this.GetActorScaleController().ScaleOverTime(this.GetScaleBasedOnHealth(), 0.1f);
    }

    protected override void FirstUpdate()
    {
        base.FirstUpdate();
        this.GetActorScaleController().SetScale(this.GetScaleBasedOnHealth());
    }

    private Vector3 GetScaleBasedOnHealth()
    {
        return this.GetMaxScale() * (this.GetPlantHealthController().GetHealthTracker().GetCurrentPercentage() / 100f);
    }

    public override float GetEaten(float biteSize)
    {
        this.GetDamaged(biteSize);
        return base.GetEaten(biteSize);
    }
}