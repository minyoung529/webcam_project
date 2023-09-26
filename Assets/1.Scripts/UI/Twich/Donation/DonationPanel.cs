using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DonationPanel : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text userText;
    [SerializeField]
    private TMP_Text contentText;
    [SerializeField]
    private float talkWaitNum = 0.3f;
    private string user;
    private string cost;
    private string content;

    private void SetDonation(string user, string cost, string content)
    {
        this.user = user;
        this.cost = cost;
        this.content = content;

        userText.text = $"{user}<#FFFFFF>´ÔÀÌ</color> {cost}¿ø <#FFFFFF>ÈÄ¿ø!</color>";
        contentText.text = content;
    }
    public IEnumerator Donation(string user, string cost, string content,float alpaWaitSecond)
    {
        SetDonation(user, cost, content);
        image.DOFade(1, 0.2f);
        userText.DOFade(1, 0.2f);
        contentText.DOFade(1, 0.2f);
        yield return new WaitForSeconds(content.Length* talkWaitNum+alpaWaitSecond);
        userText.DOFade(0, 0.2f);
        contentText.DOFade(0, 0.2f);
        image.DOFade(0, 0.3f);
    }
    
    
    public void OffDonation()
    {
        image.DOFade(0, 0.05f);
        userText.DOFade(0, 0.05f);
        contentText.DOFade(0, 0.05f);
        userText.DOFade(0, 0.05f);
        contentText.DOFade(0, 0.05f);
        image.DOFade(0, 0.05f);
    }

}
