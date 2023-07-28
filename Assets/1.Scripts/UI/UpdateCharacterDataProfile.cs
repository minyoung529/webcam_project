using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UpdateCharacterDataProfile : MonoBehaviour
{
    [Header("Character UI")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image profileImage;

    public void Trigger(Character send)
    {
        // Sender UI Update
        SetName(send);
        Addressables.LoadAssetAsync<Sprite>(send.profileLink.Trim()).Completed += SetSprite;
    }

    #region Sender UI
    private void SetSprite(AsyncOperationHandle<Sprite> handle)
    {
        profileImage.sprite = handle.Result;
    }
    private void SetName(Character character)
    {
        nameText.SetText(character.name);
    }
    #endregion
}
