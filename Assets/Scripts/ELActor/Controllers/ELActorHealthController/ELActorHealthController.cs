using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ELActorHealthController : Controller
{
    [SerializeField] private HealthTracker healthTracker;

    private IDamageable actor;

    private List<DamageRepeatedlyStruct> damageRepeatedlySources = new List<DamageRepeatedlyStruct>();
    private class DamageRepeatedlyStruct
    {
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
            DamageRepeatedlyStruct damageRepeatedlyStruct = damageRepeatedlySources[i];
            if (damageRepeatedlyStruct.damageRepeatedlyTimes == 0)
            {
                this.damageRepeatedlySources.RemoveAt(i);
                continue;
            }
            if ((damageRepeatedlyStruct.damageRepeatedlyTimer += Time.deltaTime) < damageRepeatedlyStruct.damageRepeatedlyInterval)
            {
                continue;
            }
            this.GetDamaged(damageRepeatedlyStruct.damageRepeatedlyDamage);
            damageRepeatedlyStruct.damageRepeatedlyTimes--;
            damageRepeatedlyStruct.damageRepeatedlyTimer = 0;
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

    public virtual int GetDamagedRepeatedly(float damage, float interval, uint times)
    {
        // Get damaged for {damage}, each {interval} seconds, {times} times.
        DamageRepeatedlyStruct damageRepeatedlyStruct = new DamageRepeatedlyStruct();
        damageRepeatedlyStruct.damageRepeatedlyDamage = damage;
        damageRepeatedlyStruct.damageRepeatedlyInterval = interval;
        damageRepeatedlyStruct.originalDamageRepeatedlyTimes = times;
        damageRepeatedlyStruct.damageRepeatedlyTimes = times;
        damageRepeatedlyStruct.damageRepeatedlyTimer = interval;
        this.damageRepeatedlySources.Add(damageRepeatedlyStruct);
        return this.damageRepeatedlySources.Count - 1;
    }

    public virtual void RestartDamagedRepeatedly(int index)
    {
        // Restart damage repeatedly
        if (index >= this.damageRepeatedlySources.Count) return;
        DamageRepeatedlyStruct damageRepeatedlyStruct = this.damageRepeatedlySources[index];
        damageRepeatedlyStruct.damageRepeatedlyTimes = damageRepeatedlyStruct.originalDamageRepeatedlyTimes;
    }

    public virtual void StopDamagedRepeatedly(int index)
    {
        if (index >= this.damageRepeatedlySources.Count) return;
        this.damageRepeatedlySources.RemoveAt(index);
    }
}