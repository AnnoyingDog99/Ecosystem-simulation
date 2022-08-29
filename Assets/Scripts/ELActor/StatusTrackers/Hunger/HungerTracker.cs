using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerTracker : StatusTracker<HungerTracker.HungerStatus>
{
    [SerializeField] private uint hungryPercentage = 50;
    [SerializeField] private uint starvingPercentage = 10;
    [SerializeField] private float constantRate;

    private IAnimal animal;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        this.animal = GetComponentInParent<IAnimal>();
        this.status = new Observable<HungerStatus>(HungerStatus.SATISFIED);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.IsPaused()) return;

        this.current -= (this.constantRate * Time.deltaTime);

        if (this.GetCurrentPercentage() < this.starvingPercentage)
        {
            this.status.Set(HungerStatus.STARVING);
        }
        else if (this.GetCurrentPercentage() < this.hungryPercentage)
        {
            this.status.Set(HungerStatus.HUNGRY);
        }
        else
        {
            this.status.Set(HungerStatus.SATISFIED);
        }
    }

    public void AddAmount(float amount)
    {
        this.current = Mathf.Min(this.max, this.current + amount);
    }

    public enum HungerStatus
    {
        STARVING = 0,
        HUNGRY = 1,
        SATISFIED = 2
    }
}
