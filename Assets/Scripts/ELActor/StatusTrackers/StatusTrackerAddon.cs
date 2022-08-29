using UnityEngine;
public abstract class StatusTrackerAddon : MonoBehaviour
{
    public virtual float CalculateCost() { return 0; }
}