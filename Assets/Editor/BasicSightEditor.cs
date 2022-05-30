using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (BasicSight))]
public class BasicSightEditor : Editor
{
    void OnSceneGUI()
    {
        BasicSight basicSight = (BasicSight)target;
        Handles.color = Color.green;
        Handles.DrawWireArc(basicSight.transform.position, Vector3.up, Vector3.forward, 360, basicSight.viewRadius);

        Vector3 viewAngleA = basicSight.DirFromAngle(-basicSight.viewAngle / 2, false);
        Vector3 viewAngleB = basicSight.DirFromAngle(basicSight.viewAngle / 2, false);

        Handles.DrawLine(basicSight.transform.position, basicSight.transform.position + viewAngleA * basicSight.viewRadius);
        Handles.DrawLine(basicSight.transform.position, basicSight.transform.position + viewAngleB * basicSight.viewRadius);

        Handles.color = Color.red;

        foreach (Transform visibleTarget in basicSight.GetVisibleTargets())
        {
            Handles.DrawLine(basicSight.transform.position + Vector3.up * basicSight.heightMultiplier, visibleTarget.position + Vector3.up * basicSight.heightMultiplier);
        }

        Handles.color = Color.blue;

        foreach (Transform visibleObstacle in basicSight.GetVisibleObstacles())
        {
            Handles.DrawLine(basicSight.transform.position + Vector3.up * basicSight.heightMultiplier, visibleObstacle.position + Vector3.up * basicSight.heightMultiplier);
        }
    }
}
