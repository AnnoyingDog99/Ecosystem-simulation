using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Animal animal;
    [SerializeField] uint max = 10;
    [SerializeField] float regenRate = 1f;
    [SerializeField] float regenDelay = 5f;

    [SerializeField] float current;
    float regenDelayTimer;

    // Start is called before the first frame update
    void Start()
    {
        this.animal = GetComponentInParent<Animal>();
        this.current = this.max;
        this.regenDelayTimer = regenDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (animal.isDead) return;
        if (this.current >= this.max)
        {
            // Health is at max, no need to regenerate
            this.current = this.max;
            this.regenDelayTimer = this.regenDelay;
            return;
        }
        if (this.animal.GetHungerBar().IsStarving())
        {
            // Can't regen while starving
            this.regenDelayTimer = this.regenDelay;
            return;
        }
        this.regenDelayTimer -= Time.deltaTime;
        if (this.regenDelayTimer < 0)
        {
            // Regenerate health
            this.current += (this.regenRate * Time.deltaTime);
        }
    }

    public void AddHealthPoints(float healthPoints)
    {
        this.current += healthPoints;
        if (this.current > this.max) this.current = this.max;
    }

    public void RemoveHealthPoints(float damagePoints)
    {
        this.regenDelayTimer = this.regenDelay;
        this.current -= damagePoints;
        if (this.current < 0) this.current = 0;
    }

    public uint GetHealthPercentage()
    {
        int percentage = Mathf.RoundToInt((100 / this.max) * this.current);
        if (percentage < 0) percentage = 0;
        return (uint)percentage;
    }

    public bool IsRegenerating()
    {
        return this.regenDelayTimer < this.regenDelay;
    }
}
