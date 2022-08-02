using UnityEngine;
public class MatureAnimalGrowStrategy : IAnimalGrowStrategy
{
    public void execute(IAnimalAgeable animal)
    {
        // Grow to 100% of the potential scale
        Vector3 targetScale = animal.GetMaxScale();

        // Calculate how close the animal is to being old
        float oldProgressPercentage = animal.GetAnimalAgeController().GetAgeTracker().GetCurrentComparedToOldPercentage();

        // Set the initial scale
        Vector3 currentScale = animal.GetMinScale() + ((targetScale - animal.GetMinScale()) * (oldProgressPercentage / 100));
        animal.GetActorScaleController().SetScale(currentScale);

        // Calculate how long it should take for the animal to grow it's target scale
        float currentAge = animal.GetAnimalAgeController().GetAgeTracker().GetCurrent();
        float oldAge = animal.GetAnimalAgeController().GetAgeTracker().GetOldAge();

        // 1f == 1 year
        float ageRate = animal.GetAnimalAgeController().GetAgeTracker().GetAgeRate();

        float ageDiff = oldAge - currentAge;

        float seconds = ageDiff / ageRate;

        animal.GetActorScaleController().ScaleOverTime(targetScale, seconds);
    }
}