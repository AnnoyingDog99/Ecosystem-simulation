using UnityEngine;

public class YoungAnimalGrowStrategy : IAnimalGrowStrategy
{
    public void execute(IAnimalAgeable animal)
    {
        // Grow to 75% of the potential scale (between min and max)
        Vector3 targetScale = animal.GetMinScale() + ((animal.GetMaxScale() - animal.GetMinScale()) * 0.75f);

        // Calculate how close the animal is to being mature
        float matureProgressPercentage = animal.GetAnimalAgeController().GetAgeTracker().GetCurrentComparedToMaturePercentage();

        // Set the initial scale
        Vector3 currentScale = animal.GetMinScale() + ((targetScale - animal.GetMinScale()) * (matureProgressPercentage / 100));
        animal.GetActorScaleController().SetScale(currentScale);

        // Calculate how long it should take for the animal to grow it's target scale
        float currentAge = animal.GetAnimalAgeController().GetAgeTracker().GetCurrent();
        float matureAge = animal.GetAnimalAgeController().GetAgeTracker().GetMatureAge();

        // 1f == 1 year
        float ageRate = animal.GetAnimalAgeController().GetAgeTracker().GetAgeRate();

        float ageDiff = matureAge - currentAge;

        float seconds = ageDiff / ageRate;

        animal.GetActorScaleController().ScaleOverTime(targetScale, seconds);
    }
}