using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 말풍선 하나가 가진 이모티콘 리스트
/// </summary>
public class EmojiList : MonoBehaviour
{
    [SerializeField]
    private Emoji emoji;
    private Dictionary<ReactionType, Emoji> emojis = new();

    private Emoji InstantiateEmoji(ReactionType type, Sprite sprite = null)
    {
        Emoji curEmoji = Instantiate(emoji, gameObject.transform).GetComponent<Emoji>();
        curEmoji.AddEmoji(type, sprite, 1);
        return curEmoji;
    }
    private IEnumerator AddSomeEmoji(int num, ReactionType type, Sprite sprite)
    {
        emojis.Add(type, InstantiateEmoji(type, sprite));
        for (int i = 1; i < num; i++)
        {
            InitSprite(type, 1, sprite);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }
    }
    private void InitSprite(ReactionType type, int num, Sprite sprite = null)
    {
        if (sprite == null)
            emojis[type].AddEmoji(type, num);
        else
            emojis[type].AddEmoji(type, sprite, num);
    }
    public void AddEmoji(ReactionType type, Sprite sprite = null)
    {
        if (emojis.ContainsKey(type))
        {
            InitSprite(type, 1, sprite);
        }
        else
        {
            emojis.Add(type, InstantiateEmoji(type, sprite));

        }
    }
    public void AddEmoji(ReactionType type, int num, Sprite sprite = null)
    {
        if (emojis.ContainsKey(type))
        {
            InitSprite(type, num, sprite);
        }
        else
        {
            StartCoroutine(AddSomeEmoji(num, type, sprite));
        }
    }
    public void RemoveEmoji(ReactionType type)
    {
        if (emojis.ContainsKey(type))
        {
            emojis[type].RemoveEmoji();
        }
        bool result = emojis.All((x) =>
        {
            if (x.Value.isRemoved == false)
            {
                return true;
            }
            return false;
        });
        if (result || emojis.Count <= 0)//다 꺼져있거나 아무것도 들어있지 않는다면
        {
            gameObject.SetActive(false);
        }
    }
}
