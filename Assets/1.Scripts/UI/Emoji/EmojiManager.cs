using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EmojiManager : MonoBehaviour
{
    private Dictionary<string, Sprite> emojiSprite = new();
    private List<string> list = new List<string>();


    private void Start()
    {
        SetSpriteName();
    }
    private void SetSpriteName()
    {
        for (int i = 1; i < (int)ReactionType.AI; i++)
        {
            list.Add(Enum.GetName(typeof(ReactionType), i));
        }
        SetSprite(list);
    }
    private void SetSprite(List<string> nameList)
    {
        for (int i = 0; i < nameList.Count; i++)
        {
            int a = i;
            Addressables.LoadAssetAsync<Sprite>(nameList[a].Trim()).Completed +=
                (AsyncOperationHandle<Sprite> sprite) =>
                {
                    emojiSprite.Add(nameList[a], sprite.Result);
                };
        }
    }

    public Sprite GetEmojiSprite(string name)
    {
        return emojiSprite[name];
    }
    public void AddEmoji(EmojiList emojiList, ReactionType type)
    {

    }
    public void AddEmoji(EmojiList emojiList, ReactionType type, int num)
    {

    }
}
