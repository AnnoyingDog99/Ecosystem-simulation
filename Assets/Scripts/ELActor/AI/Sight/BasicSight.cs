using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSight : Sight
{
    [SerializeField] private float reactionSpeed = 0.2f;

    // Used for detecting objects with a ray down (main purpose is detecting water)
    protected Vector3 angledDownRayDirection = new Vector3(10, -1, 10);

    protected override void Start()
    {
        base.Start();
        // Find anything in view with delay
        StartCoroutine(FindTargetsWithDelay(reactionSpeed));
        StartCoroutine(FindObstaclesWithDelay(reactionSpeed));
    }

    private void Update()
    {
        // Find anything straight ahead immediately
        this.AddTargetsAndObstaclesStraightAhead();
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            visibleTargets.Clear();
            AddTargetCentresInView();
        }
    }

    IEnumerator FindObstaclesWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            visibleObstacles.Clear();
            AddObstacleCentresInView();
        }
    }

    private void AddTargetCentresInView()
    {
        /** 
            This will only add targets if their center point is in view
        */
        List<Collider> targetsInViewRadius = new List<Collider>();
        foreach (LayerMask targetMask in targetMasks)
        {
            targetsInViewRadius.AddRange(Physics.OverlapSphere(transform.position, viewRadius, targetMask));
        }

        for (int i = 0; i < targetsInViewRadius.Count; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) > viewAngle / 2) continue;

            // Target is within view angle
            float distToTarget = Vector3.Distance(transform.position, target.position);

            if (target.gameObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                // Ignore self
                continue;
            }
            if (obstacleMasks.Count == 0)
            {
                // No obstacle masks so no obstacles inbetween actor and target (target is visible)
                visibleTargets.Add(target);
                continue;
            }
            for (int j = 0; j < obstacleMasks.Count; j++)
            {
                if (Physics.Raycast(transform.position + Vector3.up * this.GetHeightMultiplier(), dirToTarget, distToTarget, obstacleMasks[j]))
                {
                    // Obstacles inbetween actor and target (target is not visible)
                    continue;
                }
                visibleTargets.Add(target);
            }
        }
    }

    private void AddTargetsAndObstaclesStraightAhead()
    {
        /** 
            This will only add targets and obstacles that are straight ahead
        */
        RaycastHit hitStraightAhead = CastRay(transform.position, transform.forward, viewRadius);
        if (!RaycastHit.Equals(hitStraightAhead, default(RaycastHit)))
        {
            this.AddTargetsHit(hitStraightAhead);
            this.AddObstaclesHit(hitStraightAhead);
        }

        RaycastHit hitAtAnAngle = CastRay(transform.position, (new Vector3(transform.forward.x * angledDownRayDirection.x, angledDownRayDirection.y, transform.forward.z * angledDownRayDirection.z)), viewRadius);
        if (!RaycastHit.Equals(hitAtAnAngle, default(RaycastHit)))
        {
            this.AddTargetsHit(hitAtAnAngle);
            this.AddObstaclesHit(hitAtAnAngle);
        }
    }

    private void AddTargetsHit(RaycastHit hit)
    {
        // Check if hit object has a target layermask
        if (!targetMasks.Contains(LayerMask.GetMask(LayerMask.LayerToName(hit.transform.gameObject.layer)))) return;
        if (visibleTargets.Contains(((RaycastHit)hit).transform)) return;
        if (hit.transform.GetInstanceID() == gameObject.GetInstanceID()) return;
        visibleTargets.Add(((RaycastHit)hit).transform);
    }

    private void AddObstacleCentresInView()
    {
        List<Transform> possibleObstacles = new List<Transform>();

        foreach (LayerMask obstacleMask in obstacleMasks)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, viewRadius, obstacleMask);
            foreach (Collider collider in colliders) possibleObstacles.Add(collider.transform);
        }

        /** 
            This will only add obstacles if their center point is in view
        */
        for (int i = 0; i < possibleObstacles.Count; i++)
        {
            Transform obstacle = possibleObstacles[i].transform;
            Vector3 dirToObstacle = (obstacle.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToObstacle) > viewAngle / 2) continue;

            float distToObstacle = Vector3.Distance(transform.position, obstacle.position);

            RaycastHit hit = CastRay(transform.position, dirToObstacle, distToObstacle);
            if (RaycastHit.Equals(hit, default(RaycastHit))) return;
            if (hit.transform.tag == "Surface") return;
            visibleObstacles.Add(new ObstacleLocation(hit.transform, hit.point));
        }
    }

    private void AddObstaclesHit(RaycastHit hit)
    {
        /** 
            This will only add obstacles that are straight ahead
        */
        // Check if hit object has an obstacle layermask
        if (!obstacleMasks.Contains(LayerMask.GetMask(LayerMask.LayerToName(hit.transform.gameObject.layer)))) return;
        ObstacleLocation newObstacleLocation = new ObstacleLocation(hit.transform, hit.point);

        // Remove any existing transforms with older positions
        ObstacleLocation existingObstacleLocation = visibleObstacles.Find((visibleObstacle) => visibleObstacle.transform.GetInstanceID() == newObstacleLocation.transform.GetInstanceID());
        if (existingObstacleLocation != null)
        {
            visibleObstacles.Remove(existingObstacleLocation);
        }

        visibleObstacles.Add(newObstacleLocation);
    }

    private RaycastHit CastRay(Vector3 origin, Vector3 direction, float distance)
    {
        Ray ray = new Ray();
        ray.origin = origin;
        ray.direction = direction;
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, distance))
        {
            // Nothing was hit
            return default(RaycastHit);
        }
        return hit;
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
