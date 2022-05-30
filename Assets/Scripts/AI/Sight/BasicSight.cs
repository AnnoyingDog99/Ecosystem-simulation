using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSight : MonoBehaviour
{
    [SerializeField] public float viewRadius;

    [Range(0, 360)]
    [SerializeField] public float viewAngle;

    [SerializeField] public float heightMultiplier = 1.36f;

    [SerializeField] private List<LayerMask> targetMasks;
    [SerializeField] private List<LayerMask> obstacleMasks;

    private List<Transform> visibleTargets = new List<Transform>();
    private List<Transform> possibleObstacles = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        possibleObstacles.Clear();

        List<Collider> targetsInViewRadius = new List<Collider>();
        foreach (LayerMask targetMask in targetMasks)
        {
            targetsInViewRadius.AddRange(Physics.OverlapSphere(transform.position, viewRadius, targetMask));
        }

        foreach (LayerMask obstacleMask in obstacleMasks)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, viewRadius, obstacleMask);
            foreach(Collider collider in colliders) possibleObstacles.Add(collider.transform);
        }

        for (int i = 0; i < targetsInViewRadius.Count; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) > viewAngle / 2) continue;

            // Target is within view angle
            float distToTarget = Vector3.Distance(transform.position, target.position);

            if (obstacleMasks.Count == 0)
            {
                // No obstacle masks so no obstacles inbetween actor and target (target is visible)
                visibleTargets.Add(target);
                continue;
            }
            for (int j = 0; j < obstacleMasks.Count; j++)
            {
                if (!Physics.Raycast(transform.position + Vector3.up * heightMultiplier, dirToTarget, distToTarget, obstacleMasks[j]))
                {
                    // No obstacles inbetween actor and target (target is visible)
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public List<Transform> GetVisibleTargets()
    {
        return visibleTargets;
    }

    public List<Transform> GetPossibleObstacles()
    {
        return possibleObstacles;
    }
}
