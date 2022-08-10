using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeTracker : StatusTracker<AgeTracker.AgeStatus>
{
    // 1f == 1 year
    // [SerializeField] private float ageRate = 0.000278f;
    [SerializeField] private float ageRate = 0.0167f;
    [SerializeField] private uint maturePercentage = 50;
    [SerializeField] private uint oldPercentage = 80;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        this.status = new Observable<AgeStatus>(AgeStatus.YOUNG);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.IsPaused()) return;

        if (this.GetCurrentPercentage() < this.maturePercentage)
        {
            this.status.Set(AgeStatus.YOUNG);
        }
        else if (this.GetCurrentPercentage() < this.oldPercentage)
        {
            this.status.Set(AgeStatus.MATURE);
        }
        else if (this.GetCurrentPercentage() < 100)
        {
            this.status.Set(AgeStatus.OLD);
        }
        else
        {
            this.status.Set(AgeStatus.MAX);
        }

        this.current += (this.ageRate * Time.deltaTime);
    }

    public float GetMaturePercentage()
    {
        return this.maturePercentage;
    }

    public float GetMatureAge()
    {
        return (this.GetMax() / 100) * this.GetMaturePercentage();
    }

    public uint GetCurrentComparedToMaturePercentage()
    {
        int percentage = Mathf.RoundToInt((100 / this.GetMatureAge()) * this.GetCurrent());
        if (percentage < 0) percentage = 0;
        return (uint)percentage;
    }

    public float GetOldPercentage()
    {
        return this.oldPercentage;
    }

    public float GetOldAge()
    {
        return (this.GetMax() / 100) * this.GetOldPercentage();
    }

    public uint GetCurrentComparedToOldPercentage()
    {
        int percentage = Mathf.RoundToInt((100 / this.GetOldAge()) * this.GetCurrent());
        if (percentage < 0) percentage = 0;
        return (uint)percentage;
    }

    public float GetAgeRate()
    {
        return this.ageRate;
    }

    public enum AgeStatus
    {
        YOUNG = 0,
        MATURE = 1,
        OLD = 2,
        MAX = 3
    }
}
