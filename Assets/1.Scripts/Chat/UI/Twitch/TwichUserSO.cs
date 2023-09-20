using System;
using System.Collections.Generic;
using UnityEngine;

public enum UserBadgeType
{
    Manager,
    Bot,
    None,
    Subscriber1,
    Subscriber2,
    Subscriber3,
    MaxTemp
}
public enum UserColor
{
    Orange,
    Red,
    Apricot,
    ReddishBrown,
    Blue,
    Pink,
    skyblue,
    Purple,
    BlueGrey,
    MaxTemp
}

[Serializable]
public struct User
{
    public string userName;
    public string userEngName;
    public UserBadgeType badge;
    public UserColor color;

    public User(string Name, string engName, UserBadgeType badgeType, UserColor col)
    {
        userName = Name;
        userEngName = engName;
        badge = badgeType;
        color = col;
    }
}
[CreateAssetMenu(fileName = "TwichUserSO", menuName = "Data/Twich/UserSO")]
public class TwichUserSO : ScriptableObject
{
    public int priviewChatCount = 5;
    public List<Color> colors = new();
    public List<Sprite> icons = new();
    public List<User> users = new List<User>();
    public List<string> userRandomName = new List<string>();
    public List<string> userRandomEngName = new List<string>();
    public List<string> randomChat = new List<string>();

    public List<string> privewChat = new List<string>();
    public string GetRandom()
    {
        List<string> ranChat = new List<string>(randomChat);
        if (privewChat != null)
        {
            for(int i = 0; i<privewChat.Count;i++)
            {
                ranChat.Remove(privewChat[i]);
            }
        }

        string ranPrivewChat = ranChat[UnityEngine.Random.Range(0, ranChat.Count)];
        return ranPrivewChat;
    }

    public void AddPriviewChat(string priviewChat)
    {
        privewChat.Add(priviewChat);
        if(privewChat.Count>priviewChatCount)
        {
            privewChat.RemoveAt(0);
        }
    }
}
