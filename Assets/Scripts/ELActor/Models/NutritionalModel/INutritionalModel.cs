public interface INutritionalModel
{
    public float GetEaten(float biteSize);
    public float GetMaxFoodPoints();
    public float GetCurrentFoodPoints();
    public void SetCurrentFoodPoints(float foodPoints);
}