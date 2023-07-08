using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_DataLoad : MonoBehaviour
{
    private Sheet<ChattingData> sheet = new();

    void Start()
    {
        sheet.Load(SheetName.CHATTING_TEMPLATE, StartChatLog);
    }


    private void StartChatLog(List<ChattingData> datas)
    {
        StartCoroutine(Chatting());
    }

    private IEnumerator Chatting()
    {
        for(int i = 0; i < sheet.Length; i++)
        {
            yield return new WaitForSeconds(sheet[i].inputTime);
            Debug.Log(sheet[i].GetText());
        }
    }
}
