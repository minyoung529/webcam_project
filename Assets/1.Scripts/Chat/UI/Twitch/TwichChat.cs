using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class TwichChat : MonoBehaviour
{
    [SerializeField]
    private Image badgeImage;
    [SerializeField]
    private Image badgeBackgroundImage;
    [SerializeField]
    private TMP_Text userNameText;

    private string content;
    private User user;

    public void InitChat(Sprite curBadgeImage, Color color)
    {
        if (curBadgeImage != null)
            badgeImage.sprite = curBadgeImage;
        else
            badgeImage.transform.parent.gameObject.SetActive(false);

        userNameText.text = $"{user.userName} ({user.userEngName}):";
        userNameText.color = color;
        if (user.badge == UserBadgeType.Manager)
            badgeBackgroundImage.color = Color.clear;
            //badgeBackgroundImage.color = new Color(14, 184, 100);
        else if (user.badge == UserBadgeType.Bot)
            badgeBackgroundImage.color = Color.red;
        else if(user.badge != UserBadgeType.None)
            badgeBackgroundImage.color = Color.clear;
    }
    public void ShowChat(string chat)
    {
        userNameText.text = $"{user.userName} ({user.userEngName}): <color=black>{chat}</color>";
        content = chat;
    }
    public string GetChat()
    {
        Assert.IsNotNull(content, "채팅 텍스트가 존재하지 않습니다.");
        return content;
    }
    public User User { get { return user; } set { user = value; } }
}
