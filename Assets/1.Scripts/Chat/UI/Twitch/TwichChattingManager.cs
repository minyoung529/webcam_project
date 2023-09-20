using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TwichChattingManager : MonoBehaviour
{
    [SerializeField]
    private TwichUserSO twichUserSO;
    [SerializeField]
    [Range(0.3f, 1f)]
    private float chattingDelay;
    [SerializeField]
    private Transform content;
    [SerializeField]
    private Scrollbar scrollbar;
    private List<TwichChat> recentChat = new();
    private bool chattingStop = false;
    private void Start()
    {
        InitUser();
    }
    private IEnumerator CreateChat()
    {
        while (true)
        {
            if (chattingStop)
            {
                yield return new WaitUntil(() => chattingStop = false);
            }
            CreateUser();
            yield return new WaitForSeconds(Random.Range(chattingDelay - 0.3f, chattingDelay + 0.3f));
        }
    }
    public void CreateUser(string text = "")
    {
        TwichChat item = PoolManager.Instance.GetPoolObject("TwichBubble").GetComponent<TwichChat>();
        item.transform.SetParent(content);
        item.User = twichUserSO.users[Random.Range(0, twichUserSO.users.Count)];
        item.InitChat(twichUserSO.icons[(int)item.User.badge], twichUserSO.colors[(int)item.User.color]);
        if (text == "")
            item.ShowChat(twichUserSO.GetRandom());
        else
            item.ShowChat(text);
        twichUserSO.AddPriviewChat(item.GetChat());
        RecentChat(item);
    }
    private void RecentChat(TwichChat twichChat)
    {
        recentChat.Add(twichChat);
        scrollbar.value = 0;
        if (recentChat.Count >= 20)
        {
            PoolManager.Instance.Push(recentChat[0].gameObject);
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
        StartCoroutine(CreateChat());
    }
}
