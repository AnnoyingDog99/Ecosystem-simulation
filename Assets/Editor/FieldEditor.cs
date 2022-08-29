using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Field), true)]
public class FieldEditor : Editor
{
    void OnSceneGUI()
    {
        Field field = (Field)target;
        Handles.color = Color.green;

        Vector3 position = new Vector3(field.GetPosition().x, field.GetPosition().y + (field.GetReferencePlant().GetScale().y / 2), field.GetPosition().z);
        Vector3 scale = new Vector3(field.GetSize().x, field.GetReferencePlant().GetScale().y, field.GetSize().y);
        Handles.DrawWireCube(position, scale);
    }
}
