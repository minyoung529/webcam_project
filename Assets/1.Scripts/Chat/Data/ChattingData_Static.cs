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
    /// ä���� ������� �����ߴ� �����͸� ��ȯ�ϴ� �Լ�
    /// (ex. �� ǥ��, ��� ��, ���� �̸��� ���)
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
    PersonCount,    // ���� ������ ��� ��
    Emotion,        // ���� ������ ��� ǥ��
    Sex,            // ���� ����� ����
    Age,            // ���� ����� ����

    // Text
    PrevResponse,   // ������ ���õ� �亯 �ε���

    Count
}