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
    //�󸶵����̳� �� ��ٸ��ž�? 1�� ������ 5�� ���� �ٺ���? �󸶳� ��ٸ��ųİ�! ��ȣ���� 182���ٲ𵿾� �Ϸ縸 ����� ��
    private void Start()
    {
        TestChat(true, "�󸶵����̳� �� ��ٸ��ž�?", "����");
        TestChat(true, "�� �ٺ�... Ȥ��.. ��..?", "����",EmojiType.None,ChatType.Monologue);
        TestChat(false, "1��", "�̳�");
        TestChat(true, "������", "����");
        TestChat(false, "5��", "�̳�");
        TestChat(true, "���� �ٺ���? �󸶳� ��ٸ��ųİ�!", "����");
        TestChat(false, "<b>��ȣ���� 182���ٲ𵿾�.</b>", "�̳�");
        TestNotice("-�Ϸ縸 ����� ��-");
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
