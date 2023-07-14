using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TestChatUIManager))]
public class ChatEditor : Editor
{
    TestChatUIManager chatManager;
    string text;
    private void OnEnable()
    {
        chatManager = target as TestChatUIManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        text = EditorGUILayout.TextArea(text);

        if (GUILayout.Button("������", GUILayout.Width(60)) && text.Trim() != "")
        {
            chatManager.TestChat(true, text, "��", null, ReactionType.None);
            text = "";
            GUI.FocusControl(null);
        }
        if (GUILayout.Button("�ޱ�", GUILayout.Width(60)) && text.Trim() != "")
        {
            chatManager.TestChat(false, text, "�̳�",null,ReactionType.None);
            text = "";
            GUI.FocusControl(null);
        }

        EditorGUILayout.EndHorizontal();

    }
}
