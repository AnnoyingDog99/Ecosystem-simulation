using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELActorAnimator : ELAnimator
{
    private int _isIdleHash, _isDeadHash;

    protected override void Start()
    {
        base.Start();
        this._isIdleHash = base.AddAnimation("isIdle");
        this._isDeadHash = base.AddAnimation("isDead");
    }

    public bool GetIsIdleBool()
    {
        return base.GetBool(this._isIdleHash);
    }

    public void SetIsIdleBool(bool value)
    {
        base.SetBool(this._isIdleHash, value);
    }

    public bool GetIsDeadBool()
    {
        return base.GetBool(this._isDeadHash);
    }

    public void SetIsDeadBool(bool value)
    {
        base.SetBool(this._isDeadHash, value);
    }
}
