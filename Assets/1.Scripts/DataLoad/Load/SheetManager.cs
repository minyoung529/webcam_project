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
    /// ��Ʈ�� �ε��ϴ� �Լ�
    /// </summary>
    /// <param name="sheetName">SO�� �ִ� ��Ʈ �̸�</param>
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

        // ����� �����Ѵ�
        for (int i = 1; i < rows.Length; i++)
        {
            string[] datas = rows[i].Split('\t');
            list.Add(GetData<T>(datas));
        }

        return list;
    }

    /// <summary>
    /// �� ���� �����͵��� T�� ��ü�� ��ȯ�ϴ� �Լ�
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

                // ������ �´� �ڷ������� �Ľ��ؼ� �ִ´�
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
