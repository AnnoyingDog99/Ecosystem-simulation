using UnityEngine;
public class NutritionalModel : MonoBehaviour, INutritionalModel
{
    [SerializeField] private float maxFoodPoints;
    private float _currentFoodPoints;

    public float GetEaten(float biteSize)
    {
        float points = Mathf.Min(this._currentFoodPoints - biteSize, this._currentFoodPoints);
        this._currentFoodPoints -= points;
        return points;
    }

    public float GetMaxFoodPoints()
    {
        return this.maxFoodPoints;
    }
    public float GetCurrentFoodPoints()
    {
        return this._currentFoodPoints;
    }

    public void SetCurrentFoodPoints(float foodPoints)
    {
        this._currentFoodPoints = foodPoints;
    }
}