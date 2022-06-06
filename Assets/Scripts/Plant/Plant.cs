using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : ELActor
{
    [SerializeField] Vector3 startScale = new Vector3(0, 0, 0);
    [SerializeField] Vector3 endScale = new Vector3(1, 1, 10);
    [SerializeField] int growTime = 10;

    private Vector3 growthStep;
    private int recoveryTime = 10;
    private float recoveryTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        base.SetScale(this.startScale);
        this.growthStep = (endScale - startScale) / growTime;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        recoveryTimer -= Time.deltaTime;
        if (HasRecovered() && !this.IsFullyGrown()) {
            base.SetScale(base.GetScale() + (this.growthStep * Time.deltaTime));
        }
    }

    public bool IsFullyGrown() {
        Vector3 diff = (endScale - base.GetScale());
        return diff.x <= 0 && diff.y <= 0 && diff.z <= 0;
    }

    private bool HasRecovered() {
        return this.recoveryTimer <= 0;
    }

    public virtual int Eat() {
        recoveryTimer = recoveryTime;
        // Return amount of "food points" or something like that
        return 1;
    }
}
