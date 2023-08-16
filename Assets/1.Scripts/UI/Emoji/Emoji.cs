using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// �̸��� ��ü
/// </summary>
public class Emoji : MonoBehaviour
{
    [SerializeField]
    private Image emojiIcon;
    [SerializeField]
    private TMP_Text emojiText;

    public ReactionType reactionType { get; private set; }

    //�Լ����� ���� �ö󰡴� �ִϸ��̼� �ٸ��� �߰��� ��
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

