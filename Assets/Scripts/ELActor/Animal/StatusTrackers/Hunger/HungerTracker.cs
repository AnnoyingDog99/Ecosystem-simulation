using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerTracker : StatusTracker<HungerTracker.HungerStatus>
{
    [SerializeField] private uint hungryPercentage = 50;
    [SerializeField] private uint starvingPercentage = 10;

    private IAnimal animal;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        this.animal = GetComponentInParent<IAnimal>();
        this.state = new Observable<HungerStatus>(HungerStatus.SATISFIED);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (this.GetCurrentPercentage() < this.starvingPercentage)
        {
            this.state.Set(HungerStatus.STARVING);
        }
        else if (this.GetCurrentPercentage() < this.hungryPercentage)
        {
            this.state.Set(HungerStatus.HUNGRY);
        }
        else
        {
            this.state.Set(HungerStatus.SATISFIED);
        }
    }

    public void AddAmount(float amount)
    {
        this.current = Mathf.Min(this.max, this.current + amount);
    }

    public enum HungerStatus
    {
        SATISFIED,
        HUNGRY,
        STARVING
    }
}
