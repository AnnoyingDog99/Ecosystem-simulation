using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimator : ELActorAnimator
{
    private int _isEatingHash;

    protected override void Start()
    {
        base.Start();
        this._isEatingHash = base.AddAnimation("isEating");
    }

    public bool GetIsEatingBool()
    {
        return base.GetBool(this._isEatingHash);
    }

    public void SetIsEatingBool(bool value)
    {
        base.SetBool(this._isEatingHash, value);
    }
}
