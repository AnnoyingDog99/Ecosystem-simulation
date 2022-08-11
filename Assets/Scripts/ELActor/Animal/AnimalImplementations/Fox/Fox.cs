using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Animal,
ICarnivoreLandAnimal, /* Required for the ChasePreyOnLandNode */
IFertileLandAnimal /* Required for the MateWithPartnerOnLandNode */
{
    [SerializeField] private LandAnimalModel landAnimalModel;
    [SerializeField] private CarnivoreModel carnivoreModel;

    private LandAnimalMovementController _landMovementController;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this._landMovementController = GetComponent<LandAnimalMovementController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void OnDeath()
    {
        base.OnDeath();
        this.GetLandAnimalMovementController().GetStaminaTracker().Pause();
    }

    public LandAnimalMovementController GetLandAnimalMovementController()
    {
        return this._landMovementController;
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

    public ICarnivoreMemory GetCarnivoreMemory()
    {
        return this.GetFoxMemory() as ICarnivoreMemory;
    }
}
