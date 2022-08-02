using UnityEngine;
using UnityEngine.AI;

public class AnimalHealthController : Controller
{
    [SerializeField] private HealthTracker healthTracker;

    private IDamageableAnimal animal;

    private void Start()
    {
        this.animal = GetComponentInParent<IDamageableAnimal>();
        this.GetHealthTracker().GetStatus().Subscribe((HealthTracker.HealthStatus status) =>
        {
            if (status == HealthTracker.HealthStatus.DEAD)
            {
                this.Die();
            }
        });
    }

    protected override void Update()
    {
        base.Update();
    }

    public HealthTracker GetHealthTracker()
    {
        return this.healthTracker;
    }

    public bool IsDead()
    {
        return this.animal.GetAnimalAnimator().GetIsDeadBool();
    }

    public void Die()
    {
        this.GetHealthTracker().GetDamaged(this.GetHealthTracker().GetCurrent());
        this.GetHealthTracker().Pause();
        this.animal.GetAnimalAnimator().SetIsDeadBool(true);
        this.animal.OnDeath();
    }

    public void GetDamaged(float damage)
    {
        this.GetHealthTracker().GetDamaged(damage);
    }
}