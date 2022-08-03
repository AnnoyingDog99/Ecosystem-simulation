using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ELActorHealthController : Controller
{
    [SerializeField] private HealthTracker healthTracker;

    private IDamageable actor;

    private List<DamageRepeatedly> damageRepeatedlySources = new List<DamageRepeatedly>();
    private class DamageRepeatedly
    {
        public Identifier identifier;
        public float damageRepeatedlyDamage;
        public float damageRepeatedlyTimer;
        public float damageRepeatedlyInterval;
        public uint damageRepeatedlyTimes;
        public uint originalDamageRepeatedlyTimes;
    };

    protected override void Start()
    {
        base.Start();
        this.actor = GetComponentInParent<IDamageable>();
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

        if (this.IsDead() && this.damageRepeatedlySources.Count > 0)
        {
            this.damageRepeatedlySources.Clear();
        }
        for (int i = this.damageRepeatedlySources.Count - 1; i >= 0; i--)
        {
            DamageRepeatedly damageRepeatedly = damageRepeatedlySources[i];
            if (damageRepeatedly.damageRepeatedlyTimes == 0)
            {
                this.damageRepeatedlySources.RemoveAt(i);
                continue;
            }
            if ((damageRepeatedly.damageRepeatedlyTimer += Time.deltaTime) < damageRepeatedly.damageRepeatedlyInterval)
            {
                continue;
            }
            this.GetDamaged(damageRepeatedly.damageRepeatedlyDamage);
            damageRepeatedly.damageRepeatedlyTimes--;
            damageRepeatedly.damageRepeatedlyTimer = 0;
        }
    }

    public HealthTracker GetHealthTracker()
    {
        return this.healthTracker;
    }

    public virtual bool IsDead()
    {
        return this.actor.GetActorAnimator().GetIsDeadBool();
    }

    public virtual void Die()
    {
        this.GetHealthTracker().GetDamaged(this.GetHealthTracker().GetCurrent());
        this.GetHealthTracker().Pause();
        this.actor.GetActorAnimator().SetIsDeadBool(true);
        this.actor.OnDeath();
    }

    public virtual void GetDamaged(float damage)
    {
        this.GetHealthTracker().GetDamaged(damage);
    }

    public virtual Identifier GetDamagedRepeatedly(float damage, float interval, uint times)
    {
        // Get damaged for {damage}, each {interval} seconds, {times} times.
        DamageRepeatedly damageRepeatedly = new DamageRepeatedly();
        damageRepeatedly.identifier = Identifier.GetIdentifier();
        damageRepeatedly.damageRepeatedlyDamage = damage;
        damageRepeatedly.damageRepeatedlyInterval = interval;
        damageRepeatedly.originalDamageRepeatedlyTimes = times;
        damageRepeatedly.damageRepeatedlyTimes = times;
        damageRepeatedly.damageRepeatedlyTimer = interval;
        this.damageRepeatedlySources.Add(damageRepeatedly);
        return damageRepeatedly.identifier;
    }

    public virtual void RestartDamagedRepeatedly(Identifier identifier)
    {
        // Restart damage repeatedly
        DamageRepeatedly damageRepeatedly = this.damageRepeatedlySources.Find((source) => source.identifier == identifier);
        if (damageRepeatedly == null) return;
        damageRepeatedly.damageRepeatedlyTimes = damageRepeatedly.originalDamageRepeatedlyTimes;
    }

    public virtual void StopDamagedRepeatedly(Identifier identifier)
    {
        DamageRepeatedly damageRepeatedly = this.damageRepeatedlySources.Find((source) => source.identifier == identifier);
        if (damageRepeatedly == null) return;
        this.damageRepeatedlySources.Remove(damageRepeatedly);
    }
}