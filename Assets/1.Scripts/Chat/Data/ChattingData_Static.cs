using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ChattingData
{
    private static Dictionary<int, object> chattingDatas = new();

    public static void Set(ChattingDataType chattingDataType, object obj)
    {
        chattingDatas[GetHash(chattingDataType)] = obj;
    }

    /// <summary>
    /// ä���� ������� �����ߴ� �����͸� ��ȯ�ϴ� �Լ�
    /// (ex. �� ǥ��, ��� ��, ���� �̸��� ���)
    /// </summary>
    public static T Get<T>(ChattingDataType chattingDataType)
        => (T)chattingDatas[GetHash(chattingDataType)];

    public static T Get<T>(string chattingDataType)
    => (T)chattingDatas[GetHash(chattingDataType)];

    public static string GetString<T>(string chattingDataType)
    {
        return ((T)chattingDatas[GetHash(chattingDataType)]).ToString();
    }


    private static int GetHash<T>(T value)
    {
        return value.ToString().GetHashCode();
    }
}

public enum ChattingDataType
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