using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerBar : MonoBehaviour
{
    Animal animal;
    [SerializeField] uint max;

    [Range(0, 100)]
    [SerializeField] uint efficiency;
    [SerializeField] float rate = 1f;
    [SerializeField] uint idlePenalty = 1;
    [SerializeField] uint walkingPenalty = 2;
    [SerializeField] uint runningPenalty = 3;
    float current;

    // Start is called before the first frame update
    void Start()
    {
        this.animal = GetComponentInParent<Animal>();
        this.current = this.max;
    }

    // Update is called once per frame
    void Update()
    {
        float penalties = 0f;
        float constantRate = Time.deltaTime * rate;
        if (animal.isIdle)
        {
            penalties += (constantRate * this.idlePenalty);
        }
        else if (animal.isWalking)
        {
            penalties += (constantRate * this.walkingPenalty);
        }
        else if (animal.isRunning)
        {
            penalties += (constantRate * this.runningPenalty);
        }
        penalties -= ((penalties / 100) * efficiency);
        this.current = (this.current - penalties) > 0 ? this.current - penalties : 0;
    }

    public uint GetHungerPercentage()
    {
        Debug.Log(this.current);
        int percentage = Mathf.RoundToInt((100 / max) * current);
        if (percentage < 0) percentage = 0;
        return (uint)percentage;
    }
}
