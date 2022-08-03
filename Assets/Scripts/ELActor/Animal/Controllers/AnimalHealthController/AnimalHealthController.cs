using UnityEngine;
using UnityEngine.AI;

public class AnimalHealthController : ELActorHealthController
{
    private Animal animal;

    private bool isStarving = false;
    private int starvingDamageIdentifier = -1;

    protected override void Start()
    {
        base.Start();
        this.animal = GetComponentInParent<Animal>();
    }

    protected override void Update()
    {
        base.Update();

        if (this.isStarving)
        {
            if (this.starvingDamageIdentifier < 0)
            {
                this.starvingDamageIdentifier = this.animal.GetActorHealthController().GetDamagedRepeatedly(0.5f, 2f, 5);
            }
            this.animal.GetActorHealthController().RestartDamagedRepeatedly(this.starvingDamageIdentifier);
        }
        else if (this.starvingDamageIdentifier >= 0)
        {
            this.animal.GetActorHealthController().StopDamagedRepeatedly(this.starvingDamageIdentifier);
            this.starvingDamageIdentifier = -1;
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
        });
    }

    public override void Die()
    {
        base.Die();
    }
}