using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeTracker : StatusTracker<AgeTracker.AgeStatus>
{
    [SerializeField] private float ageRate = 1f;
    [SerializeField] private uint adultPercentage = 50;
    [SerializeField] private uint elderPercentage = 80;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        this.state = new Observable<AgeStatus>(AgeStatus.CHILD);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.GetCurrentPercentage() < this.adultPercentage)
        {
            this.state.Set(AgeStatus.CHILD);
        }
        else if (this.GetCurrentPercentage() < this.elderPercentage)
        {
            this.state.Set(AgeStatus.ADULT);
        }
        else
        {
            this.state.Set(AgeStatus.ELDER);
        }

        this.current += (2.5f * Time.deltaTime);
    }

    public enum AgeStatus
    {
        CHILD,
        ADULT,
        ELDER
    }
}
