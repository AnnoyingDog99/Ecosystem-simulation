using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Updatable), true)]
public class UpdatableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Updatable updatable = target as Updatable;
        if (updatable.autoUpdate)
        {
            if (GUILayout.Button("Update"))
            {
                updatable.NotifyOfUpdatedValues();
            }
        }
    }
}