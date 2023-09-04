using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DonationPanel : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text userText;
    [SerializeField]
    private TMP_Text contentText;

    private string user;
    private string cost;
    private string content;

    public void SetDonation(string user, string cost, string content)
    {
        this.user = user;
        this.cost = cost;
        this.content = content;

        userText.text = $"{user}<#FFFFFF>����</color> {cost}�� <#FFFFFF>�Ŀ�!</color>";
        contentText.text = content;
    }
}
