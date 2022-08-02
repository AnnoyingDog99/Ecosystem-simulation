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
        this.status = new Observable<HealthStatus>(HealthStatus.HEALTHY);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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

        this.current -= (2.5f * Time.deltaTime);
    }

    public enum HealthStatus
    {
        HEALTHY,
        HURT,
        DYING
    }
}
