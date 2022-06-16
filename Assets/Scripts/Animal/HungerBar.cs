using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerBar : MonoBehaviour
{
    Animal animal;
    [SerializeField] uint max = 100;

    [Range(0, 100)]
    [SerializeField] uint efficiency;
    [SerializeField] float rate = 1f;
    [SerializeField] float idlePenalty = 0.5f;
    [SerializeField] float walkingPenalty = 1f;
    [SerializeField] float runningPenalty = 1.5f;
    [SerializeField] float regeneratingPenalty = 1f;
    [SerializeField] protected uint hungryPercentage = 50;
    [SerializeField] protected uint starvingPercentage = 10;

    [SerializeField] float current;

    // Start is called before the first frame update
    void Start()
    {
        this.animal = GetComponentInParent<Animal>();
        this.current = this.max;
    }

    // Update is called once per frame
    void Update()
    {
        if (animal.isDead) return;
        float penalties = 0f;
        float constantRate = Time.deltaTime * rate;
        if (this.animal.isWalking)
        {
            penalties += (constantRate * this.walkingPenalty);
        }
        else if (this.animal.isRunning)
        {
            penalties += (constantRate * this.runningPenalty);
        }
        else if (this.animal.GetHealthBar().IsRegenerating())
        {
            penalties += (constantRate * this.regeneratingPenalty);
        }
        else
        {
            penalties += (constantRate * this.idlePenalty);
        }
        penalties -= ((penalties / 100) * efficiency);
        this.current = (this.current - penalties) > 0 ? this.current - penalties : 0;
    }

    public void AddFoodPoints(float foodPoints)
    {
        this.current += foodPoints;
        if (this.current > this.max) this.current = this.max;
    }

    public void RemoveFoodPoints(float foodPoints)
    {
        this.current -= foodPoints;
        if (this.current < 0) this.current = 0;
    }

    public uint GetHungerPercentage()
    {
        int percentage = Mathf.RoundToInt((100 / max) * current);
        if (percentage < 0) percentage = 0;
        return (uint)percentage;
    }

    public bool IsHungry()
    {
        return this.GetHungerPercentage() <= this.hungryPercentage;
    }

    public bool IsStarving()
    {
        return this.GetHungerPercentage() <= this.starvingPercentage;
    }
}
