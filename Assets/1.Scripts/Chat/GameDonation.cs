using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDonation : MonoBehaviour
{

    private void Start()
    {
        EventManager.StartListening(EventName.OnMiniGameStart, StartDonation);
        EventManager.StartListening(EventName.OnMiniGameOver, StopDonation);
    }

    private void StartDonation()
    {
        DonationManager.Instance.OffDonation();
        int randomUser = Random.Range(0, 2);

        if (randomUser == 0) DonationManager.Instance.Donation("저초딩아니에요", 2000, "G_HI");
        else DonationManager.Instance.Donation("찬구쨩", 4000, "C_HI");

        StartCoroutine(AttackDonation());
    }

    private IEnumerator AttackDonation()
    {
        while(true)
        {
            float randomTime = Random.Range(10f, 50f);
            yield return new WaitForSeconds(randomTime);
            TriggerAttackDonation();
        }
    }

    private void TriggerAttackDonation()
    {
        DonationManager.Instance.OffDonation();

        int cost = Random.Range(1000, 50000);
        int attackNumber = Random.Range(0, 4);
        int randomUser = Random.Range(0, 2);
        string content = "";
        string user = "";

        if(randomUser == 0)
        {
            user = "찬구쨩";
            content = "C_ATTACK" + attackNumber.ToString();
        }
        else
        {
            user = "저초딩아님";
            content = "G_ATTACK" + attackNumber.ToString();
        }

        DonationManager.Instance.Donation(user, cost, content);
    }

    private void StopDonation()
    {
        DonationManager.Instance.OffDonation();
        StopCoroutine(AttackDonation());

        int randomUser = Random.Range(0, 2);

        if (randomUser == 0) DonationManager.Instance.Donation("저초딩아니에요", 2000, "G_BYE");
        else DonationManager.Instance.Donation("찬구쨩", 4000, "C_BYE");

    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventName.OnMiniGameStart, StartDonation);
        EventManager.StopListening(EventName.OnMiniGameOver, StopDonation);
    }
}
