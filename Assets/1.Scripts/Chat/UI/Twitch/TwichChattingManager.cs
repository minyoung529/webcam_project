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
        CreateUser();
    }

    private void CreateUser()
    {
        for (int i = 0; i < twichUserSO.users.Count; i++)
        {
            TwichChat item = PoolManager.Instance.GetPoolObject("TwichBubble").GetComponent<TwichChat>();
            item.User = twichUserSO.users[i];
            item.InitChat(twichUserSO.icons[(int)item.User.badge], twichUserSO.colors[(int)item.User.color]);
        }
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
}
