using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwichChattingManager : MonoBehaviour
{
    [SerializeField]
    private TwichUserSO twichUserSO;
    private void Awake()
    {
        InitUser();
    }

    private void InitUser()
    {
        List<string> userNameRan = new List<string>(twichUserSO.userRandomName);
        List<string> userEngNameRan = new List<string>(twichUserSO.userRandomEngName);
        twichUserSO.users.RemoveRange(2, twichUserSO.users.Count - 2);
        for (int i = 0; i < twichUserSO.userRandomName.Count; i++)
        {
            string name = userNameRan[Random.Range(0, userNameRan.Count)];
            userNameRan.Remove(name);
            string engName = userEngNameRan[Random.Range(0, userEngNameRan.Count)];
            userEngNameRan.Remove(engName);
            UserBadgeType badgeType = (UserBadgeType)Random.Range(0, (int)UserBadgeType.MaxTemp);
            UserColor color = (UserColor)Random.Range(0, (int)UserColor.MaxTemp);
            User newUser = new User(name, engName, badgeType, color);
            twichUserSO.users.Add(newUser);
        }
    }
    public Color GetUserColor(UserColor curColor) => curColor switch
    {
        UserColor.Orange => new Color(218,28,79),
        UserColor.Red => new Color(231, 0, 0),
        UserColor.Apricot => new Color(222, 132, 84),
        UserColor.ReddishBrown => new Color(166, 72, 62),
        UserColor.Blue => new Color(37, 161, 202),
        UserColor.Pink => new Color(255, 85, 106),
        UserColor.skyblue => new Color(108, 193, 185),
        UserColor.Purple => new Color(127, 66, 166),
        UserColor.BlueGrey => new Color(73, 77, 109),
        _ => Color.white
    };
}
