using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Sight : MonoBehaviour
{
    [SerializeField] protected float viewRadius;

    [Range(0, 360)]
    [SerializeField] protected float viewAngle;

    [SerializeField] protected float heightMultiplier = 1.36f;

    [SerializeField] protected List<LayerMask> targetMasks = new List<LayerMask>();
    [SerializeField] protected List<LayerMask> obstacleMasks = new List<LayerMask>();

    protected List<Transform> visibleTargets = new List<Transform>();
    protected List<Transform> visibleObstacles = new List<Transform>();

    public virtual float GetViewAngle()
    {
        return this.viewAngle;
    }

    public virtual float GetViewRadius()
    {
        return this.viewRadius;
    }

    public virtual float GetHeightMultiplier()
    {
        return this.heightMultiplier;
    }

    public virtual List<Transform> GetVisibleTargets()
    {
        return this.visibleTargets;
    }

    public virtual List<Transform> GetVisibleObstacles()
    {
        return this.visibleObstacles;
    }
}