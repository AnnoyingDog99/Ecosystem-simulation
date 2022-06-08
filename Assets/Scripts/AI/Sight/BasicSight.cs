using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSight : Sight
{
    [SerializeField] private float reactionSpeed = 0.2f;

    private void Start()
    {
        // Find anything in view with delay
        StartCoroutine(FindTargetsWithDelay(reactionSpeed));
        StartCoroutine(FindObstaclesWithDelay(reactionSpeed));
    }

    private void Update()
    {
        // Find anything straight ahead immediately
        AddTargetsStraightAhead();
        AddObstaclesStraightAhead();
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

            if (target.gameObject?.GetComponent<Animal>()?.GetID() == gameObject?.GetComponentInParent<Animal>()?.GetID())
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
                if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, dirToTarget, distToTarget, obstacleMasks[j]))
                {
                    // Obstacles inbetween actor and target (target is not visible)
                    continue;
                }
                visibleTargets.Add(target);
            }
        }
    }

    private void AddTargetsStraightAhead()
    {
        /** 
            This will only add targets that are straight ahead
        */
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = transform.forward;
        RaycastHit hit = CastRay(transform.position, transform.forward, viewRadius);
        if (RaycastHit.Equals(hit, default(RaycastHit))) return;
        // Check if hit object has an obstacle layermask
        if (!targetMasks.Contains(LayerMask.GetMask(LayerMask.LayerToName(hit.transform.gameObject.layer)))) return;
        // It's not an obstacle if the actor is above it
        if (((RaycastHit)hit).transform.position.y < transform.position.y) return;
        if (visibleTargets.Contains(((RaycastHit)hit).transform)) return;
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
            visibleObstacles.Add(hit.transform);
        }
    }

    private void AddObstaclesStraightAhead()
    {
        /** 
            This will only add obstacles that are straight ahead
        */
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = transform.forward;
        RaycastHit hit = CastRay(transform.position, transform.forward, viewRadius);
        if (RaycastHit.Equals(hit, default(RaycastHit))) return;
        // Check if hit object has an obstacle layermask
        if (!obstacleMasks.Contains(LayerMask.GetMask(LayerMask.LayerToName(hit.transform.gameObject.layer)))) return;
        // It's not an obstacle if the actor is above it
        if (((RaycastHit)hit).transform.position.y < transform.position.y) return;
        if (visibleObstacles.Contains(((RaycastHit)hit).transform)) return;
        visibleObstacles.Add(((RaycastHit)hit).transform);
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
        // It's not an obstacle if the actor is above it
        if (hit.transform.position.y < transform.position.y) return default(RaycastHit);
        if (visibleObstacles.Contains(hit.transform)) return default(RaycastHit);
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
