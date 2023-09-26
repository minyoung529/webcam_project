using System.Collections;
using UnityEngine;

public class DonationManager : MonoSingleton<DonationManager>
{
    [SerializeField]
    private DonationAudioSO donationAudio;
    [SerializeField]
    private DonationPanel donationPanel;
    private AudioSource audioSource;

    private bool donationing = false;
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
        donationing = true;
        DonationClip donation = donationAudio.FindDonationClip(GetDonationAmount(cost));
        ContentClip content = donationAudio.FindContentClip(contentName);
        AudioClip donationClip = donation.clip[Random.Range(0, donation.clip.Count)];
        StartCoroutine(PlaySound(donationClip, content));
        StartCoroutine(donationPanel.Donation(user, cost.ToString(), content.content, donationClip.length));
    }
    
    public void OffDonation()
    {
        donationing = false;
        donationPanel.OffDonation();
        audioSource.Stop();
    }
}