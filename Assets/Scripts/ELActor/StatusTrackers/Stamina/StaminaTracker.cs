using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaTracker : StatusTracker<StaminaTracker.StaminaStatus>
{
    [SerializeField] private float recoveryRate;
    [SerializeField] private float recoveryDelay;
    [SerializeField] private uint tiredPercentage = 50;
    [SerializeField] private uint exhaustedPercentage = 10;

    private float recoveryDelayTimer;
    private float recoveryPercentagePenalty = 0f;

    private float prevValue;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        this.status = new Observable<StaminaStatus>(StaminaStatus.ENERGIZED);
        this.recoveryDelayTimer = this.recoveryDelay;
        this.prevValue = this.GetCurrent();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.IsPaused()) return;

        if (this.prevValue > this.GetCurrent())
        {
            // Stamina was used, wait for delay before recovering stamina
            this.recoveryDelayTimer = 0;
        }
        this.prevValue = this.GetCurrent();

        if ((this.recoveryDelayTimer += Time.deltaTime) >= this.recoveryDelay)
        {
            // Clamp timer to delay time (to prevent overflow)
            this.recoveryDelayTimer = this.recoveryDelay + 1f;

            // Regenerate Health
            this.current = Mathf.Min(this.current + ((this.recoveryRate * ((100 - this.recoveryPercentagePenalty) / 100)) * Time.deltaTime), this.max);
        }

        if (this.GetCurrentPercentage() < this.exhaustedPercentage)
        {
            this.status.Set(StaminaStatus.EXHAUSTED);
        }
        else if (this.GetCurrentPercentage() < this.tiredPercentage)
        {
            this.status.Set(StaminaStatus.TIRED);
        }
        else
        {
            this.status.Set(StaminaStatus.ENERGIZED);
        }
    }

    public void SetRecoveryPenalty(float penaltyPercentage)
    {
        this.recoveryPercentagePenalty = penaltyPercentage;
    }

    public float GetTiredPercentage()
    {
        return this.tiredPercentage;
    }

    public float GetExhaustedPercentage()
    {
        return this.exhaustedPercentage;
    }

    public enum StaminaStatus
    {
        ENERGIZED,
        TIRED,
        EXHAUSTED
    }
}
