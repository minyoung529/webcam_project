using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// https://docs.google.com/spreadsheets/d/{_____spreadID_____}/edit#gid={_______sheetID_______}


[CreateAssetMenu(fileName = "Sheet", menuName = "Data/SheetSO")]
public class SheetSO : ScriptableObject
{
    [SerializeField]
    private string sheetName;

    /// <summary>
    /// 스프레드 시트의 아이디
    /// </summary>
    [SerializeField]
    [TextArea(2,4)]
    private string spreadID;

    /// <summary>
    /// 시트의 아이디
    /// </summary>
    [SerializeField]
    private string sheetGID;

    #region Property
    public string SpreadID => spreadID;
    public string SheetID => sheetGID;
    public string SheetName => sheetName;
    #endregion
}
