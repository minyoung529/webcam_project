using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class DonationManager : MonoSingleton<DonationManager>
{
    [SerializeField]
    private DonationAudioSO donationAudio;
    [SerializeField]
    private DonationPanel donationPanel;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private DonationAmount GetDonationAmount(int cost)
    {
        if (cost >= 50000)//5만원
            return DonationAmount.Large;
        else if (cost >= 10000)//1만원
            return DonationAmount.Default;
        else
            return DonationAmount.Small;
    }
    private IEnumerator PlaySound(AudioClip donationClip, ContentClip content)
    {
        audioSource.clip = null;
        audioSource.clip = donationClip;
        audioSource.Play();
        yield return new WaitForSeconds(donationClip.length+0.3f);
        audioSource.clip = null;
        audioSource.clip = content.clip;
        audioSource.Play();
    }

    /// <summary>
    /// example use
    /// Donation("소정", 5000000, "G_TEST");
    /// </summary>
    public void Donation(string user, int cost, string contentName)
    {
        DonationClip donation = donationAudio.FindDonationClip(GetDonationAmount(cost));
        ContentClip content = donationAudio.FindContentClip(contentName);
        AudioClip donationClip = donation.clip[Random.Range(0, donation.clip.Count)];
        StartCoroutine(PlaySound(donationClip, content));
        StartCoroutine(donationPanel.Donation(user, cost.ToString(), content.content, donationClip.length));
    }
}
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
        GUILayout.Label("User",GUILayout.Width(50));
        user = EditorGUILayout.TextArea(user);
        GUILayout.Label("Content", GUILayout.Width(50));
        contentName = EditorGUILayout.TextArea(contentName);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        cost = EditorGUILayout.IntSlider(cost, 1000, 100000,GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("도네하기", GUILayout.Width(80)) && user.Trim() != "" && contentName.Trim() != "")
        {
            donationManager.Donation(user, cost, contentName);
        }
        EditorGUILayout.EndHorizontal();
    }
}
