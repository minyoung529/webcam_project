using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 시트 이름을 모아놓는 클래스
/// </summary>
public static class SheetName
{
    public const string CHARACTERS = "Characters";
    public const string CHATTING_TEMPLATE = "ChattingTemplate";

    /// <summary>
    /// 채팅 or 영상통화 시트 이름을 반환하는 함수
    /// </summary>
    /// <param name="chracterName">MUST English</param>
    /// <param name="dateType">Chatting or Video Call</param>
    public static string GetSheetName(string chracterName, DateType dateType, int order)
    {
        return $"{chracterName}_{dateType}_{order}";
    }
}
