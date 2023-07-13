using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterListGenerator : MonoBehaviour
{
    [SerializeField]
    private CharacterListPanel panelPrefab;

    [SerializeField]
    private Transform rootTransform;

    private void Awake()
    {
        EventManager<List<Character>>.StartListening(EventName.OnCharacterLoadComplete, Generate);
    }

    private void Generate(List<Character> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            CharacterListPanel newPanel = Instantiate(panelPrefab, rootTransform);
            newPanel.Init(list[i]);
        }
    }
}
