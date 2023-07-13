using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using UnityEngine.ResourceManagement.ResourceLocations;

/// <summary>
/// ĳ���� �����͸� �޾� �ε��ϴ� UI �г�
/// </summary>
public class CharacterListPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private Image profileImage;

    public AssetLabelReference assetLabel;

    public void Init(Character character)
    {
        nameText.text = character.name;
        Addressables.LoadAssetAsync<Sprite>(character.profileLink.Trim()).Completed += SetSprite;
    }

    private void SetSprite(AsyncOperationHandle<Sprite> handle)
    {
        profileImage.sprite = handle.Result;
    }
}
