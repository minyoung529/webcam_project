using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

/// <summary>
/// 캐릭터 데이터를 받아 로딩하는 UI 패널
/// </summary>
public class CharacterListPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro nameText;

    [SerializeField]
    private Image profileImage;


    public void Init(Character character)
    {
        nameText.text = character.name;
        Addressables.LoadAssetAsync<Sprite>(character.profileLink).Completed += SetSprite;
    }

    private void SetSprite(AsyncOperationHandle<Sprite> handle)
    {
        profileImage.sprite = handle.Result;
    }
}
