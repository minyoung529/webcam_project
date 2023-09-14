using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwichChat : MonoBehaviour
{
    [SerializeField]
    private Image badgeImage;
    [SerializeField]
    private Text userNameText;
    [SerializeField]
    private Text chattingText;
    private User user;

    public void InitChat(Sprite curBadgeImage, Color color)
    {
        if (curBadgeImage != null)
            badgeImage.sprite = curBadgeImage;
        else
            badgeImage.transform.parent.gameObject.SetActive(false);

        userNameText.text = $"{user.userName} {user.userEngName}";
        userNameText.color = color;
    }
    public void ShowChat(string chat)
    {
        chattingText.text = chat;
    }
    public User User { get { return user; } set { user = value; } }
}
