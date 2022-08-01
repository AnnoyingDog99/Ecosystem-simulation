using UnityEngine;
public class AnimalHungerModel : MonoBehaviour, IAnimalHungerModel
{
    [SerializeField] private float biteSize;

    public float GetBiteSize()
    {
        return this.biteSize;
    }
}