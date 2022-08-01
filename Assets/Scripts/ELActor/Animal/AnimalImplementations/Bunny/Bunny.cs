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

        this.GetAnimalHungerController().GetHungerTracker().GetState().Subscribe((status) =>
        {
            Debug.Log(status);
        });
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FirstUpdate()
    {
        base.FirstUpdate();
        if (this.GetLandAnimalMovementController().HasReachedDestination())
        {
            this.GetLandAnimalMovementController().Idle();
        }
        this.GetLandAnimalMovementController().RunTo(new Vector3(25f, 0.28f, 42.2f));
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
