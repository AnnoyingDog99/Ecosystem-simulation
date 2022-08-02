using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : StatusTracker<HealthTracker.HealthStatus>
{
    [SerializeField] private float regenRate = 1f;
    [SerializeField] private float regenDelay = 5f;
    [SerializeField] private uint hurtPercentage = 50;
    [SerializeField] private uint dyingPercentage = 10;

    private float regenDelayTimer;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        this.regenDelayTimer = this.regenDelay;
        this.status = new Observable<HealthStatus>(HealthStatus.HEALTHY);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.IsPaused()) return;

        if ((this.regenDelayTimer += Time.deltaTime) >= this.regenDelay)
        {
            // Clamp timer to delay time (to prevent overflow)
            this.regenDelayTimer = this.regenDelay + 1f;

            // Regenerate Health
            this.current = Mathf.Min(this.current + (this.regenRate * Time.deltaTime), this.max);
        }

        if (this.GetCurrentPercentage() <= 0)
        {
            this.status.Set(HealthStatus.DEAD);
        }
        if (this.GetCurrentPercentage() < this.dyingPercentage)
        {
            this.status.Set(HealthStatus.DYING);
        }
        else if (this.GetCurrentPercentage() < this.hurtPercentage)
        {
            this.status.Set(HealthStatus.HURT);
        }
        else
        {
            this.status.Set(HealthStatus.HEALTHY);
        }
    }

    public void GetDamaged(float damage)
    {
        if (damage < 0) return;
        this.current = Mathf.Max(0, this.current - damage);
        this.regenDelayTimer = 0;
    }

    public void AddHealth(float health)
    {
        if (health < 0) return;
        this.current = Mathf.Min(this.GetMax(), this.current + health);
    }

    public bool IsRegenerating()
    {
        return this.regenDelayTimer >= this.regenDelay;
    }

    public enum HealthStatus
    {
        HEALTHY,
        HURT,
        DYING,
        DEAD
    }
}
