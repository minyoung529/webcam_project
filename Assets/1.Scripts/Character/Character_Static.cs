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
                Debug.LogError("시트 로딩 전입니다.");
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
            Debug.LogError("CharacterName에 등록되지 않은 영어 이름입니다.");
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