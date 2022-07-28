using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeBar : MonoBehaviour
{
    Animal animal;
    [SerializeField] uint babyAge = 0;
    [SerializeField] uint childAge = 10;
    [SerializeField] uint adultAge = 20;
    [SerializeField] uint max = 50;

    [SerializeField] float current;

    // Start is called before the first frame update
    void Start()
    {
        this.animal = GetComponentInParent<Animal>();
        this.current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.animal.isDead) return;
        if (this.current > this.max)
        {
            this.current = this.max;
        }
    }

    public uint GetAgePercentage(uint age)
    {
        int percentage = Mathf.RoundToInt((100 / age) * this.current);
        if (percentage < 0) percentage = 0;
        if (percentage > 100) percentage = 100;
        return (uint)percentage;
    }

    public void AddAge(uint age = 1)
    {
        this.current += age;
    }

    public void SetAge(uint age)
    {
        this.current = age;
    }

    public float GetCurrent()
    {
        return this.current;
    }

    public uint GetBabyAge()
    {
        return this.babyAge;
    }

    public uint GetChildAge()
    {
        return this.childAge;
    }

    public uint GetAdultAge()
    {
        return this.adultAge;
    }

    public uint GetMaxAge()
    {
        return this.max;
    }
}
