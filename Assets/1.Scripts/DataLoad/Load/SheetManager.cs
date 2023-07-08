using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class SheetManager : MonoSingleton<SheetManager>
{
    [SerializeField]
    private SheetSOHolder sheets;

    /// <summary>
    /// 시트를 로드하는 함수
    /// </summary>
    /// <param name="sheetName">SO에 있는 시트 이름</param>
    public void LoadSheet<T>(string sheetName, Action<List<T>> onCompleteLoad)
    {
        SheetSO sheetData = sheets.Get(sheetName);

        if (sheetData == null)
        {
            Debug.LogError("Sheet Data SO is Null!");
        }
        else
        {
            StartCoroutine(LoadCoroutine(sheetData, onCompleteLoad));
        }
    }

    private IEnumerator LoadCoroutine<T>(SheetSO sheetSO, Action<List<T>> onCompleteLoad)
    {
        string url = GetSheetLink(sheetSO);
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        string tsvData = www.downloadHandler.text;
        onCompleteLoad.Invoke(GetDataAsList<T>(tsvData));
    }

    private List<T> GetDataAsList<T>(string tsv)
    {
        string[] rows = tsv.Split('\n');
        List<T> list = new List<T>();

        // 헤더는 제외한다
        for (int i = 1; i < rows.Length; i++)
        {
            string[] datas = rows[i].Split('\t');
            list.Add(GetData<T>(datas));
        }

        return list;
    }

    /// <summary>
    /// 한 행의 데이터들을 T형 객체로 반환하는 함수
    /// </summary>
    private T GetData<T>(string[] datas)
    {
        object data = Activator.CreateInstance(typeof(T));
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < datas.Length; i++)
        {
            try
            {
                // string > parse
                Type type = fields[i].FieldType;

                if (string.IsNullOrEmpty(datas[i])) continue;

                // 변수에 맞는 자료형으로 파싱해서 넣는다
                if (type == typeof(int))
                    fields[i].SetValue(data, int.Parse(datas[i]));

                else if (type == typeof(float))
                    fields[i].SetValue(data, float.Parse(datas[i]));

                else if (type == typeof(bool))
                {
                    if (datas[i] == "0" || datas[i] == "1")
                    {
                        datas[i] = datas[i] == "1" ? "true" : "false";
                    }

                    fields[i].SetValue(data, bool.Parse(datas[i]));
                }

                else if (type == typeof(string))
                    fields[i].SetValue(data, datas[i]);

                // enum
                else
                    fields[i].SetValue(data, Enum.Parse(type, datas[i]));
            }

            catch (Exception e)
            {
                Debug.LogError($"SpreadSheet Error : {e.Message} {fields[i].Name}");
            }

        }

        return (T)data;
    }

    private string GetSheetLink(SheetSO sheetSO)
        => $"docs.google.com/spreadsheets/d/{sheetSO.SpreadID}/export?format=tsv&gid={sheetSO.SheetID}";
}
