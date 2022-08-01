using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandAnimalAnimator : AnimalAnimator
{
    private int _isWalkingHash, _isRunningHash;

    protected override void Start()
    {
        base.Start();
        this._isWalkingHash = base.AddAnimation("isWalking");
        this._isRunningHash = base.AddAnimation("isRunning");
    }

    public bool GetIsWalkingBool()
    {
        return base.GetBool(this._isWalkingHash);
    }

    public void SetIsWalkingBool(bool value)
    {
        base.SetBool(this._isWalkingHash, value);
    }

    public bool GetIsRunningBool()
    {
        return base.GetBool(this._isRunningHash);
    }

    public void SetIsRunningBool(bool value)
    {
        base.SetBool(this._isRunningHash, value);
    }
}
