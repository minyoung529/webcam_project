using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestChatManager : ChatManager
{
    [SerializeField]
    private TMP_InputField textField;
    [SerializeField]
    private RectTransform contentRect;
    [SerializeField]
    private Scrollbar scrollbar;
    [SerializeField]
    private Button chatButton;
    [SerializeField]
    private GameObject ImageAreaPrefab;
    //얼마동안이나 나 기다린거야? 1분 거짓말 5분 내가 바보냐? 얼마나 기다린거냐고! 신호등이 182번바뀔동안 하루만 사랑해 중
    private void Start()
    {
        TestChat(true, "얼마동안이나 나 기다린거야?", "소정");
        TestChat(true, "저 바보... 혹시.. 또..?", "소정",EmojiType.None,ChatType.Monologue);
        TestChat(false, "1분", "미녕");
        TestChat(true, "거짓말", "소정");
        TestChat(false, "5분", "미녕");
        TestChat(true, "내가 바보냐? 얼마나 기다린거냐고!", "소정");
        TestChat(false, "<b>신호등이 182번바뀔동안.</b>", "미녕");
        TestNotice("-하루만 사랑해 중-");
    }
    public void TestChatClear()
    {
        ChatClear(contentRect);
    }
    public void TestNotice(string notice)
    {
        Notice(contentRect, scrollbar, notice);
    }
    public void TestChat(bool isSend, string text, string user, EmojiType emojiType = EmojiType.None, ChatType chatType = ChatType.Default)
    {
        Chat(contentRect, scrollbar, isSend, text, user, emojiType, null, chatType);
    }
}
