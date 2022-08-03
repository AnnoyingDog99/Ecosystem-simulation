using UnityEngine;
public class Grass : Plant, IGrass
{
    protected override void Start()
    {
        base.Start();

        // Set the max health equal to the amount of food points
        this.GetPlantHealthController().GetHealthTracker().SetMax(this.GetMaxFoodPoints());

    }

    protected override void Update()
    {
        base.Update();
        if (this.GetPlantHealthController().IsDead()) return;

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