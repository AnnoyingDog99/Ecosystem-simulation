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

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        this.state = new Observable<StaminaStatus>(StaminaStatus.ENERGIZED);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.GetCurrentPercentage() < this.exhaustedPercentage)
        {
            this.state.Set(StaminaStatus.EXHAUSTED);
        }
        else if (this.GetCurrentPercentage() < this.tiredPercentage)
        {
            this.state.Set(StaminaStatus.TIRED);
        }
        else
        {
            this.state.Set(StaminaStatus.ENERGIZED);
        }

        this.current -= (2.5f * Time.deltaTime);
    }

    public enum StaminaStatus
    {
        ENERGIZED,
        TIRED,
        EXHAUSTED
    }
}
