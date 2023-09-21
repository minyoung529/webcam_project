using System;
using System.Collections.Generic;
using UnityEngine;
public enum DonationAmount
{
    Default,
    Small,
    Large,
}
[Serializable]
public struct DonationClip
{
    public DonationAmount amount;
    public List<AudioClip> clip;
}
[Serializable]
public struct ContentClip
{
    public string clipName;
    public string content;
    public AudioClip clip;
}
[Serializable]
[CreateAssetMenu(menuName ="Data/Twich/Donation Audio",fileName = "DonationAudioSO")]
public class DonationAudioSO : ScriptableObject
{
    [SerializeField]
    private List<DonationClip> donationClips;
    [SerializeField]
    private List<ContentClip> contentClips;

    public List<DonationClip> DonationClips { get { return donationClips; } }
    public List<ContentClip> ContentClips { get { return contentClips; } }

    public ContentClip FindContentClip(string clipName)
    {
        for(int i = 0; i < contentClips.Count; i++)
        {
            if(contentClips[i].clipName == clipName)
            {
                return contentClips[i];
            }
        }
        return contentClips[contentClips.Count-1];
    }
    public DonationClip FindDonationClip(DonationAmount clipType)
    {
        for (int i = 0; i < donationClips.Count; i++)
        {
            if (donationClips[i].amount == clipType)
            {
                return donationClips[i];
            }
        }
        return donationClips[donationClips.Count - 1];
    }
}
