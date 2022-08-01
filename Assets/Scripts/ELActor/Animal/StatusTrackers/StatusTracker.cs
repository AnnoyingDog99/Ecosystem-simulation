using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusTracker<T> : MonoBehaviour
{
    [SerializeField] protected float max = 100;
    [SerializeField] protected float current = 100;
    [SerializeField] protected List<StatusTrackerAddon> addons = new List<StatusTrackerAddon>();
    protected Observable<T> state;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Update()
    {
        foreach (StatusTrackerAddon addon in this.addons)
        {
            this.current -= addon.CalculateCost();
        }
        this.current = Mathf.Clamp(this.current, 0, this.max);
    }

    public float GetCurrent()
    {
        return this.current;
    }

    public float GetMax()
    {
        return this.max;
    }

    public uint GetCurrentPercentage()
    {
        int percentage = Mathf.RoundToInt((100 / this.GetMax()) * this.GetCurrent());
        if (percentage < 0) percentage = 0;
        return (uint)percentage;
    }

    public Observable<T> GetState()
    {
        return this.state;
    }
}
