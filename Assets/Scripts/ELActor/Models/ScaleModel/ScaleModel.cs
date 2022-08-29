using UnityEngine;

public class ScaleModel : MonoBehaviour, IScaleModel
{
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;

    public Vector3 GetMinScale() 
    {
        return this.minScale;
    }

    public Vector3 GetMaxScale() 
    {
        return this.maxScale;
    }
}