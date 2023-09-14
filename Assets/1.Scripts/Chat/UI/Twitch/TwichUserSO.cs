using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UserBadgeType
{
    Manager = -2,
    Bot = -1,
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
    public List<Color> colors = new();
    public List<User> users = new List<User>();
    public List<string> userRandomName = new List<string>();
    public List<string> userRandomEngName = new List<string>();
}
