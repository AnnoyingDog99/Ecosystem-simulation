using UnityEngine;

public class AnimalMovementController : ELActorMovementController
{
    [SerializeField] private StaminaTracker staminaTracker;
    private Animal animal;
    protected override void Start()
    {
        base.Start();
        this.animal = GetComponentInParent<Animal>();
    }

    public override void Idle()
    {
        base.Idle();
    }

    public override bool IsIdle()
    {
        return base.IsIdle();
    }

    public StaminaTracker GetStaminaTracker()
    {
        return this.staminaTracker;
    }
}