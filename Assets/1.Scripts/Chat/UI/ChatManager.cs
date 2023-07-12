using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public enum ChatType
{
    Default = 0,
    Monologue, //독백
}
public enum EmojiType
{
    None = 0,
    Star = 1,
}
public class ChatManager : MonoBehaviour
{
    [SerializeField]
    protected GameObject otherArea, mineArea, noticeArea, otherTypingPrefab, mineTypingPrefab;

    protected Bubble LastArea;
    protected Bubble otherTyping, mineTyping;
    protected bool isother = false;

    protected void SetupTypingBubble(RectTransform contentRect)
    {
        otherTyping = CreateArea(contentRect, otherTypingPrefab);
        mineTyping = CreateArea(contentRect, mineTypingPrefab);

        otherTyping.gameObject.SetActive(false);
        mineTyping.gameObject.SetActive(false);
    }
    public void IsOtherPeople(Toggle isOther)
    {
        isother = isOther.isOn;
    }
    protected void ChatClear(RectTransform contentRect)
    {
        foreach (Transform child in contentRect.transform)
        {
            Destroy(child.gameObject);
        }
    }

    protected void Notice(RectTransform contentRect, Scrollbar scrollbar, string notice)
    {
        Bubble bubble = CreateArea(contentRect, noticeArea, notice);

        Fit(bubble.BoxRect);
        Fit(bubble.BubbleRect);
        Fit(contentRect);

        LastArea = bubble;

        StartCoroutine(ScrollDelay(scrollbar));
    }
    protected void ShowTyping(RectTransform contentRect, Scrollbar scrollbar, bool isSend, string user = "", Sprite picture = null)
    {
        Bubble bubble = null;

        if (isSend)
            bubble = mineTyping;
        else
            bubble = otherTyping;

        bubble.gameObject.SetActive(true);
        bubble.transform.SetAsLastSibling();

        LastArea = bubble;
        if (!isSend)
        {
            bubble.UserName = user;
            bubble.UserNameText.text = bubble.UserName;
        }

        StartCoroutine(ScrollDelay(scrollbar));
    }
    protected void RemoveTyping(bool isSend)
    {
        if (isSend)
        {
            mineTyping.gameObject.SetActive(false);
            return;
        }
        otherTyping.gameObject.SetActive(false);
    }
    protected void Chat(RectTransform contentRect, Scrollbar scrollbar, bool isSend, string text, string user, EmojiType emojiType = EmojiType.None, Sprite picture = null, ChatType chatType = ChatType.Default)
    {
        if (text.Trim() == "") return; //스페이스, 엔터 걸러줌
        text = text.Trim();
        bool isBottom = scrollbar.value <= 0.1f;

        Bubble bubble = CreateArea(contentRect, isSend ? mineArea : otherArea, text);

        if (picture != null && bubble.UserImage != null)
            bubble.UserImage.sprite = picture;

        switch (emojiType)
        {
            case EmojiType.None:
                bubble.Emojis.SetActive(false);
                break;
            case EmojiType.Star:
                bubble.Emojis.SetActive(true);
                break;
            default:
                break;
        }
        switch (chatType)
        {
            case ChatType.Default:
                break;
            case ChatType.Monologue:
                {
                    bubble.BoxRect.gameObject.GetComponent<Image>().color = new Color32(151, 191, 209, 255);
                    bubble.Trail.gameObject.GetComponent<Image>().color = new Color32(151, 191, 209, 255);
                    break;
                }
            default:
                break;
        }

        Fit(bubble.BoxRect);
        FitTextHeight(bubble);

        SetTime(bubble, user, isSend);

        Fit(bubble.BoxRect);
        Fit(bubble.BubbleRect);
        Fit(contentRect);
        LastArea = bubble;

        if (isSend || isBottom)
            StartCoroutine(ScrollDelay(scrollbar));
    }
    protected void Fit(RectTransform rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
    private IEnumerator ScrollDelay(Scrollbar scrollbar)
    {
        yield return new WaitForSeconds(0.03f);
        scrollbar.value = 0;
    }
    protected Bubble CreateArea(RectTransform contentRect, GameObject obj, string text = "")
    {
        Bubble bubble = Instantiate(obj).GetComponent<Bubble>();
        bubble.transform.SetParent(contentRect.transform, false);
        //bubble.BoxRect.sizeDelta = new Vector2(600, bubble.BoxRect.sizeDelta.y);
        if (text != string.Empty)
            bubble.TextRect.GetComponent<TMP_Text>().text = text;

        return bubble;
    }
    private void SetTime(Bubble bubble, string user, bool isSend)
    {
        DateTime t = DateTime.Now;
        bubble.UserName = user;

        //같은 시간에 같은 시람이 보냈는지 판별
        bool isSameUser = LastArea != null && LastArea.SendTime == bubble.SendTime && LastArea.UserName == bubble.UserName;
        bool isSameTime = LastArea != null && LastArea.SendTime == bubble.SendTime;
        //내가 같은 시간에 보냈으면 꼬리 없애기
        bubble.Trail.SetActive(!isSameUser);
        //타인이 같은 시간에 보냈으면 프사, 이름 없애기
        if (!isSend)
        {
            bubble.UserImage.gameObject.SetActive(!isSameUser);
            bubble.UserNameText.gameObject.SetActive(!isSameUser);
            bubble.UserNameText.text = bubble.UserName;
        }
    }

    /// <summary>
    ///두 줄 이상이면 크기를 줄여가면서 한 줄이 아래로 내려가면 바로 전 크기를 대입함 
    /// </summary>
    private void FitTextHeight(Bubble bubble)
    {

        float X = bubble.TextRect.sizeDelta.x + 42;
        float Y = bubble.TextRect.sizeDelta.y;
        if (Y > 60) //위 아래 여백이 원래 여백 초과 == 두 줄 이상
        {
            String[] tmpStr = bubble.TextRect.GetComponent<TMP_Text>().text.Split('\n');
            bool isLong = false;
            for (int i = 0; i < tmpStr.Length; i++)
            {
                if (tmpStr[i].Length > 1)
                {
                    isLong = true;
                    break;
                }
            }
            if (!isLong)
            {
                bubble.BoxRect.sizeDelta = new Vector2(bubble.TextRect.sizeDelta.x + 42, bubble.TextRect.sizeDelta.y);
                return;
            }
            for (int i = 0; i < 200; i++)
            {
                bubble.BoxRect.sizeDelta = new Vector2(X - i * 2, bubble.BoxRect.sizeDelta.y);

                Fit(bubble.BoxRect);
                if (Y != bubble.TextRect.sizeDelta.y)
                {
                    bubble.BoxRect.sizeDelta = new Vector2(X - (i * 2) + 2, Y);
                    break;
                }
            }
        }
        else
        {
            bubble.BoxRect.sizeDelta = new Vector2(X, Y);
        }

    }
}
