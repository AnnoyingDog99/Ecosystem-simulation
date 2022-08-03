using System.Collections.Generic;
using UnityEngine;

public class Bunny : Animal, IHerbivore, ILandAnimal
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
        this.GetLandAnimalMovementController().RunTo(new Vector3(25f, 0.28f, 42.2f));
        // this.GetActorScaleController().ScaleOverTime(new Vector3(0.05f, 0.05f, 0.05f), 2f);
        // this.GetActorHealthController().GetDamaged(10f);
        // this.GetActorHealthController().GetDamagedRepeatedly(1f, 1f, 4);
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
}
