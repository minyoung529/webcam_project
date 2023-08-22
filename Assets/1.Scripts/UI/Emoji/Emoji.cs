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

    public bool isRemoved { get; private set; }
    public ReactionType reactionType { get; private set; }

    //�Լ����� ���� �ö󰡴� �ִϸ��̼� �ٸ��� �߰��� ��
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

