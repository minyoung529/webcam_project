using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestChatUIManager : ChatManager
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

    private void Start()
    {
        SetupTypingBubble(contentRect);
    }
    public void TestChatClear()
    {
        ChatClear(contentRect);
    }
    public void TestNotice(string notice)
    {
        Notice(contentRect, scrollbar, notice);
    }
    public void TestShowTyping(bool isSend, string user = "", Sprite picture = null)
    {
        ShowTyping(contentRect,scrollbar, isSend, user, picture);
    }
    public void TestRemoveTyping(bool isSend)
    {
        RemoveTyping(isSend);
    }
    public void TestChat(bool isSend, string text, string user, Sprite picture = null,ReactionType emojiType = ReactionType.None, ChatType chatType = ChatType.Default)
    {
        Chat(contentRect, scrollbar, isSend, text, user, emojiType, picture, chatType);
    }
}
