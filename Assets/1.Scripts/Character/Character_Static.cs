using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character
{
    private static Sheet<Character> characterSheet;
    public static Sheet<Character> CharacterSheet
    {
        get
        {
            if (characterSheet == null)
            {
                Debug.LogError("��Ʈ �ε� ���Դϴ�.");
                return null;
            }

            return characterSheet;
        }
    }

    public static void LoadCharacter()
    {
        characterSheet = new();
        characterSheet.Load(SheetName.CHARACTERS, TriggerCompleteEvent);
    }

    private static void TriggerCompleteEvent(List<Character> list)
    {
        EventManager<List<Character>>.TriggerEvent(EventName.OnCharacterLoadComplete, list);
    }

    #region Access
    public Character GetCharacter(CharacterName characterName)
    {
        return GetCharacter(characterName.ToString());
    }

    public Character GetCharacter(string characterName)
    {
        List<Character> characterList = new (CharacterSheet.List);
        Character character = characterList.Find(x => x.englishName == characterName);

        if (character == null)
        {
            Debug.LogError("CharacterName�� ��ϵ��� ���� ���� �̸��Դϴ�.");
        }

        return character;
    }

    #endregion
}

public enum CharacterName
{
    NamSoJeong,
    LeeMinYoung,

    Count
}