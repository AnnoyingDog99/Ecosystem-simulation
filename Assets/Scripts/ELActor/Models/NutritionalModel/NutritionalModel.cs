using UnityEngine;
public class NutritionalModel : MonoBehaviour, INutritionalModel
{
    [SerializeField] private float foodPoints = 10;

    private void Start()
    {
    }

    public float GetEaten(float biteSize)
    {
        float points = Mathf.Min(biteSize, this.foodPoints);
        this.foodPoints -= points;
        return points;
    }

    public float GetMaxFoodPoints()
    {
        return this.foodPoints;
    }
    public float GetCurrentFoodPoints()
    {
        return this.foodPoints;
    }

    public void SetCurrentFoodPoints(float foodPoints)
    {
        this.foodPoints = foodPoints;
    }
}