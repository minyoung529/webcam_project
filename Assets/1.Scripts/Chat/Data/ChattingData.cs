using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ��Ʈ���� �޾ƿ��� ä�� ������
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

    public int selectedIndx = -1;

    public string GetText()
    {
        switch (chattingType)
        {
            case ChattingType.Chat:
                return text;

            case ChattingType.CalculateSelect:
                return CalculateSelectedText();

            case ChattingType.Select:
                ChattingData.Set(ChattingDataType.PrevResponse, selectedIndx);
                return GetSelectedText(selectedIndx);

            default:
                return "";
        }
    }
}

public enum ChattingType
{
    Chat,               // ��縦 �״�� ������
    Select,             // ��縦 $ Split���� �ɰ��� ����â�� ����
    CalculateSelect,    // ����� ������ �м��� ������
    Camera,             // ī�޶� UI�� ���� ���� ������ ������
    Input,              // �÷��̾ �Է��� �� �ֵ��� �Ѵ�

    Count
}

public enum FileType
{
    None,               // ÷�� ���� X
    Picture,            // ����
    Gif,                // ��©
    Sound,              // �Ҹ�
    Video,              // ������

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
    AI,                 // ���� �޽��� �������� AI�� �м�

    Count
}