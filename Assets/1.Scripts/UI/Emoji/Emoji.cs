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

    public ReactionType reactionType { get; private set; }

    //함수마다 숫자 올라가는 애니메이션 다르게 추가할 것
    public void SetEmoji(ReactionType type, Sprite emojiSprite, int emojiNum)
    {
        reactionType = type;
        emojiText.text = (int.Parse(emojiText.text) + emojiNum).ToString();
        emojiIcon.sprite = emojiSprite;
    }
    public void SetEmoji(ReactionType type, int emojiNum)
    {
        reactionType = type;
        emojiText.text = (int.Parse(emojiText.text) + emojiNum).ToString();
    }
}

