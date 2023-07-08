using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ε�� ��Ʈ�� ����Ʈ ��ü�� ������ ��ũ����
/// </summary>
/// <typeparam name="T"></typeparam>
public class Sheet<T>
{
    private List<T> list = new List<T>();
    public IReadOnlyList<T> List { get { return list; } }
    public int Length => list.Count;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= list.Count)
            {
                Debug.LogError($"Index Out Of Range (Spread Sheet Load) : {typeof(T)}");
            }
            return list[index];
        }
    }

    public void Load(string sheetName, Action<List<T>> onComplete)
    {
        // Set List�� ���� ���� ����Ǿ�� �Ѵ�
        Action<List<T>> callback = SetList + onComplete;
        SheetManager.Instance.LoadSheet(sheetName, callback);
    }

    private void SetList(List<T> list)
    {
        this.list = list;
    }
}
