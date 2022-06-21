using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BasicSight))]
public class BasicSightEditor : Editor
{
    void OnSceneGUI()
    {
        BasicSight basicSight = (BasicSight)target;
        Handles.color = Color.green;
        Handles.DrawWireArc(basicSight.transform.position, Vector3.up, Vector3.forward, 360, basicSight.GetViewRadius());

        Vector3 viewAngleA = basicSight.DirFromAngle(-basicSight.GetViewAngle() / 2, false);
        Vector3 viewAngleB = basicSight.DirFromAngle(basicSight.GetViewAngle() / 2, false);

        Handles.DrawLine(basicSight.transform.position, basicSight.transform.position + viewAngleA * basicSight.GetViewRadius());
        Handles.DrawLine(basicSight.transform.position, basicSight.transform.position + viewAngleB * basicSight.GetViewRadius());

        Handles.color = Color.red;

        foreach (Transform visibleTarget in basicSight.GetVisibleTargets())
        {
            Handles.DrawLine(basicSight.transform.position + Vector3.up * basicSight.GetHeightMultiplier(), visibleTarget.position + Vector3.up * basicSight.GetHeightMultiplier());
        }

        Handles.color = Color.blue;

        foreach (Transform visibleObstacle in basicSight.GetVisibleObstacles())
        {
            Handles.DrawLine(basicSight.transform.position + Vector3.up * basicSight.GetHeightMultiplier(), visibleObstacle.position + Vector3.up * basicSight.GetHeightMultiplier());
        }
    }
}
