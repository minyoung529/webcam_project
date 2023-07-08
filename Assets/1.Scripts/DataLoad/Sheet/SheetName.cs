using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʈ �̸��� ��Ƴ��� Ŭ����
/// </summary>
public static class SheetName
{
    public const string CHARACTERS = "Characters";
    public const string CHATTING_TEMPLATE = "ChattingTemplate";

    /// <summary>
    /// ä�� or ������ȭ ��Ʈ �̸��� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="chracterName">MUST English</param>
    /// <param name="dateType">Chatting or Video Call</param>
    public static string GetSheetName(string chracterName, DateType dateType, int order)
    {
        return $"{chracterName}_{dateType}_{order}";
    }
}
