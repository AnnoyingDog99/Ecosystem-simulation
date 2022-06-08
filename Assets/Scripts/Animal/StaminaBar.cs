using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    Animal animal;
    [SerializeField] uint max = 100;
    [SerializeField] float recoveryRate = 1;
    [SerializeField] float recoveryDelay = 5;
    [SerializeField] float walkingCost = 1;
    [SerializeField] float runningCost = 3;
    [SerializeField] public uint minimumRunPercentage { get; protected set; } = 25;
    [SerializeField] public uint minimumWalkPercentage { get; protected set; } = 5;
    float current;
    float recoveryDelayTimer;

    // Start is called before the first frame update
    void Start()
    {
        this.animal = GetComponentInParent<Animal>();
        this.current = this.max;
        this.recoveryDelayTimer = recoveryDelay;
    }

    // Update is called once per frame
    void Update()
    {
        float cost = animal.isWalking ? walkingCost : animal.isRunning ? runningCost : 0;
        if (cost == 0)
        {
            recoveryDelayTimer -= Time.deltaTime;
            if (this.recoveryDelayTimer <= 0)
            {
                // Recharge Stamina
                this.current += (recoveryRate * Time.deltaTime);
            }
        }
        else
        {
            recoveryDelayTimer = recoveryDelay;
            // Discharge Stamina
            this.current -= (cost * Time.deltaTime);
        }
    }

    public uint GetStaminaPercentage()
    {
        int percentage = Mathf.RoundToInt((100 / max) * current);
        if (percentage < 0) percentage = 0;
        return (uint)percentage;
    }
}
