using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TestChatManager))]
public class ChatEditor : Editor
{
    TestChatManager chatManager;
    string text;
    private void OnEnable()
    {
        chatManager = target as TestChatManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        text = EditorGUILayout.TextArea(text);

        if (GUILayout.Button("보내기", GUILayout.Width(60)) && text.Trim() != "")
        {
            chatManager.TestChat(true, text, "나", EmojiType.None);
            text = "";
            GUI.FocusControl(null);
        }
        if (GUILayout.Button("받기", GUILayout.Width(60)) && text.Trim() != "")
        {
            chatManager.TestChat(false, text, "미녕",EmojiType.None);
            text = "";
            GUI.FocusControl(null);
        }

        EditorGUILayout.EndHorizontal();

    }
}
