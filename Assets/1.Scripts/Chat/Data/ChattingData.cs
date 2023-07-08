using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스프레드 시트에서 받아오는 채팅 데이터
/// </summary>
public partial class ChattingData
{
    private string text;
    public string sender;
    public float inputTime;
    public ChattingType chattingType;

    public bool hasFile;
    public FileType fileType;
    public string fileAddress;
    public ReactionType reactionType;

    public string GetText()
    {
        switch (chattingType)
        {
            case ChattingType.Chat:
                return text;

            case ChattingType.CalculateSelect:
                return CalculateSelectedText();

            // Test Code
            case ChattingType.Select:
                ChattingData.Set(ChattingDataType.PrevResponse, Random.Range(0,3));
                return GetSelectedText(1);

            default:
                return "";
        }
    }
}

public enum ChattingType
{
    Chat,               // 대사를 그대로 보낸다
    Select,             // 대사를 $ Split으로 쪼개어 선택창을 띄운다
    CalculateSelect,    // 대사의 조건을 분석해 보낸다
    Camera,             // 카메라 UI를 띄우고 찍은 사진을 보낸다
    Input,              // 플레이어가 입력할 수 있도록 한다

    Count
}

public enum FileType
{
    None,               // 첨부 파일 X
    Picture,            // 사진
    Gif,                // 움짤
    Sound,              // 소리
    Video,              // 동영상

    Count
}

public enum ReactionType
{
    None,
    Heart,
    Good,
    Check,
    Sad,
    Fun,
    AI,                 // 직전 메시지 바탕으로 AI가 분석

    Count
}