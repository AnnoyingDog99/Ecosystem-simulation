using UnityEngine;
using UnityEngine.AI;

public abstract class LandAnimalMovementState
{
    protected LandAnimalMovementContext context;
    protected ILandAnimal animal;

    public LandAnimalMovementState(ILandAnimal animal)
    {
        this.animal = animal;
        this.context = new LandAnimalMovementContext();
    }
}