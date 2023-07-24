using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

/// <summary>
/// 영상통화 받아서 Character Data UI에 Update
/// </summary>
public class RecieveVideoCall : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image profileImage;

    private Character character;

    private void Awake()
    {
        EventManager<List<Character>>.StartListening(EventName.OnCharacterLoadComplete, Init);
    }
    private void Init(List<Character> list)
    {
        if(list.Count < 1) return;
        character = list[0];

        SetName();
        Addressables.LoadAssetAsync<Sprite>(character.profileLink.Trim()).Completed += SetSprite;
    }
    private void SetSprite(AsyncOperationHandle<Sprite> handle)
    {
        profileImage.sprite = handle.Result;
    }
    private void SetName()
    {
        nameText.SetText(character.name);
    }

    private void OnDestroy()
    {
        EventManager<List<Character>>.StopListening(EventName.OnCharacterLoadComplete, Init);
    }
}
