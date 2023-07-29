using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TextCore.Text;

public class TestDataLoadWithUI : MonoBehaviour
{
    private Sheet<ChattingData> sheet = new();
    private TestChatUIManager chatUIManager = null;
    private SelectionUI selectionUI = null;
    private Dictionary<string, Sprite> characterSpriteDictionary = new();
    private void Start()
    {
        chatUIManager = FindObjectOfType<TestChatUIManager>();
        selectionUI = FindObjectOfType<SelectionUI>();
        Character.LoadCharacter();
        EventManager<List<Character>>.StartListening(EventName.OnCharacterLoadComplete, InitCharacter);
    }

    private void InitCharacter(List<Character> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int a = i;
            Addressables.LoadAssetAsync<Sprite>(list[a].profileLink.Trim()).Completed +=
                (AsyncOperationHandle<Sprite> sprite) =>
                {
                    SetSprite(list[a].englishName, sprite);
                };
        }
        sheet.Load(SheetName.CHATTING_TEMPLATE, StartChatLog);
    }

    private void StartChatLog(List<ChattingData> datas)
    {
        StartCoroutine(Chatting());
    }
    private void SetSprite(string englishName, AsyncOperationHandle<Sprite> sprite)
    {
        characterSpriteDictionary.Add(englishName, sprite.Result);
    }
    private IEnumerator ShowTypingBubble(bool isSend, string characterName, string sender, float inputTime)
    {
        chatUIManager.TestShowTyping(isSend, characterName, characterSpriteDictionary[sender]);
        yield return new WaitForSeconds(inputTime);
        chatUIManager.TestRemoveTyping(isSend);
    }
    private IEnumerator Chatting()
    {
        bool isSend = false;
        bool isNotice = false;
        ChattingType chattingType = ChattingType.Chat;
        for (int i = 0; i < sheet.Length; i++)
        {
            isSend = false;
            isNotice = false;
            chattingType = sheet[i].chattingType;

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
                string characterName = Character.GetCharacter(sheet[i].sender).name;
                switch (chattingType)
                {
                    case ChattingType.Chat:
                    case ChattingType.CalculateSelect:
                        ShowTypingBubble(isSend, characterName, sheet[i].sender, sheet[i].inputTime);

                        chatUIManager.TestChat(isSend, sheet[i].GetText(), characterName, characterSpriteDictionary[sheet[i].sender], sheet[i].reactionType, ChatType.Default);
                        break;
                    case ChattingType.Select:
                        ShowTypingBubble(isSend, characterName, sheet[i].sender, sheet[i].inputTime);
                        selectionUI.SetSelection(sheet[i].GetAllSelecteText());
                        yield return new WaitUntil(()=>selectionUI.selectionIndex != -1);
                        sheet[i].selectedIndx = selectionUI.selectionIndex;
                        selectionUI.ResetSelection();
                        chatUIManager.TestChat(isSend, sheet[i].GetText(), characterName, characterSpriteDictionary[sheet[i].sender], sheet[i].reactionType, ChatType.Default);
                        break;
                    case ChattingType.Camera:
                    case ChattingType.Input:
                    case ChattingType.Count:
                        break;
                    default:
                        break;
                }

                Debug.Log(sheet[i].GetText());
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
