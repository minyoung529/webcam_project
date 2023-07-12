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

    private void Start()
    {
        SetupTypingBubble(contentRect);

        TestChat(true, "�󸶵����̳� �� ��ٸ��ž�?", "����");
        TestChat(true, "�� �ٺ�... Ȥ��.. ��..?", "����",EmojiType.None,ChatType.Monologue);
        TestChat(false, "1��", "�̳�");
        TestChat(true, "������", "����");
        TestChat(false, "5��", "�̳�");
        TestChat(true, "���� �ٺ���? �󸶳� ��ٸ��ųİ�!", "����");
        TestChat(false, "��ȣ���� 182���ٲ𵿾�.", "�ο�");
        TestNotice("-�Ϸ縸 ����� ��-");
        TestShowTyping(false,"�̳�");
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
    public void TestChat(bool isSend, string text, string user, EmojiType emojiType = EmojiType.None, ChatType chatType = ChatType.Default)
    {
        Chat(contentRect, scrollbar, isSend, text, user, emojiType, null, chatType);
    }
}
