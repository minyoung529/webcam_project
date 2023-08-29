using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChangeableRect))]
public class ChangeableRectEditor : Editor
{
    private float num = 0;
    private ChangeableRect scroll;
    private void OnEnable()
    {
        scroll = target as ChangeableRect;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        num = EditorGUILayout.Slider(num, 0, 500);

        if (GUILayout.Button("늘이기", GUILayout.Width(80)))
        {
            scroll.ChangeRectLength(num);
            GUI.FocusControl(null);
        }
        if (GUILayout.Button("줄이기", GUILayout.Width(80)))
        {
            scroll.ChangeRectLength(-num);
            GUI.FocusControl(null);
        }
        EditorGUILayout.EndHorizontal();
    }
}