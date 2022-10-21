using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapManager))]
public class MapManagerBase : Editor
{
    private void OnEnable()
    {
        if (Application.isEditor)
        {
            (target as MapManager).CreateMap();
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if(EditorGUI.EndChangeCheck())
        {
            (target as MapManager).CreateMap();
        }
    }
}
