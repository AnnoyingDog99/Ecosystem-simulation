using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal, ICarnivore, ILandAnimal
{
    [SerializeField] private LandAnimalMovementController landMovementController;
    [SerializeField] private LandAnimalModel landAnimalModel;
    [SerializeField] private CarnivoreModel carnivoreModel;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public LandAnimalMovementController GetLandAnimalMovementController()
    {
        return this.landMovementController;
    }

    public LandAnimalAnimator GetLandAnimalAnimator()
    {
        return this.landAnimalModel.GetLandAnimalAnimator();
    }
    public float GetWalkSpeed()
    {
        return this.landAnimalModel.GetWalkSpeed();
    }

    public float GetRunSpeed()
    {
        return this.landAnimalModel.GetRunSpeed();
    }

    public List<string> GetPreyTags()
    {
        return this.carnivoreModel.GetPreyTags();
    }

    public FoxAnimator GetFoxAnimator()
    {
        return this.GetAnimalAnimator() as FoxAnimator;
    }

    public FoxMemory GetFoxMemory()
    {
        return this.GetAnimalMemory() as FoxMemory;
    }
}
