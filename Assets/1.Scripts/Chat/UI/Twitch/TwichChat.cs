using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TwichChat : MonoBehaviour
{
    [SerializeField]
    private Image badgeImage;
    [SerializeField]
    private Image badgeBackgroundImage;
    [SerializeField]
    private TMP_Text userNameText;
    [SerializeField]
    private TMP_Text chattingText;
    private User user;

    public void InitChat(Sprite curBadgeImage, Color color)
    {
        if (curBadgeImage != null)
            badgeImage.sprite = curBadgeImage;
        else
            badgeImage.transform.parent.gameObject.SetActive(false);

        userNameText.text = $"{user.userName} {user.userEngName}";
        userNameText.color = color;

        if (user.badge == UserBadgeType.Manager)
            badgeBackgroundImage.color = new Color(14, 184, 100);
        if (user.badge == UserBadgeType.Bot)
            badgeBackgroundImage.color = Color.red;
    }
    public void ShowChat(string chat)
    {
        chattingText.text = chat;
    }
    public User User { get { return user; } set { user = value; } }
}
