using UnityEngine;

public class LandAnimalModel : MonoBehaviour, ILandAnimalModel
{
    [SerializeField] private LandAnimalAnimator landAnimalAnimator;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private void Start()
    {
    }

    public LandAnimalAnimator GetLandAnimalAnimator()
    {
        return this.landAnimalAnimator;
    }

    public float GetWalkSpeed()
    {
        return this.walkSpeed;
    }

    public float GetRunSpeed()
    {
        return this.runSpeed;
    }

}