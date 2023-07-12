using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDataLoadWithUI : MonoBehaviour
{
    private Sheet<ChattingData> sheet = new();
    private TestChatUIManager chatUIManager = null;
    private void Start()
    {
        chatUIManager = FindObjectOfType<TestChatUIManager>();
        sheet.Load(SheetName.CHATTING_TEMPLATE, StartChatLog);
    }

    private void StartChatLog(List<ChattingData> datas)
    {
        StartCoroutine(Chatting());
    }

    private IEnumerator Chatting()
    {
        for (int i = 0; i < sheet.Length; i++)
        {
            bool isSend = false;
            bool isNotice = false;
            switch (sheet[i].sender)
            {
                case "Player": //플레이어
                    isSend = true;
                    break;
                case "System": //notice
                    isNotice = true;
                    break;
                default: //캐릭터
                    break;
            }
            if (isNotice)
            {
                yield return new WaitForSeconds(sheet[i].inputTime);
                chatUIManager.TestNotice(sheet[i].GetText());
            }
            else
            {
                chatUIManager.TestShowTyping(isSend);
                yield return new WaitForSeconds(sheet[i].inputTime);
                chatUIManager.TestRemoveTyping(isSend);
                chatUIManager.TestChat(isSend, sheet[i].GetText(), sheet[i].sender, sheet[i].reactionType);

                Debug.Log(sheet[i].GetText());
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
