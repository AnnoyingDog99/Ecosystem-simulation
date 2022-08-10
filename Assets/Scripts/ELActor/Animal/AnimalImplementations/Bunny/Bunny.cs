using System.Collections.Generic;
using UnityEngine;

public class Bunny : Animal,
IHerbivoreLandAnimal, /* Required for the ForageOnLandNode */
IFertileLandAnimal /* Required for the MateWithPartnerOnLandNode */
{
    [SerializeField] private LandAnimalModel landAnimalModel;
    [SerializeField] private HerbivoreModel herbivoreModel;
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
        if (this.GetLandAnimalMovementController().HasReachedDestination())
        {
            this.GetLandAnimalMovementController().Idle();
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        this.GetLandAnimalMovementController().GetStaminaTracker().Pause();
    }

    protected override void FirstUpdate()
    {
        base.FirstUpdate();
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

    public List<string> GetPlantTags()
    {
        return this.herbivoreModel.GetPlantTags();
    }

    public BunnyAnimator GetBunnyAnimator()
    {
        return this.GetAnimalAnimator() as BunnyAnimator;
    }

    public BunnyMemory GetBunnyMemory()
    {
        return this.GetAnimalMemory() as BunnyMemory;
    }

    public IHerbivoreMemory GetHerbivoreMemory()
    {
        return this.GetBunnyMemory() as IHerbivoreMemory;
    }
}
