using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ChattingData
{
    public string GetSelectedText(int idx)
    {
        string[] texts = text.Split('$');

        if (idx >= texts.Length || idx < 0)
        {
            Debug.LogError("Index Out Of Range - GetSelectedText");
        }

        return texts[idx];
    }

    private string CalculateSelectedText()
    {
        // /{자료형} {변수명}/조건~
        string[] conditions = text.Split('/');
        string[] header = conditions[0].Split(' ');
        string dataType = header[0];
        string variableName = header[1];

        for (int i = 1; i < conditions.Length; i++)
        {
            // N{연산자}>{대사}
            string[] datas = conditions[i].Split('>');

            string data = datas[0].Substring(0, datas[0].Length - 1);         // 비교군
            string experimental = GetParameters(dataType, variableName); // 실험군

            bool result = false;

            switch (datas[0][^1])
            {
                case '+':
                    result = !EqualCompare(dataType, experimental, data) && !MinCompare(dataType, experimental, data);
                    // 작지 않고 같지도 않다면
                    break;

                case '-':
                    result = MinCompare(dataType, experimental, data);
                    break;

                case '!':
                    result = !EqualCompare(dataType, data, experimental);
                    break;

                case '=':
                    result = EqualCompare(dataType, data, experimental);
                    break;
            }

            if (result)
                return datas[1];    // 대사 반환
        }

        Debug.LogError("대사가 어떠한 조건도 통과하지 못했습니다.");
        return "";
    }

    private bool EqualCompare(string dataType, string data1, string data2)
    {
        if (dataType == "string" || dataType == "int")
        {
            return data1.Equals(data2);
        }

        if (dataType == "float")
        {
            return float.Parse(data1) == float.Parse(data2);
        }

        if (dataType == "bool")
        {
            if (data1 == "0")
                data1 = "false";
            else if (data1 == "1")
                data1 = "true";

            return bool.Parse(data1) == bool.Parse(data2);
        }

        Debug.LogError("EqualCompare Not Defined Data Type");
        return false;
    }

    private bool MinCompare(string dataType, string data1, string data2)
    {
        // not string, boolean

        if (dataType == "float")
        {
            return float.Parse(data1) < float.Parse(data2);
        }

        if (dataType == "int")
        {
            return int.Parse(data1) < int.Parse(data2);
        }

        Debug.LogError("MinCompare Not Defined Data Type");
        return false;
    }

    private string GetParameters(string dataType, string variableName)
    {
        switch (dataType)
        {
            case "int":
                return GetString<int>(variableName);
            case "float":
                return GetString<float>(variableName);

            case "bool":
                return GetString<bool>(variableName);

            case "string":
                return GetString<string>(variableName);
        }

        Debug.LogError("Unexpected Data Type");
        return "";
    }
}
