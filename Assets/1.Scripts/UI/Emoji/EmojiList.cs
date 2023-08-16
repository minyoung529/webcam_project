using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ǳ�� �ϳ��� ���� �̸�Ƽ�� ����Ʈ
/// </summary>
public class EmojiList : MonoBehaviour
{
    [SerializeField]
    private Emoji emoji;
    private Dictionary<ReactionType, Emoji> emojis = new();

    private Emoji InstantiateEmoji()
    {
        return new Emoji();
    }
    private void InitSprite(ReactionType type,int num,Sprite sprite = null)
    {
        if (sprite == null)
            emojis[type].SetEmoji(type, num);
        else
            emojis[type].SetEmoji(type, sprite, num);
    }
    public void AddEmoji(ReactionType type, Sprite sprite = null)
    {
        if (emojis.ContainsKey(type))
        {
            InitSprite(type, 1, sprite);
        }
        else
        {

        }
    }
    public void AddEmoji(ReactionType type, int num, Sprite sprite = null)
    {
        if (emojis.ContainsKey(type))
        {
           InitSprite(type,num, sprite);
        }
        else
        {

        }
    }
}
