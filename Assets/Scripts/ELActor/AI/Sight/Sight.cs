using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Sight : MonoBehaviour
{
    [SerializeField] protected float viewRadius;

    [Range(0, 360)]
    [SerializeField] protected float viewAngle;

    [SerializeField] protected List<LayerMask> targetMasks = new List<LayerMask>();
    [SerializeField] protected List<LayerMask> obstacleMasks = new List<LayerMask>();

    protected List<Transform> visibleTargets = new List<Transform>();

    public class ObstacleLocation
    {
        public ObstacleLocation(Transform transform, Vector3 position)
        {
            this.transform = transform;
            this.position = position;
        }
        public Transform transform;
        public Vector3 position;
    };
    protected List<ObstacleLocation> visibleObstacles = new List<ObstacleLocation>();

    private float heightMultiplier = 1.36f;

    protected virtual void Start()
    {
        ELActor actor = GetComponent<ELActor>();
        if (actor == null)
        {
            Debug.LogWarning("Sight not bound to Actor");
            return;
        }
        this.heightMultiplier = actor.GetScale().y;
    }

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
        return this.visibleTargets.FindAll((target) => target != null);
    }

    public virtual List<ObstacleLocation> GetVisibleObstacles()
    {
        return this.visibleObstacles.FindAll((obstacle) => obstacle != null);
    }
}