using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// 이모지 객체
/// </summary>
public class Emoji : MonoBehaviour
{
    [SerializeField]
    private Image emojiIcon;
    [SerializeField]
    private TMP_Text emojiText;

    public bool isRemoved { get; private set; }
    public ReactionType reactionType { get; private set; }

    //함수마다 숫자 올라가는 애니메이션 다르게 추가할 것
    public void AddEmoji(ReactionType type, Sprite emojiSprite, int emojiNum)
    {
        if(isRemoved)
        {
            gameObject.SetActive(true);
            isRemoved = false;
        }
        reactionType = type;
        emojiText.text = (int.Parse(emojiText.text) + emojiNum).ToString();
        emojiIcon.sprite = emojiSprite;
    }
    public void AddEmoji(ReactionType type, int emojiNum)
    {
        if (isRemoved)
        {
            gameObject.SetActive(true);
            isRemoved = false;
        }
        reactionType = type;
        emojiText.text = (int.Parse(emojiText.text) + emojiNum).ToString();
    }
    public void SetEmoji(ReactionType type, int emojiNum)
    {
        if (isRemoved)
        {
            gameObject.SetActive(true);
            isRemoved = false;
        }
        reactionType = type;
        emojiText.text = (emojiNum).ToString();
    }
    public void RemoveEmoji()
    {
        emojiText.text = "0";
        gameObject.SetActive(false);
        isRemoved = true;
    }
}

