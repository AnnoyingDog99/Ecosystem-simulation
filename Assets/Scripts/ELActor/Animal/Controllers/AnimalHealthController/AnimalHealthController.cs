using UnityEngine;
using UnityEngine.AI;

public class AnimalHealthController : ELActorHealthController
{
    private Animal animal;

    private bool isStarving = false;
    private Identifier starvingDamageIdentifier = null;

    protected override void Start()
    {
        base.Start();
        this.animal = GetComponentInParent<Animal>();
    }

    protected override void Update()
    {
        base.Update();
        if (this.IsDead())
        {
            this.isStarving = false;
        }

        if (this.isStarving)
        {
            if (this.starvingDamageIdentifier == null)
            {
                this.starvingDamageIdentifier = this.animal.GetActorHealthController().GetDamagedRepeatedly(0.5f, 2f, 2);
            }
            this.animal.GetActorHealthController().RestartDamagedRepeatedly(this.starvingDamageIdentifier);
        }
        else if (this.starvingDamageIdentifier != null)
        {
            this.animal.GetActorHealthController().StopDamagedRepeatedly(this.starvingDamageIdentifier);
            this.starvingDamageIdentifier = null;
        }
    }

    protected override void FirstUpdate()
    {
        base.FirstUpdate();

        this.animal.GetAnimalAgeController().GetAgeTracker().GetStatus().Subscribe((AgeTracker.AgeStatus status) =>
        {
            if (status == AgeTracker.AgeStatus.MAX)
            {
                this.Die();
            }
        });

        this.animal.GetAnimalHungerController().GetHungerTracker().GetStatus().Subscribe((HungerTracker.HungerStatus status) =>
        {
            if (status == HungerTracker.HungerStatus.STARVING)
            {
                this.isStarving = true;
            }
            else
            {
                this.isStarving = false;
            }
            switch (status)
            {
                case HungerTracker.HungerStatus.STARVING:
                    this.GetHealthTracker().SetRegenPenalty(100f);
                    break;
                case HungerTracker.HungerStatus.HUNGRY:
                    this.GetHealthTracker().SetRegenPenalty(50f);
                    break;
                case HungerTracker.HungerStatus.SATISFIED:
                default:
                    this.GetHealthTracker().SetRegenPenalty(0f);
                    break;
            }
        });
    }

    public override void Die()
    {
        base.Die();
    }
}