using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DonationManager))]
public class DonationEditor : Editor
{
    private DonationManager donationManager;
    private string user;
    private int cost;
    private string contentName;
    private void OnEnable()
    {
        donationManager = target as DonationManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("User", GUILayout.Width(50));
        user = EditorGUILayout.TextArea(user);
        GUILayout.Label("Content", GUILayout.Width(50));
        contentName = EditorGUILayout.TextArea(contentName);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        cost = EditorGUILayout.IntSlider(cost, 1000, 100000, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("도네하기", GUILayout.Width(80)) && user.Trim() != "" && contentName.Trim() != "")
        {
            donationManager.Donation(user, cost, contentName);
        }
        EditorGUILayout.EndHorizontal();
    }
}
