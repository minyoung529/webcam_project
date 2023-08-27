using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ChattingData
{
    private static Dictionary<int, object> chattingDatas = new();

    public static void Set(InformationType chattingDataType, object obj)
    {
        chattingDatas[GetHash(chattingDataType.ToString())] = obj;
    }

    /// <summary>
    /// 채팅을 기반으로 저장했던 데이터를 반환하는 함수
    /// (ex. 얼굴 표정, 사람 수, 반응 이모지 등등)
    /// </summary>
    public static T Get<T>(InformationType chattingDataType)
        => (T)chattingDatas[GetHash(chattingDataType.ToString())];

    public static T Get<T>(string chattingDataType)
    => (T)chattingDatas[GetHash(chattingDataType.ToString())];

    public static string GetString<T>(string chattingDataType)
    {
        return ((T)chattingDatas[GetHash(chattingDataType.ToString())]).ToString();
    }


    private static int GetHash<T>(T value)
    {
        return value.ToString().GetHashCode();
    }
}

public enum InformationType
{
    // Picture
    PersonCount,    // 직전 사진의 사람 수
    Emotion,        // 직전 사진의 사람 표정
    Sex,            // 직전 사람의 성별
    Age,            // 직전 사람의 나이

    // Text
    PrevResponse,   // 직전의 선택된 답변 인덱스

    Count
}